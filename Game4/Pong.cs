using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    public class Pong : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D background, hearthSprite, paddle;
        Paddle paddleL, paddleR;
        Ball ball;
        InputHelper inputHelper;
        private SpriteFont font;
        bool pause;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            inputHelper = new InputHelper();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Background");
            hearthSprite = Content.Load<Texture2D>("Hearth");
            font = Content.Load<SpriteFont>("ScoreFont");
            paddle = Content.Load<Texture2D>("Bat");
            paddleR = new Paddle(Content, (int)GraphicsDevice.Viewport.Width - 15, (int)GraphicsDevice.Viewport.Height / 2, Keys.Up, Keys.Down);
            paddleL = new Paddle(Content, 15, (int)GraphicsDevice.Viewport.Height / 2, Keys.W, Keys.S);
            
            ball = new Ball(Content,  (int)GraphicsDevice.Viewport.Width, (int)GraphicsDevice.Viewport.Height);

            
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update();
            Input();
            if (!pause)
            {
                paddleL.Update(gameTime, GraphicsDevice.Viewport.Height);
                paddleR.Update(gameTime, GraphicsDevice.Viewport.Height);
                ball.Update(gameTime);
                OnCollission();
            }
        }

        private void Input()
        {
            if (inputHelper.KeyPressed(Keys.Escape))
            {
                pause = !pause;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1));
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            paddleL.Draw(gameTime, spriteBatch, Color.Blue);
            paddleR.Draw(gameTime, spriteBatch, Color.Red);

            for (int i = 0; i < ball.LivesL; i++)
            {
                Vector2 hearthPosition = new Vector2(i * 25, 3);
                spriteBatch.Draw(hearthSprite, hearthPosition,  null, Color.Blue, 0.0f, Vector2.Zero, 2.0f,  SpriteEffects.None, 0.0f);
            }
            for (int i = 0; i < ball.LivesR; i++)
            {
                Vector2 hearthPosition = new Vector2(i * -25 + 770, 3);
                spriteBatch.Draw(hearthSprite, hearthPosition, null, Color.Red, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }

            if (pause)
            {
                spriteBatch.DrawString(font, "PAUSE!", new Vector2(GraphicsDevice.Viewport.Width / 2 - 130, 150), Color.White); 
            }
            
            
            ball.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
        private void OnCollission()
        {
            if (ball.collisionBox.Intersects(paddleL.collisionBox))
            {
                Vector2 collisionPoint = ball.ballPosition;
                
                float width = paddle.Height / 2;
                float offset = paddleL.paddlePosition.Y  - ball.ballPosition.Y;

                float percentage = (offset / width);
                ball.direction.X *= -1;
                ball.direction = new Vector2(ball.direction.X, -percentage);
                ball.direction.Normalize();

                ball.speed *= 1.03f;
            }
            else if (ball.collisionBox.Intersects(paddleR.collisionBox))
            {
                Vector2 collisionPoint = ball.ballPosition;

                float width = paddle.Height / 2;
                float offset = paddleR.paddlePosition.Y - ball.ballPosition.Y;

                float percentage = (offset / width);
                ball.direction.X *= -1;
                ball.direction = new Vector2(ball.direction.X, -percentage);
                ball.direction.Normalize();

                ball.speed *= 1.03f;
            }
        }
    }
}
