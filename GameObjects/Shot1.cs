using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Shot1
    {
        Vector2 position;
        Vector2 speed;
        Texture2D texture;

        public Rectangle Bounds { get; private set; }
        public bool IsActive { get; private set; }

        public Shot1(Texture2D superShot)
        {
            this.texture = superShot;
            position = Vector2.Zero;
            speed = new Vector2(0, 10);
            IsActive = false;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            if (IsActive)
            {
                position.Y -= speed.Y;

                if (position.Y <= 0)
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
