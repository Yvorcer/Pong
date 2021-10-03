using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    class PongCpu
    {
        Texture2D background, hearthSprite, paddle;
        Paddle paddleL;
        PaddleCPU paddleR;
        Ball ball;
        public bool pause;
        Vector2 startPaddlePos = new Vector2(15, (int)Pong.Screen.Y / 2);

        public PongCpu(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Background");
            hearthSprite = Content.Load<Texture2D>("Hearth");
            paddle = Content.Load<Texture2D>("Bat");

            paddleR = new PaddleCPU(Content, (int)Pong.Screen.X - 15, (int)startPaddlePos.Y, Color.Red);
            paddleL = new Paddle(Content, 15, (int)startPaddlePos.Y, Keys.W, Keys.S, Color.Blue);

            ball = new Ball(Content, (int)Pong.Screen.X, (int)Pong.Screen.Y);
        }

        public void Update(GameTime gameTime)
        {
            if (!pause)
            {
                paddleL.Update(gameTime, (int)Pong.Screen.Y);
                paddleR.Update(gameTime, (int)Pong.Screen.Y);
                paddleR.CPUAI(gameTime, ball.ballPosition, Pong.Screen.X);
                ball.Update(gameTime);
                OnCollission();
            }
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Escape))
            {
                Pong.Selected.Play();
                pause = !pause;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            paddleL.Draw(gameTime, spriteBatch, Color.Blue);
            paddleR.Draw(gameTime, spriteBatch, Color.Red);
            ball.Draw(gameTime, spriteBatch);
            DrawUI(spriteBatch);
        }

        private void DrawUI(SpriteBatch spriteBatch)
        {
            if (ball.LivesL > 0 && ball.LivesR > 0 && pause)
            {
                //Centering the text on Screen
                string pauseText = "PAUSE!";
                Vector2 pauseTextLength = Pong.BigFont.MeasureString(pauseText);
                spriteBatch.DrawString(Pong.BigFont, pauseText, new Vector2((Pong.Screen.X - pauseTextLength.X) / 2, (Pong.Screen.Y - pauseTextLength.Y) / 2), Color.White);

                string Unpause = "Press 'Escape'to Unpause";
                Vector2 UnpauseLength = Pong.SmallFont.MeasureString(Unpause);
                spriteBatch.DrawString(Pong.SmallFont, Unpause, new Vector2((Pong.Screen.X - UnpauseLength.X) / 2, Pong.Screen.Y - 50), Color.Gray);
            }
            else if (ball.LivesL == 0 || ball.LivesR == 0)
            {
                pause = true;

                string gameOverText = "GAME OVER";
                Vector2 gameOverTextLength = Pong.BigFont.MeasureString(gameOverText);
                Vector2 gameOVerPos = new Vector2((Pong.Screen.X - gameOverTextLength.X) / 2, (Pong.Screen.Y - gameOverTextLength.Y) / 2);
                spriteBatch.DrawString(Pong.BigFont, gameOverText, gameOVerPos, Color.White);
                if (ball.LivesR > 0)
                    spriteBatch.DrawString(Pong.SmallFont, "Red Wins", gameOVerPos + new Vector2(75, gameOverTextLength.Y), Color.Red);
                else
                    spriteBatch.DrawString(Pong.SmallFont, "Blue Wins", gameOVerPos + new Vector2(75, gameOverTextLength.Y), Color.Blue);

                string proceed = "Press 'Enter' to play again,";
                Vector2 proceedLength = Pong.SmallFont.MeasureString(proceed);
                spriteBatch.DrawString(Pong.SmallFont, proceed, new Vector2((Pong.Screen.X - proceedLength.X) / 2, Pong.Screen.Y - 100), Color.Gray);

                string proceed2 = "Press 'Back' for TitleScreen";
                Vector2 proceedLength2 = Pong.SmallFont.MeasureString(proceed);
                spriteBatch.DrawString(Pong.SmallFont, proceed2, new Vector2((Pong.Screen.X - proceedLength2.X) / 2, Pong.Screen.Y - 50), Color.Gray);
            }

            // Back of the lives
            for (int i = 0; i < 3; i++)
            {
                Vector2 hearthPosition = new Vector2(i * 25, 3);
                spriteBatch.Draw(hearthSprite, hearthPosition, null, Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 hearthPosition = new Vector2(i * -25 + 770, 3);
                spriteBatch.Draw(hearthSprite, hearthPosition, null, Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }
            // Lives 
            for (int i = 0; i < ball.LivesL; i++)
            {
                Vector2 hearthPosition = new Vector2(i * 25, 3);
                spriteBatch.Draw(hearthSprite, hearthPosition, null, Color.Blue, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }
            for (int i = 0; i < ball.LivesR; i++)
            {
                Vector2 hearthPosition = new Vector2(i * -25 + 770, 3);
                spriteBatch.Draw(hearthSprite, hearthPosition, null, Color.Red, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }
        }
        private void OnCollission()
        {
            if (ball.collisionBox.Intersects(paddleL.collisionBox))
            {
                Pong.PaddleHit.Play();

                float width = paddle.Height / 2;
                float offset = paddleL.paddlePosition.Y - ball.ballPosition.Y;

                float percentage = (offset / width); // Distance that the baal is from the center 
                ball.direction.X *= -1;
                ball.direction = new Vector2(ball.direction.X, -percentage); // Giving the ball a random angle depending on where it hits the paddle.
                ball.direction.Normalize();

                //Keep adding steps until not colliding anymore to prevent double hitting.
                while (ball.collisionBox.Intersects(paddleL.collisionBox))
                {
                    ball.ballPosition += new Vector2(ball.direction.X, 0) * ball.speed;
                }

                ball.speed *= 1.03f; // Increasing the speed ever so slightly.
            }
            //Same Code as above but for the other Paddle.
            else if (ball.collisionBox.Intersects(paddleR.collisionBox))
            {
                Pong.PaddleHit.Play();

                float width = paddle.Height / 2;
                float offset = paddleR.paddlePosition.Y - ball.ballPosition.Y;

                float percentage = (offset / width);
                ball.direction.X *= -1;
                ball.direction = new Vector2(ball.direction.X, -percentage);
                ball.direction.Normalize();

                while (ball.collisionBox.Intersects(paddleR.collisionBox))
                {
                    ball.ballPosition += new Vector2(ball.direction.X, 0) * ball.speed;
                }

                ball.speed *= 1.03f;
            }
        }

        public void Reset()
        {
            ball.LivesL = 3;
            ball.LivesR = 3;

            paddleR.Reset();
            paddleL.Reset();

            ball.BallReset();
        }
    }
}
