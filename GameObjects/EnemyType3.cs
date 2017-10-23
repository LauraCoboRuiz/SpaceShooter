using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    enum Enemy3State { Disabled = 0, Active, Explosion }

    class EnemyType3
    {
        const int MAX_LIFE = 200;
        const int MAX_SHOTS = 20;

        public Rectangle Bounds { get; private set; }

        Vector2 position;
        Vector2 speed;

        Texture2D texture;
        Texture2D bLife;
        Texture2D bNoLife;
        Texture2D bStruct;

        int life;
        int shotCounter;

        ShotEnemy1[] shots;

        Explosion explosion;

        Enemy2State state;

        public EnemyType3(Texture2D texture, Texture2D texExplo, Texture2D texShot, Texture2D barlife01, Texture2D barlife02, Texture2D barenemy)
        {
            this.texture = texture;
            position = new Vector2(0, 50);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            speed = new Vector2(5, 0);
            life = 200;
            shotCounter = 0;

            bLife = barlife01;
            bNoLife = barlife02;
            bStruct = barenemy;

            explosion = new Explosion(texExplo, 5, 1);

            shots = new ShotEnemy1[MAX_SHOTS];

            for (int i = 0; i < MAX_SHOTS; i++) shots[i] = new ShotEnemy1(texShot);

            state = Enemy2State.Active;
        }

        public void Update()
        {
            switch (state)
            {
                case Enemy2State.Disabled: Reset(); break;
                case Enemy2State.Active:
                    {
                        position.X += speed.X;

                        if ((position.X) > SpaceGame.SCREEN_WIDTH - texture.Width)
                        {
                            position = new Vector2(0, 50);
                            Reset();
                        }

                        //SHOT
                        shotCounter++;

                        if (shotCounter >= 10)
                        {
                            for (int i = 0; i < MAX_SHOTS; i++)
                            {
                                if (!shots[i].IsActive)
                                {
                                    shots[i].Fire(position);

                                    shotCounter = 0;

                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < MAX_SHOTS; i++) shots[i].Update();

                        Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    }
                    break;
                case Enemy2State.Explosion:
                    {
                        explosion.Update();

                        if (!explosion.IsActive) state = Enemy2State.Active;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case Enemy2State.Disabled: break;
                case Enemy2State.Active:
                    {
                        //DRAW SHOTS
                        for (int i = 0; i < MAX_SHOTS; i++) shots[i].Draw(spriteBatch);

                        //DRAW ENEMY
                        spriteBatch.Draw(texture, position, Color.White);

                        //DRAW LIFE
                        spriteBatch.Draw(bStruct, new Vector2((int)position.X, (int)position.Y - 20), Color.White);

                        spriteBatch.Draw(bLife, new Rectangle((int)position.X, (int)position.Y - 20, MAX_LIFE, 50), Color.White);
                        spriteBatch.Draw(bNoLife, new Rectangle((int)position.X, (int)position.Y - 20, life, 50), Color.White);
                    }
                    break;
                case Enemy2State.Explosion:
                    {
                        explosion.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }

#if DEBUG
            //sb.Draw(SpaceGame.Pixel, Bounds, Color.Red * 0.5f);
#endif
        }

        public bool CheckCollisionShots(Rectangle bounds)
        {
            bool collision = false;

            // TODO: Check collision with enemy shots
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if ((shots[i].IsActive) && shots[i].Bounds.Intersects(bounds))
                {
                    collision = true;
                    shots[i].Reset();

                    break;
                }
            }

            return collision;
        }

        public void ReceiveDamage(int damage)
        {
            life -= damage;

            if (life <= 0)
            {
                life = 0;
                state = Enemy2State.Explosion;
                explosion.Explode(position);

                Reset();
            }
        }

        public void Reset()
        {
            position = new Vector2(0, 50);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            life = MAX_LIFE;

            for (int i = 0; i < MAX_SHOTS; i++) shots[i].Reset();

            shotCounter++;
        }
    }
}
