using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace SpaceShooter
{
    class LogoScreen
    {
        Texture2D background;
        Texture2D logo;

        SoundEffect logoSound;

        int framesCounter;
        float alpha;

        int fadeCounter;
        float fade;

        public LogoScreen(ContentManager content)
        {
            background = content.Load<Texture2D>("graphics/backLvl101");
            logo = content.Load<Texture2D>("graphics/logo");
            logoSound = content.Load<SoundEffect>("audio/logo");

            framesCounter = 0;
            alpha = 0.0f;

            fadeCounter = 0;
            fade = 1.0f;
        }

        public void Update(InputManager input)
        {
            if(input.IsKeyDown(Keys.Space))
            {
                SpaceGame.CurrentScreen = GameScreen.Title;
                //AudioManager.PlayMusic(Track.Track02);
            }

            //LOGO ANIM
            fadeCounter++;

            if(fadeCounter < 180)
            {
                fade -= 1.0f / 90.0f;
            }

            if(fadeCounter == 180)
            {
                logoSound.Play();
                //AudioManager.PlaySound(Fx.LogoAppear);
            }

            if(fade <= 0.0f)
            {
                fade = 0.0f;
                framesCounter++;

                if(framesCounter < 180) alpha += 1.0f / 90.0f;

                if(alpha >= 1.0f)
                {
                    alpha = 1.0f;
                }

                if(framesCounter >= 180) framesCounter = 180;

                if(framesCounter == 180) alpha -= 1.0f / 90.0f;
            }

            if(fadeCounter >= 390)
            {
                fadeCounter = 390;
                fade += 1.0f / 90.0f;
            }

            if(fade >= 1.0f)
            {
                SpaceGame.CurrentScreen = GameScreen.Title;
                //AudioManager.PlayMusic(Track.Track02);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(logo, new Vector2(SpaceGame.SCREEN_WIDTH / 2 - logo.Width / 2, SpaceGame.SCREEN_HEIGHT / 2 - logo.Height / 2), Color.White * alpha);

            spriteBatch.Draw(SpaceGame.Pixel, new Rectangle(0, 0, SpaceGame.SCREEN_WIDTH, SpaceGame.SCREEN_HEIGHT), Color.Black * fade);
        }
    }
}