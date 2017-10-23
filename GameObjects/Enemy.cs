using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    enum EnemyState { Disabled = 0, Active, Explosion }

    class Enemy
    {
        const int MAX_LIFE = 200;
        const int MAX_SHOTS = 20;

        Vector2 position;
        Vector2 speed;

        Texture2D texture;
        Texture2D bLife;
        Texture2D bNoLife;
        Texture2D bStruct;

        public Rectangle Bounds { get; private set; }

        int life;
        int shotCounter;

        ShotsEnemy[] shots;

        Explosion explosion;

        EnemyState state;

        public Enemy(Texture2D texture, Texture2D texExplo, Texture2D texShot, Texture2D barlife01, Texture2D barlife02, Texture2D barenemy)
        {
            this.texture = texture;
            position = new Vector2(SpaceGame.Random.Next(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_WIDTH * 2),
                                   SpaceGame.Random.Next(0 + texture.Height + 50, SpaceGame.SCREEN_HEIGHT - texture.Height - 50));
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            speed = new Vector2(-10, 0);
            life = 200;
            shotCounter = 0;

            bLife = barlife01;
            bNoLife = barlife02;
            bStruct = barenemy;

            explosion = new Explosion(texExplo, 5, 1);

            shots = new ShotsEnemy[MAX_SHOTS];

            for(int i = 0; i < MAX_SHOTS; i++) shots[i] = new ShotsEnemy(texShot);

            state = EnemyState.Active;
        }

        public void Update()
        {
            switch(state)
            {
                case EnemyState.Disabled: Reset(); break;
                case EnemyState.Active:
                    {
                        position += speed;

                        if((position.X + texture.Width) < 0)
                        {
                            position = new Vector2(SpaceGame.Random.Next(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_WIDTH * 2),
                                                   SpaceGame.Random.Next(0 + texture.Height + 50, SpaceGame.SCREEN_HEIGHT - texture.Height - 50));
                            Reset();
                        }

                        //SHOT
                        shotCounter++;

                        if(shotCounter >= 10)
                        {
                            for(int i = 0; i < MAX_SHOTS; i++)
                            {
                                if(!shots[i].IsActive)
                                {
                                    shots[i].Fire(position + new Vector2(100, texture.Height / 2 - 5));

                                    shotCounter = 0;

                                    break;
                                }
                            }
                        }
                        for(int i = 0; i < MAX_SHOTS; i++) shots[i].Update();

                        Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    }
                    break;
                case EnemyState.Explosion:
                    {
                        explosion.Update();

                        if(!explosion.IsActive) state = EnemyState.Active;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            switch(state)
            {
                case EnemyState.Disabled: break;
                case EnemyState.Active:
                    {
                        //DRAW SHOTS
                        for(int i = 0; i < MAX_SHOTS; i++) shots[i].Draw(sb);

                        //DRAW ENEMY
                        sb.Draw(texture, position, Color.White);

                        //DRAW LIFE
                        sb.Draw(bStruct, new Vector2((int)position.X, (int)position.Y - 20), Color.White);

                        sb.Draw(bLife, new Rectangle((int)position.X, (int)position.Y - 20, MAX_LIFE, 50), Color.White);
                        sb.Draw(bNoLife, new Rectangle((int)position.X, (int)position.Y - 20, life, 50), Color.White);
                    }
                    break;
                case EnemyState.Explosion:
                    {
                        explosion.Draw(sb);
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
            for(int i = 0; i < MAX_SHOTS; i++)
            {
                if((shots[i].IsActive) && shots[i].Bounds.Intersects(bounds))
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

            if(life <= 0)
            {
                life = 0;
                state = EnemyState.Explosion;
                explosion.Explode(position);

                Reset();
            }
        }

        public void Reset()
        {
            position = new Vector2(SpaceGame.Random.Next(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_WIDTH * 2),
                                   SpaceGame.Random.Next(0 + texture.Height + 50, SpaceGame.SCREEN_HEIGHT - texture.Height - 50));
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            life = MAX_LIFE;

            for(int i = 0; i < MAX_SHOTS; i++) shots[i].Reset();

            shotCounter++;
        }
    }
}

