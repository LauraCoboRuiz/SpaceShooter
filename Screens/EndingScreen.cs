using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceShooter
{
    class EndingScreen
    {
        Texture2D background;
        SpriteFont font01;
        SpriteFont font02;

        Vector2 position;

        int elementPositionX;
        int framesCounter;
        int fadeCounter;
        float fade;

        public EndingScreen(ContentManager content)
        {
            background = content.Load<Texture2D>("graphics/backEndingWin");
            font01 = content.Load<SpriteFont>("fonts/Sprite");
            font02 = content.Load<SpriteFont>("fonts/NewSpriteFont");

            framesCounter = 0;

            elementPositionX = 5;
            position = new Vector2(SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT / 2 - 100);
        }

        public void Update(InputManager input)
        {
            fadeCounter++;

            if (fadeCounter <= 180)
            {
                fade -= 1.0f / 90.0f;
            }

            if (fade <= 0.0f)
            {
                framesCounter++;
                fade = 0.0f;

                position.X -= elementPositionX;

                if (position.X <= SpaceGame.SCREEN_WIDTH / 2 - 300)
                {
                    position.X = SpaceGame.SCREEN_WIDTH / 2 - 300;
                }
            }
            if (input.IsKeyDown(Keys.Enter))
            {
                if (fadeCounter >= 390)
                {
                    MediaPlayer.Stop();
                    SpaceGame.CurrentScreen = GameScreen.Title;
                }
            }  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            if ((framesCounter / 30) % 2 == 1)
            {
                spriteBatch.DrawString(font02, "Press ENTER to menu", new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 175, SpaceGame.SCREEN_HEIGHT - 50), Color.White);
            }

            spriteBatch.DrawString(font01, "YOU WIN!", position, Color.White);

            spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black * fade);
        }
    }
}
