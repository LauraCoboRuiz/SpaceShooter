using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SpaceShooter
{
    class GameplayScreen
    {
        SoundEffect sound;
        Song song;

        const int MAX_ENEMIES = 5;
        const int MAX_ENEMIES1 = 5;
        const int MAX_ENEMIES2 = 5;
        const int MAX_ENEMIES3 = 5;

        public static int score;
        public static int hiscore;
        public static int time;

        public static int bossTime;
        public static int currentBoss;

        SpriteFont font01;
        Texture2D back01;
        Texture2D back02;
        Texture2D back03;
        Texture2D back04;
        Texture2D back05;

        Player ship;
        Enemy[] enemies;
        EnemyType1[] enemies1;
        EnemyType2[] enemies2;
        EnemyType3[] enemies3;

        Vector2 scrolling;
        Vector2 scroll01;
        Vector2 scroll02;
        Vector2 scroll03;
        Vector2 scroll04;

        int framesCounter;
        int timeCounter;

        bool pause;

        public GameplayScreen(ContentManager content)
        {
            scrolling = Vector2.Zero;

            score = 0;
            hiscore = 0;

            scroll01 = Vector2.Zero;
            scroll02 = Vector2.Zero;
            scroll03 = Vector2.Zero;
            scroll04 = Vector2.Zero;

            score = 0;
            hiscore = 0;

            bossTime = 100;
            timeCounter = 0;

            pause = false;

            sound = content.Load<SoundEffect>("audio/Laser");
            song = content.Load<Song>("audio/Levi");

            //BACKGROUND
            font01 = content.Load<SpriteFont>("fonts/NewSpriteFont");
            back01 = content.Load<Texture2D>("graphics/backLvl101");
            back02 = content.Load<Texture2D>("graphics/backLvl102");
            back03 = content.Load<Texture2D>("graphics/backLvl103");
            back04 = content.Load<Texture2D>("graphics/backLvl104");
            back05 = content.Load<Texture2D>("graphics/backSmoke");

            //PLAYER
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

            framesCounter = 0;

            //ENEMY
            enemies = new Enemy[MAX_ENEMIES];

            for(int i = 0; i < MAX_ENEMIES; i++)
            {
                if(i / (MAX_ENEMIES / 2) == 0) enemies[i] = new Enemy(content.Load<Texture2D>("graphics/enemy001"),
                                                                       content.Load<Texture2D>("graphics/explosion_basic"),
                                                                       content.Load<Texture2D>("graphics/shot002"),
                                                                       content.Load<Texture2D>("graphics/barlife02"),
                                                                       content.Load<Texture2D>("graphics/barlife01"),
                                                                       content.Load<Texture2D>("graphics/barenemy"));

                else enemies[i] = new Enemy(content.Load<Texture2D>("graphics/enemy002"),
                                            content.Load<Texture2D>("graphics/explosion_basic"),
                                            content.Load<Texture2D>("graphics/shot002"),
                                            content.Load<Texture2D>("graphics/barlife02"),
                                            content.Load<Texture2D>("graphics/barlife01"),
                                            content.Load<Texture2D>("graphics/barenemy"));
            }

            //ENEMIES 1
            enemies1 = new EnemyType1[MAX_ENEMIES1];

            for(int i = 0; i < MAX_ENEMIES1; i++)
            {
                if(i / (MAX_ENEMIES1 / 2) == 0) enemies1[i] = new EnemyType1(content.Load<Texture2D>("graphics/enemy001"),
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

            for(int i = 0; i < MAX_ENEMIES2; i++)
            {
                if(i / (MAX_ENEMIES2 / 2) == 0) enemies2[i] = new EnemyType2(content.Load<Texture2D>("graphics/enemy001"),
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

            for(int i = 0; i < MAX_ENEMIES3; i++)
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

            if(framesCounter == 0) MediaPlayer.Play(song);

            if(input.IsKeyDown(Keys.P)) pause = true;
            if(input.IsKeyUp(Keys.P)) pause = false;

            if(!pause)
            {//TIMES
                timeCounter++;

                if(timeCounter == 60)
                {
                    time++;
                    bossTime--;
                    timeCounter = 0;
                    currentBoss = 0;
                }

                //UPDATE PLAYER
                ship.Update(input);

                //UPDATE ENEMIES
                if(time >= 5 && time < 35)
                {
                    for(int i = 0; i < MAX_ENEMIES; i++)
                    {
                        enemies[i].Update();
                    }

                    for(int i = 0; i < MAX_ENEMIES3; i++)
                    {
                        enemies3[i].Update();
                    }
                }

                if(time >= 35 && time < 50)
                {
                    for(int i = 0; i < MAX_ENEMIES1; i++)
                    {
                        sound.Play();
                    }
                    for(int i = 0; i < MAX_ENEMIES2; i++)
                    {
                        sound.Play();
                    }
                    for(int i = 0; i < MAX_ENEMIES3; i++)
                    {
                        sound.Play();
                    }
                }


                //BACKGROUND SCROLLING
                scroll01.X -= 6;

                if(scroll01.X <= -back02.Width) scroll01.X = 0;

                scroll02.X -= 2;

                if(scroll02.X <= -back03.Width) scroll02.X = 0;

                scroll03.X -= 4;

                if(scroll03.X <= -back04.Width) scroll03.X = 0;

                scroll04.X -= 1;

                if(scroll04.X <= -back05.Width) scroll04.X = 0;

                //COLLISIONS
                //PLAYER VS ENEMIES[]
                for(int i = 0; i < MAX_ENEMIES; i++)
                {
                    if(ship.CheckCollision(enemies[i].Bounds))
                    {
                        enemies[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                        sound.Play();
                    }
                }

                for(int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if(ship.CheckCollision(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                        sound.Play();
                    }
                }

                for(int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if(ship.CheckCollision(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                        sound.Play();
                    }
                }

                for(int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if(ship.CheckCollision(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(100);
                        ship.ReceiveDamage(50);
                        sound.Play();
                    }
                }

                //PLAYER.SHOTS[] VS ENEMIES[]
                for(int i = 0; i < MAX_ENEMIES; i++)
                {
                    if(ship.CheckCollisionShots(enemies[i].Bounds))
                    {
                        enemies[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES; i++)
                {
                    if(ship.CheckCollisionShots1(enemies[i].Bounds))
                    {
                        enemies[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES; i++)
                {
                    if(ship.CheckCollisionShots2(enemies[i].Bounds))
                    {
                        enemies[i].ReceiveDamage(300);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if(ship.CheckCollisionShots(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if(ship.CheckCollisionShots1(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if(ship.CheckCollisionShots2(enemies1[i].Bounds))
                    {
                        enemies1[i].ReceiveDamage(300);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if(ship.CheckCollisionShots(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if(ship.CheckCollisionShots1(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if(ship.CheckCollisionShots2(enemies2[i].Bounds))
                    {
                        enemies2[i].ReceiveDamage(300);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if(ship.CheckCollisionShots(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if(ship.CheckCollisionShots1(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(100);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                for(int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if(ship.CheckCollisionShots2(enemies3[i].Bounds))
                    {
                        enemies3[i].ReceiveDamage(300);
                        sound.Play();

                        score += 100;
                        if(score > hiscore) hiscore = score;
                    }
                }

                //PLAYER VS ENEMIES[].SHOTS[]
                for(int i = 0; i < MAX_ENEMIES; i++)
                {
                    if(enemies[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                        sound.Play();
                    }
                }

                for(int i = 0; i < MAX_ENEMIES1; i++)
                {
                    if(enemies1[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                        sound.Play();
                    }
                }

                for(int i = 0; i < MAX_ENEMIES2; i++)
                {
                    if(enemies2[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                        sound.Play();
                    }
                }

                for(int i = 0; i < MAX_ENEMIES3; i++)
                {
                    if(enemies3[i].CheckCollisionShots(ship.Bounds))
                    {
                        ship.ReceiveDamage(20);
                        sound.Play();
                    }
                }

                //BOSSTIME
                if(bossTime <= 0)
                {
                    MediaPlayer.Stop();
                    SpaceGame.CurrentScreen = GameScreen.Boss;
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

            if(pause)
            {
                spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black * 0.5f);

                if((framesCounter / 30) % 2 == 1)
                {
                    spriteBatch.DrawString(font01, "Paused", new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 75, SpaceGame.SCREEN_HEIGHT / 2 - 15), Color.Yellow);
                }
            }

            if(!pause)
            {
                //DRAW ENEMIES 
                if(time >= 5 && time < 35)
                {
                    for(int i = 0; i < MAX_ENEMIES; i++) enemies[i].Draw(spriteBatch);
                    for(int i = 0; i < MAX_ENEMIES3; i++) enemies3[i].Draw(spriteBatch);
                }

                if(time >= 35 && time < 50)
                {
                    for(int i = 0; i < MAX_ENEMIES1; i++) enemies1[i].Draw(spriteBatch);
                    for(int i = 0; i < MAX_ENEMIES2; i++) enemies2[i].Draw(spriteBatch);
                    for(int i = 0; i < MAX_ENEMIES3; i++) enemies3[i].Draw(spriteBatch);
                }

                //DRAW BARS
                spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, 50), Color.Black);
                spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, SpaceGame.SCREEN_HEIGHT - 50, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black);

                // Draw player
                ship.Draw(spriteBatch);

                //DRAW DANGER
                if(time <= 5)
                {
                    spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, SpaceGame.SCREEN_HEIGHT / 2 - 20, SpaceGame.SCREEN_WIDTH, 50), Color.Red * 0.5f);

                    if((framesCounter / 30) % 2 == 1)
                    {
                        spriteBatch.DrawString(font01, "DANGER!", new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 75, SpaceGame.SCREEN_HEIGHT / 2 - 15), Color.Yellow);
                    }
                }

                //DRAW TIME - SCORE - HISCORE
                spriteBatch.DrawString(font01, "SCORE " + score.ToString("00000"), new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 100, SpaceGame.SCREEN_HEIGHT - 45), Color.White);
                spriteBatch.DrawString(font01, "HI-SCORE " + hiscore.ToString("00000"), new Vector2(SpaceGame.SCREEN_WIDTH / 2 + 250, SpaceGame.SCREEN_HEIGHT - 45), Color.White);

                spriteBatch.DrawString(font01, "" + time.ToString("00"), new Vector2(SpaceGame.SCREEN_WIDTH / 2, 10), Color.White);
            }
        }
    }
}
