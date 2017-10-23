using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceShooter
{
    enum GameScreen { Logo, Title, Gameplay, Win, Lose, Boss }

    class SpaceGame : Game
    {
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;

        public static Random Random { get; private set; }
        public static Texture2D Pixel { get; private set; }
        public static GameScreen CurrentScreen { private get; set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        LogoScreen logoScreen;
        TitleScreen titleScreen;
        GameplayScreen gameplayScreen;
        BossScreen bossScreen;
        EndingScreen endingScreen;
        EndingScreen2 endScreen;

        InputManager input;

        public SpaceGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Random = new Random();

            IsMouseVisible = true;
            Window.Title = "SUPER SPACE SHOOTER";
            Content.RootDirectory = "Content";

            CurrentScreen = GameScreen.Logo;
        }

        protected override void Initialize()
        {
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();

            input = new InputManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            logoScreen = new LogoScreen(Content);
            titleScreen = new TitleScreen(Content);
            gameplayScreen = new GameplayScreen(Content);
            bossScreen = new BossScreen(Content);
            endingScreen = new EndingScreen(Content);
            endScreen = new EndingScreen2(Content);

            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new Color[1] { Color.White });

            // TODO: Initialize AudioManager data
        }

        protected override void Update(GameTime gameTime)
        {
            // Save keyboard state to use later
            input.Update();

            if(input.IsKeyPressed(Keys.Escape)) Exit();

            switch(CurrentScreen)
            {
                case GameScreen.Logo: logoScreen.Update(input); break;
                case GameScreen.Title: titleScreen.Update(input); break;
                case GameScreen.Gameplay: gameplayScreen.Update(input); break;
                case GameScreen.Win: endingScreen.Update(input); break;
                case GameScreen.Lose: endScreen.Update(input); break;
                case GameScreen.Boss: bossScreen.Update(input); break;
                default: break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();

            switch(CurrentScreen)
            {
                case GameScreen.Logo: logoScreen.Draw(spriteBatch); break;
                case GameScreen.Title: titleScreen.Draw(spriteBatch); break;
                case GameScreen.Gameplay: gameplayScreen.Draw(spriteBatch); break;
                case GameScreen.Win: endingScreen.Draw(spriteBatch); break;
                case GameScreen.Lose: endScreen.Draw(spriteBatch); break;
                case GameScreen.Boss: bossScreen.Draw(spriteBatch); break;
                default: break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            // Unload spriteBatch object
            spriteBatch.Dispose();
            Pixel.Dispose();

            base.UnloadContent();
        }
    }
}

