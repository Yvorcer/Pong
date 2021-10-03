using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    class TitleScreen
    {
        Texture2D background, title;
        Vector2 titlePos, titleOrigin;
        public TitleScreen(ContentManager Content)
        {
            title = Content.Load<Texture2D>("PongLogo");
            background = Content.Load<Texture2D>("Background");
            titlePos = new Vector2(Pong.Screen.X, Pong.Screen.Y) / 2;
            titleOrigin = new Vector2(title.Width, title.Height + 100) / 2;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(title, titlePos, null , Color.White, 0.0f, titleOrigin, 1.0f, SpriteEffects.None, 0.0f);

            //Centering the text on Screen
            string proceed = "Press 'Enter' to continue";
            Vector2 proceedLength = Pong.SmallFont.MeasureString(proceed);
            spriteBatch.DrawString(Pong.SmallFont, proceed, new Vector2((Pong.Screen.X - proceedLength.X)/2, Pong.Screen.Y - 50), Color.Gray);
        }
    }
}