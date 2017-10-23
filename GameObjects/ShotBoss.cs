using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class ShotBoss
    {
        Vector2 position;
        Vector2 speed;
        Texture2D texture;

        public Rectangle Bounds1 { get; private set; }
        public Rectangle Bounds2 { get; private set; }
        public Rectangle Bounds3 { get; private set; }
        public Rectangle Bounds4 { get; private set; }

        public bool IsActive { get; private set; }

        public ShotBoss(Texture2D texture)
        {
            this.texture = texture;
            position = Vector2.Zero;
            speed = new Vector2(25, 0);
            IsActive = false;

            Bounds1 = new Rectangle((int)position.X - 100, (int)position.Y - 100, texture.Width, texture.Height);
            Bounds2 = new Rectangle((int)position.X - 105, (int)position.Y - 105, texture.Width, texture.Height);

            Bounds3 = new Rectangle((int)position.X + 100, (int)position.Y + 100, texture.Width, texture.Height);
            Bounds4 = new Rectangle((int)position.X + 105, (int)position.Y + 105, texture.Width, texture.Height);
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

                Bounds1 = new Rectangle((int)position.X - 100, (int)position.Y - 100, texture.Width, texture.Height);
                Bounds2 = new Rectangle((int)position.X - 105, (int)position.Y - 105, texture.Width, texture.Height);

                Bounds3 = new Rectangle((int)position.X + 100, (int)position.Y + 100, texture.Width, texture.Height);
                Bounds4 = new Rectangle((int)position.X + 105, (int)position.Y + 105, texture.Width, texture.Height);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                sb.Draw(texture, position - new Vector2(100), Color.White);
                sb.Draw(texture, position - new Vector2(105), Color.White);

                sb.Draw(texture, position + new Vector2(100), Color.White);
                sb.Draw(texture, position + new Vector2(105), Color.White);

                //BOUNDS
                /*
                sb.Draw(SpaceGame.Pixel, Bounds1, Color.Red * 0.5f);
                sb.Draw(SpaceGame.Pixel, Bounds2, Color.Red * 0.5f);
                sb.Draw(SpaceGame.Pixel, Bounds3, Color.Red * 0.5f);
                sb.Draw(SpaceGame.Pixel, Bounds4, Color.Red * 0.5f);
                */
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
            Bounds1 = new Rectangle((int)position.X - 100, (int)position.Y - 100, texture.Width, texture.Height);
            Bounds2 = new Rectangle((int)position.X - 105, (int)position.Y - 105, texture.Width, texture.Height);

            Bounds3 = new Rectangle((int)position.X + 100, (int)position.Y + 100, texture.Width, texture.Height);
            Bounds4 = new Rectangle((int)position.X + 105, (int)position.Y + 105, texture.Width, texture.Height);
        }
    }
}