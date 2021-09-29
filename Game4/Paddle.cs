using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game4
{
    class Paddle
    {
        public Vector2 paddlePosition;
        Vector2 paddleOrigin;
        Texture2D paddle;
        float paddleSpeed = 0.5f;
        private Keys keyUp, keyDown;
        public Rectangle collisionBox;

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
            var delta = (float)gametime.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(keyUp) && paddlePosition.Y - (paddle.Height / 2) > 0)
                paddlePosition.Y -= paddleSpeed * delta;
            if (Keyboard.GetState().IsKeyDown(keyDown) && paddlePosition.Y + (paddle.Height / 2) < windowHeight)
                paddlePosition.Y += paddleSpeed * delta;

            collisionBox = new Rectangle((int)paddlePosition.X - (paddle.Width/2), (int)paddlePosition.Y - (paddle.Height / 2), paddle.Width, paddle.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(paddle, paddlePosition, collisionBox, color, 0.0f, paddleOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }

    }
}
