using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game4
{
    class Ball
    {
        Texture2D ball;
        Vector2 ballPosition, ballOrigin, startPos;
        public Vector2 direction = Vector2.One;
        public Rectangle collisionBox;
        public float speed = 5.0f;
        int windowWidth, windowHeight;

        //public Rectangle Rectangle
        //{
        //    get { return collisionBox; }
        //}

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
            ballPosition += direction * speed;

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
            //spriteBatch.Draw(ball, ballPosition, Color.White);
            spriteBatch.Draw(ball, ballPosition, collisionBox, Color.White, 0.0f, ballOrigin, 1.0f, SpriteEffects.None, 0.0f);

        }

        private void Score()
        {
            BallReset();
            speed = 5.0f;
        }
        
        private void BallReset()
        {
            ballPosition = startPos;
        }


    }
}
