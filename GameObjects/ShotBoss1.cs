using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class ShotBoss1
    {
        Vector2 position;
        Vector2 position1;
        Vector2 speed;
        Vector2 speed1;
        Texture2D texture;
        Texture2D texture1;

        public Rectangle Bounds1 { get; private set; }
        public Rectangle Bounds2 { get; private set; }
        public bool IsActive { get; private set; }

        public ShotBoss1(Texture2D superShot)
        {
            this.texture = superShot;
            this.texture1 = superShot;
            position = Vector2.Zero;
            position1 = Vector2.Zero;
            speed = new Vector2(5, 1);
            speed1 = new Vector2(5, -1);
            IsActive = false;

            Bounds1 = new Rectangle((int)position.X - 30, (int)position.Y - 30, texture.Width, texture.Height);
            Bounds2 = new Rectangle((int)position1.X - 25, (int)position1.Y - 25, texture.Width, texture.Height);
        }

        public void Update()
        {
            if (IsActive)
            {
                position -= speed;
                position1 -= speed1;

                if (position.X >= SpaceGame.SCREEN_WIDTH)
                {
                    position = Vector2.Zero;
                    IsActive = false;
                }

                if (position1.X >= SpaceGame.SCREEN_WIDTH)
                {
                    position1 = Vector2.Zero;
                    IsActive = false;
                }

                Bounds1 = new Rectangle((int)position.X - 30, (int)position.Y - 30, texture.Width, texture.Height);
                Bounds2 = new Rectangle((int)position1.X - 25, (int)position1.Y - 25, texture.Width, texture.Height);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                sb.Draw(texture, position - new Vector2(30), Color.White);
                sb.Draw(texture1, position1 - new Vector2(25), Color.White);
            }

#if DEBUG
            //sb.Draw(SpaceGame.Pixel, Bounds1, Color.Red * 0.5f);
            //sb.Draw(SpaceGame.Pixel, Bounds2, Color.Red * 0.5f);
#endif
        }

        public void Fire(Vector2 pos)
        {
            position = pos;
            position1 = pos;
            IsActive = true;
        }

        public void Reset()
        {
            IsActive = false;
            position = new Vector2(0);
            position1 = new Vector2(0);
            Bounds1 = new Rectangle((int)position.X - 30, (int)position.Y - 30, texture.Width, texture.Height);
            Bounds2 = new Rectangle((int)position1.X - 25, (int)position1.Y - 25, texture.Width, texture.Height);
        }
    }
}
