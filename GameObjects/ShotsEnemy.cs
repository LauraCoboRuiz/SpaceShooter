using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class ShotsEnemy
    {
        Vector2 position;
        Vector2 speed;
        Texture2D texture;

        public Rectangle Bounds { get; private set; }
        public bool IsActive { get; private set; }

        public ShotsEnemy(Texture2D texture)
        {
            this.texture = texture;
            position = Vector2.Zero;
            speed = new Vector2(25, 0);
            IsActive = false;

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            if (IsActive)
            {
                position -= speed;

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
                sb.Draw(texture, position - new Vector2(30), Color.White);
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
