using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    enum RockState { Disabled = 0, Active, Explosion }

    class Rock
    {
        const int MAX_LIFE = 1;

        Vector2 position;
        Vector2 speed;
        Texture2D texture;

        public Rectangle Bounds { get; private set; }
        public bool IsActive { get; private set; }

        int life;

        RockState state;

        Explosion explosion;

        public Rock(Texture2D texture, Texture2D texExplo)
        {
            this.texture = texture;
            position = new Vector2(SpaceGame.Random.Next(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_WIDTH * 2),
                                   SpaceGame.Random.Next(0 + texture.Height + 50, SpaceGame.SCREEN_HEIGHT - texture.Height - 50));
            speed = new Vector2(2, 0);
            IsActive = false;
            life = 1;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            explosion = new Explosion(texExplo, 5, 1);

            state = RockState.Active;
        }

        public void Update()
        {
            switch (state)
            {
                case RockState.Disabled: Reset(); break;
                case RockState.Active:
                    {
                        if (IsActive)
                        {
                            position.X -= speed.X;

                            if (position.X > SpaceGame.SCREEN_WIDTH)
                            {
                                position = new Vector2(SpaceGame.Random.Next(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_WIDTH * 2),
                                   SpaceGame.Random.Next(0 + texture.Height + 50, SpaceGame.SCREEN_HEIGHT - texture.Height - 50));
                                IsActive = false;
                            }
                            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                        }
                    }
                    break;
                case RockState.Explosion:
                    {
                        explosion.Update();

                        if (!explosion.IsActive) state = RockState.Active;
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
                case RockState.Disabled: break;
                case RockState.Active:
                    {
                        //DRAW ENEMY
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                    break;
                case RockState.Explosion:
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

        public void ReceiveDamage(int damage)
        {
            life -= damage;

            if (life <= 0)
            {
                life = 0;
                state = RockState.Explosion;
                explosion.Explode(position);

                Reset();
            }
        }

        public void Reset()
        {
            IsActive = false;
            position = new Vector2(SpaceGame.Random.Next(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_WIDTH * 2),
                                   SpaceGame.Random.Next(0 + texture.Height + 50, SpaceGame.SCREEN_HEIGHT - texture.Height - 50));
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            life = MAX_LIFE;
        }
    }
}
