using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    class MenuScreen
    {
        Texture2D background;
        public MenuScreen(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Background");
        }



        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            
            spriteBatch.DrawString(Pong.SmallFont, "1.) Pong Versus", new Vector2(15, 75), Color.White);
            spriteBatch.DrawString(Pong.SmallFont, "2.) Pong Multiplayer", new Vector2(15, 150), Color.White);
            spriteBatch.DrawString(Pong.SmallFont, "3.) Pong Versus CPU", new Vector2(15, 225), Color.White);

            string proceed = "Proceed by pressing 'NumberKey'";
            Vector2 proceedLength = Pong.SmallFont.MeasureString(proceed);
            spriteBatch.DrawString(Pong.SmallFont, proceed, new Vector2((Pong.Screen.X - proceedLength.X) / 2, Pong.Screen.Y - 50), Color.Gray);
        }
    }
}
