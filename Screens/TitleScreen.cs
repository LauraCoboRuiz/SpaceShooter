using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceShooter
{
    class TitleScreen
    {
        Texture2D background;
        Texture2D title01;
        Texture2D title02;
        Texture2D buttonStart;
        Texture2D buttonControl;

        Song songTitle;

        Rectangle buttonFrame;
        Vector2 buttonPosition;
        Rectangle buttonRec;

        Rectangle buttonFrame1;
        Vector2 buttonPosition1;
        Rectangle buttonRec1;

        MouseState mouse;
        MouseState prevMouse;

        int mouseX;
        int mouseY;

        int mouseCounter;

        SpriteFont font01;

        int framesCounter;
        int elementPosition01Y;
        int elementPosition02Y;
        int speedY;

        int fadeCounter;
        float fade;

        bool control;

        public TitleScreen(ContentManager content)
        {
            background = content.Load<Texture2D>("graphics/backTitle");
            title01 = content.Load<Texture2D>("graphics/title01");
            title02 = content.Load<Texture2D>("graphics/title02");
            font01 = content.Load<SpriteFont>("fonts/NewSpriteFont");
            buttonStart = content.Load<Texture2D>("graphics/buttonStart");
            buttonControl = content.Load<Texture2D>("graphics/button_Controls");
            songTitle = content.Load<Song>("audio/spacediver");

            control = false;

            speedY = 5;

            //START
            buttonFrame = new Rectangle(0, 0, buttonStart.Width, buttonStart.Height / 4);
            buttonPosition = new Vector2(20, SpaceGame.SCREEN_HEIGHT / 2 - buttonStart.Height + 150);
            buttonRec = new Rectangle(20, SpaceGame.SCREEN_HEIGHT / 2 - buttonStart.Height + 150, buttonStart.Width, buttonStart.Height / 4);

            //CONTROL
            buttonFrame1 = new Rectangle(0, 0, buttonControl.Width, buttonControl.Height / 4);
            buttonPosition1 = new Vector2(20, SpaceGame.SCREEN_HEIGHT / 2 - buttonControl.Height + 150 + buttonControl.Height / 2);
            buttonRec1 = new Rectangle(20, SpaceGame.SCREEN_HEIGHT / 2 - buttonControl.Height + 150 + buttonControl.Height / 2, buttonControl.Width, buttonControl.Height / 4);

            framesCounter = 0;
            elementPosition01Y = 0 - title01.Height;
            elementPosition02Y = 0 - title02.Height;

            fadeCounter = 0;
            fade = 1.0f;
        }

        public void Update(InputManager input)
        {
            fadeCounter++;

            if(framesCounter == 0) MediaPlayer.Play(songTitle);

            if(fadeCounter < 180)
            {
                fade -= 1.0f / 90.0f;
            }

            if(fade <= 0.0f)
            {
                fade = 0.0f;
                fadeCounter = 0;
                framesCounter++;

                if(elementPosition01Y < SpaceGame.SCREEN_HEIGHT) elementPosition01Y += speedY;

                if(elementPosition01Y == SpaceGame.SCREEN_WIDTH / 2 - title01.Width / 2 - 150) speedY = 0;

                if(elementPosition02Y < SpaceGame.SCREEN_HEIGHT) elementPosition02Y += speedY;

                if(elementPosition02Y == SpaceGame.SCREEN_WIDTH / 2 - title02.Width / 2 - 150 - title01.Width - title01.Width / 2) speedY = 0;
            }

            mouse = Mouse.GetState();
            prevMouse = Mouse.GetState();

            mouseX = mouse.X;
            mouseY = mouse.Y;

            //START
            if(buttonRec.Contains(mouseX, mouseY))
            {
                buttonFrame.Y = 2 * buttonStart.Height / 4;

                if(mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    buttonFrame.Y = buttonStart.Height / 4;

                    //AudioManager.PlaySound(Fx.PressStart);

                    mouseCounter++;

                    if(mouseCounter >= 5)
                    {
                        buttonFrame.Y = buttonStart.Height / 4;

                        MediaPlayer.Stop();
                        SpaceGame.CurrentScreen = GameScreen.Gameplay;

                    }
                }
            }
            else buttonFrame.Y = 0;

            //CONTROLS
            if(buttonRec1.Contains(mouseX, mouseY))
            {
                buttonFrame1.Y = 2 * buttonControl.Height / 4;

                if(mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    buttonFrame1.Y = buttonControl.Height / 4;

                    //AudioManager.PlaySound(Fx.PressStart);

                    mouseCounter++;

                    if(mouseCounter >= 5)
                    {
                        buttonFrame1.Y = buttonControl.Height / 4;

                        control = true;
                    }
                }
            }
            else buttonFrame1.Y = 0;

            if(input.IsKeyPressed(Keys.B)) control = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            spriteBatch.Draw(title01, new Vector2(SpaceGame.SCREEN_WIDTH / 2 - title01.Width, elementPosition01Y), Color.White);
            spriteBatch.Draw(title02, new Vector2(SpaceGame.SCREEN_WIDTH / 2, elementPosition02Y), Color.White);

            spriteBatch.Draw(buttonStart, buttonPosition, buttonFrame, Color.White);
            spriteBatch.Draw(buttonControl, buttonPosition1, buttonFrame1, Color.White);

            spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black * fade);

            if(control)
            {
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black * 0.5f);

                spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(100, 30, SpaceGame.SCREEN_WIDTH - 200, SpaceGame.SCREEN_HEIGHT - 60), Color.Black * 0.5f);

                spriteBatch.DrawString(font01, "CONTROLS", new Vector2(SpaceGame.SCREEN_WIDTH / 2 - 50, 35), Color.Yellow);
                spriteBatch.DrawString(font01, "LEFT SHIFT      to run", new Vector2(105, 105), Color.Yellow);
                spriteBatch.DrawString(font01, "W       shot up", new Vector2(105, 205), Color.Yellow);
                spriteBatch.DrawString(font01, "S       super shot", new Vector2(105, 305), Color.Yellow);
                spriteBatch.DrawString(font01, "D       normal shot", new Vector2(105, 405), Color.Yellow);
                spriteBatch.DrawString(font01, "Press B to return", new Vector2(105, SpaceGame.SCREEN_HEIGHT - 55), Color.Yellow);
            }
        }
    }
}