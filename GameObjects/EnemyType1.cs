using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    enum Enemy1State { Disabled = 0, Active, Explosion }

    class EnemyType1
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

        ShotsEnemy[] shots;

        Explosion explosion;

        Enemy1State state;

        public EnemyType1(Texture2D texture, Texture2D texExplo, Texture2D texShot, Texture2D barlife01, Texture2D barlife02, Texture2D barenemy)
        {
            this.texture = texture;
            position = new Vector2(100, 0);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            speed = new Vector2(5, 3);
            life = 200;
            shotCounter = 0;

            bLife = barlife01;
            bNoLife = barlife02;
            bStruct = barenemy;

            explosion = new Explosion(texExplo, 5, 1);

            shots = new ShotsEnemy[MAX_SHOTS];

            for (int i = 0; i < MAX_SHOTS; i++) shots[i] = new ShotsEnemy(texShot);

            state = Enemy1State.Active;
        }

        public void Update()
        {
            switch (state)
            {
                case Enemy1State.Disabled: Reset(); break;
                case Enemy1State.Active:
                    {
                        position += speed;

                        if ((position.Y + texture.Height) > SpaceGame.SCREEN_HEIGHT)
                        {
                            position = new Vector2(100, 0);
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
                                    shots[i].Fire(position + new Vector2(100, texture.Height / 2 - 5));

                                    shotCounter = 0;

                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < MAX_SHOTS; i++) shots[i].Update();

                        Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    }
                    break;
                case Enemy1State.Explosion:
                    {
                        explosion.Update();

                        if (!explosion.IsActive) state = Enemy1State.Active;
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
                case Enemy1State.Disabled: break;
                case Enemy1State.Active:
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
                case Enemy1State.Explosion:
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
                state = Enemy1State.Explosion;
                explosion.Explode(position);

                Reset();
            }
        }

        public void Reset()
        {
            position = new Vector2(100, 0);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            life = MAX_LIFE;

            for (int i = 0; i < MAX_SHOTS; i++) shots[i].Reset();

            shotCounter++;
        }
    }
}
