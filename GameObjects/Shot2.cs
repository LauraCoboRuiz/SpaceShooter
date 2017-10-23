using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    class Shot2
    {
        Vector2 position;
        Vector2 speed;
        Texture2D texture;

        public Rectangle Bounds { get; private set; }
        public bool IsActive { get; private set; }

        public Shot2(Texture2D texture)
        {
            this.texture = texture;
            position = Vector2.Zero;
            speed = new Vector2(2, 0);
            IsActive = false;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            if (IsActive)
            {
                position.X += speed.X;

                if (position.X > SpaceGame.SCREEN_WIDTH)
                {
                    position = Vector2.Zero;
                    IsActive = false;
                }

                Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                sb.Draw(texture, position + new Vector2(10, 0), Color.White);
            }

#if DEBUG
            //sb.Draw(SpaceGame.Pixel, Bounds, Color.Red * 0.5f);
#endif
        }

        public void Fire(Vector2 pos)
        {
            position = pos;
            IsActive = true;
        }

        public void Reset()
        {
            IsActive = false;
            position = new Vector2(0);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
