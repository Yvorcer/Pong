using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game4
{
    public class Pong : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D  background;
        Paddle paddleL, paddleR;
        Ball ball;
        private SpriteFont font;
        int ScoreL, ScroreR;
        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Background");
            font = Content.Load<SpriteFont>("ScoreFont");
            paddleR = new Paddle(Content, (int)GraphicsDevice.Viewport.Width - 15, (int)GraphicsDevice.Viewport.Height / 2, Keys.Up, Keys.Down);
            paddleL = new Paddle(Content, 15, (int)GraphicsDevice.Viewport.Height / 2, Keys.W, Keys.S);
            
            ball = new Ball(Content,  (int)GraphicsDevice.Viewport.Width, (int)GraphicsDevice.Viewport.Height);                                 

        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            paddleL.Update(gameTime, GraphicsDevice.Viewport.Height);
            paddleR.Update(gameTime, GraphicsDevice.Viewport.Height);
            ball.Update(gameTime);
            //ballCollision = ball.Rectangle;
            OnCollission();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            //spriteBatch.DrawString("")
            paddleL.Draw(gameTime, spriteBatch);
            paddleR.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, ScoreL.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 150, 150), Color.White); ;
            ball.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void OnCollission()
        {
            if (ball.collisionBox.Intersects(paddleL.collisionBox) || ball.collisionBox.Intersects(paddleR.collisionBox))
            {
                ball.direction.X *= -1;
                ball.speed *= 1.03f;
            }
        }
    }
}
