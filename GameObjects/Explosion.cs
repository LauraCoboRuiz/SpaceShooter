using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Explosion
    {
        Vector2 position;
        Vector2 speed;
        Texture2D texture;
        Rectangle source;
        Rectangle bounds;

        int framesCounter;
        int currentFrame;
        int framesCountX;
        int framesLinesY;

        public bool IsActive { get; private set; }

        public Explosion(Texture2D texture, int framesX, int linesY)
        {
            this.texture = texture;
            position = Vector2.Zero;
            speed = new Vector2(10, 0);
            IsActive = false;

            framesCounter = 0;
            currentFrame = 0;
            framesCountX = framesX;
            framesLinesY = linesY;

            source = new Rectangle(currentFrame*(texture.Width / framesCountX), 0, texture.Width / framesCountX, texture.Height / linesY);
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width/ framesCountX, texture.Height/ framesLinesY);
        }

        public void Update()
        {
            if (IsActive)
            {
                framesCounter++;

                if (framesCounter >= 10)
                {
                    framesCounter = 0;
                    currentFrame++;

                    if (currentFrame >= framesCountX)
                    {
                        currentFrame = 0;
                        IsActive = false;

                        // TODO: Consider multiple frames lines in the texture
                    }

                    source.X = currentFrame * (texture.Width / framesCountX);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                sb.Draw(texture, position, source, Color.White);
            }
        }

        public void Explode(Vector2 pos)
        {
            position = pos;
            IsActive = true;
        }
    }
}
