using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Game4
{
    public class Pong : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        InputHelper inputHelper;

        //GameStates
        TitleScreen titleScreen;
        MenuScreen menuScreen;
        GameWorld gameWorld;
        PongMultiplayer multiplayer;
        PongCpu pongCpu;

        static SpriteFont bigFont, smallFont; //Fonts
        static SoundEffect wallHit, paddleHit, pointLost, selected;
        static Random random;
        static Vector2 screenSize;

        public GameStates gameState;

        
        [STAThread]
        static void Main()
        {
            Pong game = new Pong();
            game.Run();
        }

        public Pong()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);

            IsMouseVisible = true;
            random = new Random();
            inputHelper = new InputHelper();

            gameState = GameStates.TitleScreen;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Loading in Fonts
            bigFont = Content.Load<SpriteFont>("ScoreFont");
            smallFont = Content.Load<SpriteFont>("smallFont");

            // Loading in Sounds
            wallHit = Content.Load<SoundEffect>("Sounds/WallHit");
            paddleHit = Content.Load<SoundEffect>("Sounds/PaddleHit");
            pointLost = Content.Load<SoundEffect>("Sounds/PointLost");
            selected = Content.Load<SoundEffect>("Sounds/Selected");

            screenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            titleScreen = new TitleScreen(Content);
            menuScreen = new MenuScreen(Content);
            gameWorld = new GameWorld(Content);
            multiplayer = new PongMultiplayer(Content);
            pongCpu = new PongCpu(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput(inputHelper);
            inputHelper.Update();
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    break;
                case GameStates.MenuScreen:                    
                    break;
                case GameStates.PongGame:
                    gameWorld.Update(gameTime);
                    gameWorld.HandleInput(inputHelper);
                    break;
                case GameStates.PongMultiplayer:
                    multiplayer.Update(gameTime);
                    multiplayer.HandleInput(inputHelper);
                    break;
                case GameStates.PongCpu:
                    pongCpu.Update(gameTime);
                    pongCpu.HandleInput(inputHelper);
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    titleScreen.Draw(gameTime, spriteBatch);
                    break;
                case GameStates.MenuScreen:
                    menuScreen.Draw(gameTime, spriteBatch);
                    break;
                case GameStates.PongGame:
                    gameWorld.Draw(gameTime, spriteBatch);
                    break;
                case GameStates.PongMultiplayer:
                    multiplayer.Draw(gameTime, spriteBatch);
                    break;
                case GameStates.PongCpu:
                    pongCpu.Draw(gameTime, spriteBatch);
                    break;
            }
            spriteBatch.End();
        }

        private void HandleInput(InputHelper inputHelper)
        {
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if (inputHelper.KeyPressed(Keys.Enter) || inputHelper.KeyPressed(Keys.Space))
                    {
                        gameState = GameStates.MenuScreen;
                        selected.Play();
                    }
                    break;
                case GameStates.MenuScreen:
                    if (inputHelper.KeyPressed(Keys.D1))
                    {
                        gameState = GameStates.PongGame;
                        selected.Play();
                    }
                    else if (inputHelper.KeyPressed(Keys.D2))
                    {
                        gameState = GameStates.PongMultiplayer;
                        selected.Play();
                    }
                    else if (inputHelper.KeyPressed(Keys.D3))
                    {
                        gameState = GameStates.PongCpu;
                        selected.Play();
                    }
                    break;
                case GameStates.PongGame:
                    if (inputHelper.KeyPressed(Keys.Enter) && gameWorld.pause)
                    {
                        gameWorld.Reset();
                        selected.Play();
                    }
                    else if (inputHelper.KeyPressed(Keys.Back) && gameWorld.pause)
                    {
                        gameState = GameStates.TitleScreen;
                        gameWorld.Reset();
                        selected.Play();
                    }
                    break;
                case GameStates.PongMultiplayer:
                    if (inputHelper.KeyPressed(Keys.Enter) && multiplayer.pause)
                    {
                        multiplayer.Reset();
                        selected.Play();
                    }
                    else if (inputHelper.KeyPressed(Keys.Back) && multiplayer.pause)
                    {
                        gameState = GameStates.TitleScreen;
                        multiplayer.Reset();
                        selected.Play();
                    }
                    break;
                case GameStates.PongCpu:
                    if (inputHelper.KeyPressed(Keys.Enter) && pongCpu.pause)
                    {
                        pongCpu.Reset();
                        selected.Play();
                    }
                    else if (inputHelper.KeyPressed(Keys.Back) && pongCpu.pause)
                    {
                        gameState = GameStates.TitleScreen;
                        pongCpu.Reset();
                        selected.Play();
                    }
                    break;
            }
        }


        public static Vector2 Screen
        {
            get { return screenSize; }
        }
        public static Random Random
        {
            get { return random; }
        }
        public static SpriteFont BigFont
        {
            get { return bigFont; }
        }
        public static SpriteFont SmallFont
        {
            get { return smallFont; }
        }

        public static SoundEffect WallHit
        {
            get { return wallHit; }
        }
        public static SoundEffect PaddleHit
        {
            get { return paddleHit; }
        }
        public static SoundEffect PointLost
        {
            get { return pointLost; }
        }
        public static SoundEffect Selected
        {
            get { return selected; }
        }

        public enum GameStates // All the different Gamestates in the game.
        {
            TitleScreen,
            MenuScreen,
            PongGame,
            PongMultiplayer,
            PongCpu,
        }
    }
}
