using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    enum BossState { Disabled = 0, Active, Explosion }

    class Boss
    {
        public Rectangle Bounds { get; private set; }
        public Rectangle Life { get; private set; }

        public static bool IsActive { get; private set; }

        const int MAX_LIFE = 500;
        const int MAX_SHOTS = 1000;
        const int MAX_SHOTS1 = 1000;
        const int MAX_SHOTS2 = 100;

        Vector2 position;
        Vector2 speed;

        Texture2D texture;
        Texture2D barS;
        Texture2D noLife;
        Texture2D bLife;

        int life;
        int shotCounter;
        int shotCounter1;
        int shotCounter2;
        int framesCounter;

        ShotBoss[] shots;
        ShotBoss1[] shots1;
        ShotBoss2[] shots2;
        Explosion explosion;

        BossState bState;

        public Boss(Texture2D texture, Texture2D texExplo, Texture2D texShot, Texture2D barboss, Texture2D barlife01, Texture2D barlife02, Texture2D superShot, Texture2D shot005)
        {
            this.texture = texture;
            position = new Vector2(SpaceGame.SCREEN_WIDTH - 10, SpaceGame.SCREEN_HEIGHT / 2 - texture.Height / 2 + 50);

            Bounds = new Rectangle((int)position.X - 10, (int)position.Y, texture.Width, texture.Height);
            Life = new Rectangle(170, 10, texture.Width, 10);

            speed = new Vector2(2, 2);
            life = 500;

            barS = barboss;
            noLife = barlife01;
            bLife = barlife02;

            IsActive = false;

            shots = new ShotBoss[MAX_SHOTS];
            for (int i = 0; i < MAX_SHOTS; i++) shots[i] = new ShotBoss(texShot);

            shots1 = new ShotBoss1[MAX_SHOTS1];
            for (int i = 0; i < MAX_SHOTS1; i++) shots1[i] = new ShotBoss1(superShot);

            shots2 = new ShotBoss2[MAX_SHOTS2];
            for (int i = 0; i < MAX_SHOTS2; i++) shots2[i] = new ShotBoss2(shot005);

            explosion = new Explosion(texExplo, 5, 1);

            bState = BossState.Active;
        }

        public void Update()
        {
            switch (bState)
            {
                case BossState.Disabled:
                    break;
                case BossState.Active:
                    {
                        framesCounter++;
                        IsActive = true;
                        position.X -= speed.X;

                        if (position.X <= SpaceGame.SCREEN_WIDTH / 2 + texture.Width)
                        {
                            position.X = SpaceGame.SCREEN_WIDTH / 2 + texture.Width;

                            if ((framesCounter / 130) % 2 == 1)
                            {
                                position.Y = position.Y - speed.Y;
                            }
                            else
                            {
                                position.Y = position.Y + speed.Y;
                            }

                            //SHOT
                            shotCounter++;

                            if (shotCounter >= 10)
                            {
                                for (int i = 0; i < MAX_SHOTS; i++)
                                {
                                    if (!shots[i].IsActive)
                                    {
                                        shots[i].Fire(position + new Vector2(100, texture.Height / 2 - 5));

                                        shotCounter = 0;

                                        break;
                                    }
                                }
                            }

                            shotCounter1++;

                            if (shotCounter1 >= 40)
                            {
                                for (int i = 0; i < MAX_SHOTS1; i++)
                                {
                                    if (!shots1[i].IsActive)
                                    {
                                        shots1[i].Fire(position + new Vector2(100, texture.Height / 2 - 5));

                                        shotCounter1 = 0;

                                        break;
                                    }
                                }
                            }


                            shotCounter2++;

                            if (shotCounter2 >= 80)
                            {
                                for (int i = 0; i < MAX_SHOTS2; i++)
                                {
                                    if (!shots2[i].IsActive)
                                    {
                                        shots2[i].Fire(position + new Vector2(100, texture.Height / 2 - 5));

                                        shotCounter2 = 0;

                                        break;
                                    }
                                }
                            }

                            for (int i = 0; i < MAX_SHOTS; i++) shots[i].Update();
                            for (int i = 0; i < MAX_SHOTS1; i++) shots1[i].Update();
                            for (int i = 0; i < MAX_SHOTS2; i++) shots2[i].Update();
                        }
                        Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                        Life = new Rectangle(170, 10, texture.Width, 10);
                    }
                    break;
                case BossState.Explosion:
                    {
                        explosion.Update();

                        if (!explosion.IsActive) bState = BossState.Disabled;

                        IsActive = false;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            switch (bState)
            {
                case BossState.Disabled:
                    break;
                case BossState.Active:
                    {
                        //DRAW LIFE
                        sb.Draw(barS, new Vector2(SpaceGame.SCREEN_WIDTH - barS.Width, 0), Color.White);

                        sb.Draw(bLife, new Rectangle(800, -4, MAX_LIFE, 50), Color.White);
                        sb.Draw(noLife, new Rectangle(800, -4, life, 50), Color.White);

                        //DRAW SHOTS
                        for (int i = 0; i < MAX_SHOTS; i++) shots[i].Draw(sb);
                        for (int i = 0; i < MAX_SHOTS1; i++) shots1[i].Draw(sb);
                        for (int i = 0; i < MAX_SHOTS2; i++) shots2[i].Draw(sb);

                        //DRAW BOSS
                        sb.Draw(texture, position, Color.White);
                        //BOUNDS: sb.Draw(SpaceGame.Pixel, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), Color.Red * 0.5f);
                    }
                    break;
                case BossState.Explosion:
                    {
                        explosion.Draw(sb);
                    }
                    break;
                default:
                    break;
            }
        }

        public void ReceiveDamage(int damage)
        {
            life -= damage;

            if (life <= 0)
            {
                life = 0;
                explosion.Explode(position);

                SpaceGame.CurrentScreen = GameScreen.Win;
            }
        }

        public bool CheckCollisionShots(Rectangle bounds)
        {
            bool collision = false;

            //COLLISION BOSS SHOTS
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (shots[i].IsActive && shots[i].Bounds1.Intersects(bounds))
                {
                    collision = true;
                    shots[i].Reset();
                    break;
                }
            }

            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (shots[i].IsActive && shots[i].Bounds2.Intersects(bounds))
                {
                    collision = true;
                    shots[i].Reset();
                    break;
                }
            }

            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (shots[i].IsActive && shots[i].Bounds3.Intersects(bounds))
                {
                    collision = true;
                    shots[i].Reset();
                    break;
                }
            }

            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (shots[i].IsActive && shots[i].Bounds4.Intersects(bounds))
                {
                    collision = true;
                    shots[i].Reset();
                    break;
                }
            }

            return collision;
        }

        public bool CheckCollisionShots1(Rectangle bounds)
        {
            bool collision1 = false;

            //COLLISION BOSS SHOTS
            for (int i = 0; i < MAX_SHOTS1; i++)
            {
                if (shots1[i].IsActive && shots1[i].Bounds1.Intersects(bounds))
                {
                    collision1 = true;
                    shots1[i].Reset();
                    break;
                }
            }

            for (int i = 0; i < MAX_SHOTS1; i++)
            {
                if (shots1[i].IsActive && shots1[i].Bounds2.Intersects(bounds))
                {
                    collision1 = true;
                    shots1[i].Reset();
                    break;
                }
            }

            return collision1;
        }

        public bool CheckCollisionShots2(Rectangle bounds)
        {
            bool collision2 = false;

            //COLLISION BOSS SHOTS
            for (int i = 0; i < MAX_SHOTS2; i++)
            {
                if (shots2[i].IsActive && shots2[i].Bounds.Intersects(bounds))
                {
                    collision2 = true;
                    shots2[i].Reset();
                    break;
                }
            }
            return collision2;
        }
    }
}
