using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class ShotBoss2
    {
        Vector2 position;
        Vector2 speed;
        Texture2D texture;

        public Rectangle Bounds { get; private set; }
        public bool IsActive { get; private set; }

        public ShotBoss2(Texture2D superShot)
        {
            this.texture = superShot;
            position = Vector2.Zero;
            speed = new Vector2(SpaceGame.Random.Next(2, 7), 0);
            IsActive = false;

            Bounds = new Rectangle((int)position.X - 30, (int)position.Y - 30, texture.Width, texture.Height);
        }

        public void Update()
        {
            if (IsActive)
            {
                position -= speed;

                if (speed.X == 0)
                {
                    speed.X = 0;
                    IsActive = false;
                }

                if (position.X >= SpaceGame.SCREEN_WIDTH)
                {
                    position = Vector2.Zero;
                    IsActive = false;
                }

                Bounds = new Rectangle((int)position.X - 30, (int)position.Y - 30, texture.Width, texture.Height);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                sb.Draw(texture, position - new Vector2(30), Color.White);
            }

#if DEBUG
            //sb.Draw(SpaceGame.Pixel, Bounds1, Color.Red * 0.5f);
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
            Bounds = new Rectangle((int)position.X - 30, (int)position.Y - 30, texture.Width, texture.Height);
        }
    }
}
