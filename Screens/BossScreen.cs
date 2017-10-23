using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SpaceShooter
{
    class BossScreen
    {
        const int MAX_ENEMIES1 = 5;
        const int MAX_ENEMIES2 = 5;
        const int MAX_ENEMIES3 = 5;
        const int MAX_ROCK = 5;

        SoundEffect sound;
        Song song;

        Boss boss;
        Player ship;
        EnemyType1[] enemies1;
        EnemyType2[] enemies2;
        EnemyType3[] enemies3;
        Rock[] rock;

        SpriteFont font01;
        Texture2D back01;
        Texture2D back02;
        Texture2D back03;
        Texture2D back04;
        Texture2D back05;

        Vector2 scroll01;
        Vector2 scroll02;
        Vector2 scroll03;
        Vector2 scroll04;

        int framesCounter;
        int timeCounter;
        int time;
        int intro;

        bool pause;

        public BossScreen(ContentManager content)
        {
            scroll01 = Vector2.Zero;
            scroll02 = Vector2.Zero;
            scroll03 = Vector2.Zero;
            scroll04 = Vector2.Zero;

            time = GameplayScreen.time;

            pause = false;

            sound = content.Load<SoundEffect>("audio/Laser");
            song = content.Load<Song>("audio/Genos");

            //TEXTURES
            font01 = content.Load<SpriteFont>("fonts/NewSpriteFont");
            back01 = content.Load<Texture2D>("graphics/backLvl101");
            back02 = content.Load<Texture2D>("graphics/backLvl102");
            back03 = content.Load<Texture2D>("graphics/backLvl103");
            back04 = content.Load<Texture2D>("graphics/backLvl104");
            back05 = content.Load<Texture2D>("graphics/backSmoke");

            boss = new Boss(content.Load<Texture2D>("graphics/boss01"),
                    content.Load<Texture2D>("graphics/explosion_basic"),
                    content.Load<Texture2D>("graphics/shot003"),
                    content.Load<Texture2D>("graphics/barboss"),
                    content.Load<Texture2D>("graphics/barlife01"),
                    content.Load<Texture2D>("graphics/barlife02"),
                    content.Load<Texture2D>("graphics/superShot"),
                    content.Load<Texture2D>("graphics/shot005"));

            ship = new Player(content.Load<Texture2D>("graphics/player001"),
                              content.Load<Texture2D>("graphics/explosion_basic"),
                              content.Load<Texture2D>("graphics/shot001"),
                              content.Load<Texture2D>("graphics/barlife01"),
                              content.Load<Texture2D>("graphics/barmana01"),
                              content.Load<Texture2D>("graphics/barplayer"),
                              content.Load<Texture2D>("graphics/barlife02"),
                              content.Load<Texture2D>("graphics/barmana02"),
                              content.Load<Texture2D>("graphics/superShot1"),
                              content.Load<Texture2D>("graphics/superShot2"));
            //ROCK
            rock = new Rock[MAX_ROCK];

            for (int i = 0; i < MAX_ROCK; i++) rock[i] = new Rock (content.Load<Texture2D>("graphics/rock01"), 
                                                                   content.Load<Texture2D>("graphics/explosion_basic"));

            //ENEMIES 1
            enemies1 = new EnemyType1[MAX_ENEMIES1];

            for (int i = 0; i < MAX_ENEMIES1; i++)
            {
                if (i / (MAX_ENEMIES1 / 2) == 0) enemies1[i] = new EnemyType1(content.Load<Texture2D>("graphics/enemy001"),
                                                                        content.Load<Texture2D>("graphics/explosion_basic"),
                                                                        content.Load<Texture2D>("graphics/shot002"),
                                                                        content.Load<Texture2D>("graphics/barlife02"),
                                                                        content.Load<Texture2D>("graphics/barlife01"),
                                                                        content.Load<Texture2D>("graphics/barenemy"));

                else enemies1[i] = new EnemyType1(content.Load<Texture2D>("graphics/enemy002"),
                                            content.Load<Texture2D>("graphics/explosion_basic"),
                                            content.Load<Texture2D>("graphics/shot002"),
                                            content.Load<Texture2D>("graphics/barlife02"),
                                            content.Load<Texture2D>("graphics/barlife01"),
                                            content.Load<Texture2D>("graphics/barenemy"));
            }

            //ENEMIES 2
            enemies2 = new EnemyType2[MAX_ENEMIES2];

            for (int i = 0; i < MAX_ENEMIES2; i++)
            {
                if (i / (MAX_ENEMIES2 / 2) == 0) enemies2[i] = new EnemyType2(content.Load<Texture2D>("graphics/enemy001"),
                                                                        content.Load<Texture2D>("graphics/explosion_basic"),
                                                                        content.Load<Texture2D>("graphics/shot002"),
                                                                        content.Load<Texture2D>("graphics/barlife02"),
                                                                        content.Load<Texture2D>("graphics/barlife01"),
                                                                        content.Load<Texture2D>("graphics/barenemy"));

                else enemies2[i] = new EnemyType2(content.Load<Texture2D>("graphics/enemy002"),
                                            content.Load<Texture2D>("graphics/explosion_basic"),
                                            content.Load<Texture2D>("graphics/shot002"),
                                            content.Load<Texture2D>("graphics/barlife02"),
                                            content.Load<Texture2D>("graphics/barlife01"),
                                            content.Load<Texture2D>("graphics/barenemy"));
            }

            //ENEMIES 3
            enemies3 = new EnemyType3[MAX_ENEMIES3];

            for (int i = 0; i < MAX_ENEMIES3; i++)
            {
                enemies3[i] = new EnemyType3(content.Load<Texture2D>("graphics/enemy003"),
                                             content.Load<Texture2D>("graphics/explosion_basic"),
                                             content.Load<Texture2D>("graphics/shot004"),
                                             content.Load<Texture2D>("graphics/barlife02"),
                                             content.Load<Texture2D>("graphics/barlife01"),
                                             content.Load<Texture2D>("graphics/barenemy"));
            }
        }

        public void Update(InputManager input)
        {
            framesCounter++;

            if (framesCounter == 0) MediaPlayer.Play(song);

            if (input.IsKeyDown(Keys.P)) pause = true;
            if (input.IsKeyUp(Keys.P)) pause = false;

            if (!pause)
            {
                timeCounter++;

                if (timeCounter == 60)
                {
                    GameplayScreen.time++;
                    intro++;
                    timeCounter = 0;
                }

                //SCROLL BACKGROUND
                scroll01.X -= 6;

                if (scroll01.X <= -back02.Width) scroll01.X = 0;

                scroll02.X -= 2;

                if (scroll02.X <= -back03.Width) scroll02.X = 0;

                scroll03.X -= 4;

                if (scroll03.X <= -back04.Width) scroll03.X = 0;

                scroll04.X -= 1;

                if (scroll04.X <= -back05.Width) scroll04.X = 0;

                // BOSS-PLAYER UPDATE

                if (GameplayScreen.time >= 35)
                {
                    for (int i = 0; i < MAX_ENEMIES1; i++)
                    {
                        enemies1[i].Update();
                        sound.Play();
                    }
                    for (int i = 0; i < MAX_ENEMIES2; i++)
                    {
                        sound.Play();
                        enemies2[i].Update();
                    }
                    for (int i = 0; i < MAX_ENEMIES3; i++)
                    {
                        sound.Play();
                        enemies3[i].Update();
                    }
                }

                if (GameplayScreen.time >= 50)
                {
                    for (int i = 0; i < MAX_ROCK; i++) rock[i].Update();
                }

                boss.Update();
                ship.Update(input);

                //COLLISIONS
                //ENEMIES - PLAYER
                for (int i = 0; i < MAX_ROCK; i++)
                {
                    if (ship.CheckCollision(rock[i].Bounds))
                    {
                        ship.ReceiveDamage(50);
                    }
                }

                for (int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if (ship.CheckCollision(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                    }
                }

                for (int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if (ship.CheckCollision(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                    }
                }

                for (int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if (ship.CheckCollision(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                    }
                }

                //PLAYER SHOTS - ENEMIES
                for (int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if (ship.CheckCollisionShots(rock[i].Bounds))
                    {
                        rock[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if (ship.CheckCollisionShots(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if (ship.CheckCollisionShots1(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if (ship.CheckCollisionShots2(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(300);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if (ship.CheckCollisionShots(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if (ship.CheckCollisionShots1(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if (ship.CheckCollisionShots2(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(300);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if (ship.CheckCollisionShots(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if (ship.CheckCollisionShots1(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(100);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                for (int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if (ship.CheckCollisionShots2(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(300);

                        GameplayScreen.score += 100;
                        if (GameplayScreen.score > GameplayScreen.hiscore) GameplayScreen.hiscore = GameplayScreen.score;
                    }
                }

                //ENEMIES SHOTS - PLAYER
                for (int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if (enemies1[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                    }
                }

                for (int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if (enemies2[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                    }
                }

                for (int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if (enemies3[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                    }
                }

                //BOSS  -PLAYER
                if (ship.CheckCollision(boss.Bounds))
                {
                    ship.ReceiveDamage(20);
                    boss.ReceiveDamage(2);
                }

                //PLAYER SHOTS - BOSS
                if (ship.CheckCollisionShots(boss.Bounds))
                {
                    boss.ReceiveDamage(10);
                    GameplayScreen.score = GameplayScreen.score + 100;
                }

                if (ship.CheckCollisionShots1(boss.Bounds))
                {
                    boss.ReceiveDamage(10);
                    GameplayScreen.score = GameplayScreen.score + 100;
                }

                if (ship.CheckCollisionShots2(boss.Bounds))
                {
                    boss.ReceiveDamage(25);
                    GameplayScreen.score = GameplayScreen.score + 100;
                }

                //BOSS SHOTS - PLAYER
                if (boss.CheckCollisionShots(ship.Bounds))
                {
                    ship.ReceiveDamage(20);
                }

                if (boss.CheckCollisionShots1(ship.Bounds))
                {
                    ship.ReceiveDamage(20);
                }

                if (boss.CheckCollisionShots2(ship.Bounds))
                {
                    ship.ReceiveDamage(45);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //DRAW BACKGROUND
            spriteBatch.Draw(back01, Vector2.Zero, Color.White);
            spriteBatch.Draw(back02, scroll01, Color.White);
            spriteBatch.Draw(back02, scroll01 + new Vector2(SpaceGame.SCREEN_WIDTH, 0), Color.White);
            spriteBatch.Draw(back03, scroll02, Color.White);
            spriteBatch.Draw(back03, scroll02 + new Vector2(SpaceGame.SCREEN_WIDTH, 0), Color.White);
            spriteBatch.Draw(back04, scroll03, Color.White);
            spriteBatch.Draw(back04, scroll03 + new Vector2(SpaceGame.SCREEN_WIDTH, 0), Color.White);
            spriteBatch.Draw(back05, scroll04, Color.White);
            spriteBatch.Draw(back05, scroll04 + new Vector2(SpaceGame.SCREEN_WIDTH, 0), Color.White);

            //DRAW BARS
            spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, 50), Color.Black);
            spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, SpaceGame.SCREEN_HEIGHT - 50, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black);

            if (pause)
            {
                spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black * 0.5f);

                if ((framesCounter / 30) % 2 == 1)
                {
                    spriteBatch.DrawString(font01, "Paused", new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 75, SpaceGame.SCREEN_HEIGHT / 2 - 15), Color.Yellow);
                }
            }

            if (!pause)
            {
                //INTRO
                if (intro <= 2)
                {
                    if ((framesCounter / 10) % 2 == 1)
                    {
                        spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 50, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT - 100), Color.Black);
                    }
                }

                //DRAW PLAYER - BOSS - ENEMIES
                if (GameplayScreen.time >= 35)
                {
                    for (int i = 0; i < MAX_ENEMIES1; i++) enemies1[i].Draw(spriteBatch);
                    for (int i = 0; i < MAX_ENEMIES2; i++) enemies2[i].Draw(spriteBatch);
                    for (int i = 0; i < MAX_ENEMIES3; i++) enemies3[i].Draw(spriteBatch);
                }

                if (GameplayScreen.time >= 50) for (int i = 0; i < MAX_ROCK; i++) rock[i].Draw(spriteBatch);

                ship.Draw(spriteBatch);
                boss.Draw(spriteBatch);

                //DRAW SCORE
                spriteBatch.DrawString(font01, "SCORE " + GameplayScreen.score.ToString("00000"), new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 100, SpaceGame.SCREEN_HEIGHT - 45), Color.White);
                spriteBatch.DrawString(font01, "HI-SCORE " + GameplayScreen.hiscore.ToString("00000"), new Vector2(SpaceGame.SCREEN_WIDTH / 2 + 250, SpaceGame.SCREEN_HEIGHT - 45), Color.White);

                //DRAW TIME
                spriteBatch.DrawString(font01, "" + GameplayScreen.time.ToString("00"), new Vector2(SpaceGame.SCREEN_WIDTH / 2, 10), Color.White);
            }
        }
    }
}
