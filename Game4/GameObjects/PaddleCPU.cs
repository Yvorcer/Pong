using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    class PaddleCPU
    {
        public Vector2 paddlePosition;
        Vector2 paddleOrigin, paddleStartPos;
        Texture2D paddle;
        float paddleSpeed = 0.4f;
        public Rectangle collisionBox;
        public Color color;


        public PaddleCPU(ContentManager Content, int startX, int startY, Color color)
        {
            this.color = color;

            this.paddleStartPos = new Vector2(startX, startY);
            Reset();

            paddle = Content.Load<Texture2D>("Bat");
            paddleOrigin = new Vector2(paddle.Width / 2, paddle.Height / 2);
        }

        public void Update(GameTime gametime, int windowHeight)
        {
            collisionBox = new Rectangle((int)paddlePosition.X - (paddle.Width / 2), (int)paddlePosition.Y - (paddle.Height / 2), paddle.Width, paddle.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(paddle, paddlePosition, collisionBox, color, 0.0f, paddleOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

        public void Reset()
        {
            paddlePosition = paddleStartPos;
        }

        public void CPUAI (GameTime gametime, Vector2 ballPos, float side, float difficulty = 1.01f, float earlyStopChance = 2.7f) 
        {
            //paddlePosition.Y = ballPos.Y; Now that would not be fun XD, So let's go about it another way:

            var delta = (float)gametime.ElapsedGameTime.Milliseconds;

            float random = (float)Pong.Random.NextDouble(); // My CPU Could destroy every gamer, but that's not fair so lets make him randomly dumb or smart.
            if ((float)Math.Pow(ballPos.X, difficulty) > side * random) // The closer the ball is the better the CPU is at predicting where to move next.
            {
                if (ballPos.Y > paddlePosition.Y + paddle.Height / earlyStopChance * random) // Move to the right location while also having a chance to stop to early, 
                    paddlePosition.Y += paddleSpeed * delta; // Make it move the same as a human would so it's not teleporting.
                else if (ballPos.Y < paddlePosition.Y - paddle.Height / earlyStopChance * random) // this will also make the cpu hit the ball on random locations.
                    paddlePosition.Y -= paddleSpeed * delta;
            }
            paddlePosition.Y = Math.Clamp(paddlePosition.Y, 0 + paddle.Height / 2, Pong.Screen.Y - paddle.Height / 2); // Keep the AI in BOUNDS, who knows what could happen if it would break free.

        }


    }
}
