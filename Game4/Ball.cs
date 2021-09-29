using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    class Ball
    {
        Texture2D ball;
        public Vector2 ballPosition;
        Vector2 ballOrigin, startPos;
        public Vector2 direction = Vector2.One;
        public Rectangle collisionBox;
        public Vector2 speed;
        private int windowWidth, windowHeight;
        Random random = new Random();
        public int LivesL = 3, LivesR = 3;
        


        public Ball(ContentManager Content, int windowWidth, int windowheight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowheight;

            ball = Content.Load<Texture2D>("Ball");
            startPos = new Vector2(windowWidth / 2, windowheight / 2);
            BallReset();
            ballOrigin = new Vector2(ball.Width, ball.Height) / 2;
        } 

        public void Update(GameTime gametime)
        {
            direction.Normalize();
            var delta = (float)gametime.ElapsedGameTime.TotalMilliseconds;
            ballPosition += direction * speed * delta;

            if (ballPosition.Y >= windowHeight || ballPosition.Y <= 0)
                direction.Y *= -1;
            if (ballPosition.X >= windowWidth || ballPosition.X <= 0)
            {
                Score();
            }

            collisionBox = new Rectangle((int)ballPosition.X - (ball.Width / 2), (int)ballPosition.Y - (ball.Height / 2), ball.Width, ball.Height);
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, ballPosition, collisionBox, Color.White, 0.0f, ballOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

        private void Score()
        {

            if (ballPosition.X <= 0)
            {          
                LivesL--;
                BallReset();
            }
            else
            {
                LivesR--;
                BallReset();
            }
        }
        
        private void BallReset()
        {
            speed = new Vector2(1, 1) * 0.3f;
            ballPosition = startPos;
            double randomDir = random.NextDouble();

            if(randomDir >= 0.5)
            {
                direction = new Vector2(-1, 1);
            }
            else
                direction = new Vector2(1, -1);
        }
    }
}
