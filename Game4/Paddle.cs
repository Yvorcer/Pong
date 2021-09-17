using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game4
{
    class Paddle
    {
        Vector2 paddlePosition, paddleOrigin;
        Texture2D paddle;
        float paddleSpeed = 5.0f;
        private Keys keyUp, keyDown;
        public Rectangle collisionBox;
        //public Rectangle Rectangle { get { } }

        public Paddle(ContentManager Content, int startX, int startY, Keys keyUp, Keys keyDown) 
        {
            this.keyUp = keyUp;
            this.keyDown = keyDown;

            paddlePosition = new Vector2(startX, startY);
            paddle = Content.Load<Texture2D>("Bat");
            paddleOrigin = new Vector2(paddle.Width / 2, paddle.Height / 2);
        }

        public void Update(GameTime gametime, int windowHeight)
        {
            if (Keyboard.GetState().IsKeyDown(keyUp) && paddlePosition.Y - (paddle.Height / 2) > 0)
                paddlePosition.Y -= paddleSpeed;
            if (Keyboard.GetState().IsKeyDown(keyDown) && paddlePosition.Y + (paddle.Height / 2) < windowHeight)
                paddlePosition.Y += paddleSpeed;

            collisionBox = new Rectangle((int)paddlePosition.X - (paddle.Width/2), (int)paddlePosition.Y - (paddle.Height / 2), paddle.Width, paddle.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(paddle, collisionBox, Color.White);
            spriteBatch.Draw(paddle, paddlePosition, collisionBox, Color.White, 0.0f, paddleOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

    }
}
