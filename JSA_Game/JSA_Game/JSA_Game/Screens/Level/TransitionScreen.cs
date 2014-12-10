using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using JSA_Game.Maps;

namespace JSA_Game.Screens
{
    class TransitionScreen : GameScreen
    {
        const int DURATION = 1000;
        const float MESSAGE_SCALE = 1;
        string message;
        int messageHeight;
        Vector2 messagePosition;
        Color messageColor;
        float elapsedTime = 0;


          /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public TransitionScreen(string message, Color messageColor)
        {
           // No events since there is no list of menu entries.
           // This is used to simply show transitions between some states.
            this.message = message;
            messageHeight = 0;
            this.messageColor = messageColor;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                      bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            


            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            
            if(elapsedTime >= DURATION){
                ExitScreen();
            }

        }

        protected virtual void UpdateMessageLocation(SpriteFont font)
        {

            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            messageHeight = (int)((ScreenManager.GraphicsDevice.Viewport.Height - font.MeasureString(message).Y)/2) - 100;
            messagePosition = new Vector2((ScreenManager.GraphicsDevice.Viewport.Width-font.MeasureString(message).X)/2, messageHeight);


            if (ScreenState == ScreenState.TransitionOn)
                messagePosition.X -= transitionOffset * 256;
            else
                messagePosition.X += transitionOffset * 512;
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            UpdateMessageLocation(font);
            spriteBatch.Begin();

            
            // Draw the menu title centered on the screen
            //Vector2 messagePosition = new Vector2(graphics.Viewport.Width / 2, messageHeight);
            Vector2 messageOrigin = new Vector2(0,0);
            //Vector2 messageOrigin = new Vector2(graphics.Viewport.Width / 2, 0);


            //titlePosition.Y -= transitionOffset * 100;
            //Border
            Texture2D pixel;
            pixel = new Texture2D(ScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it

            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(0, messageHeight - 10,
                ScreenManager.GraphicsDevice.Viewport.Width, 1), messageColor);



            spriteBatch.DrawString(font, message, messagePosition, messageColor, 0,
                                   messageOrigin, MESSAGE_SCALE, SpriteEffects.None, 0);

            spriteBatch.Draw(pixel, new Rectangle(0,(int)( messageHeight + 10 + font.MeasureString(message).Y),
                ScreenManager.GraphicsDevice.Viewport.Width, 1), messageColor);
            spriteBatch.End();
        }
    }
}
