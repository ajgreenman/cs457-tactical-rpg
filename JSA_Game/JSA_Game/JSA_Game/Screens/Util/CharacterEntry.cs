using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
using JSA_Game.CharClasses;

namespace JSA_Game.Screens
{
    class CharacterEntry
    {
        const float ENTRY_SIZE_SCALE = 1.00f;
        string text;
        float selectionFade;
        Vector2 position;
        public event EventHandler<PlayerIndexEventArgs> Selected;

        public CharacterEntry(Character c)
        {
            text = c.Name+ " Lv " + c.Level;
        }

        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }

        public virtual void Update(CharacterListScreen screen, bool isSelected, GameTime gameTime)
        {
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }

        public virtual void Draw(CharacterListScreen screen, bool isSelected, GameTime gameTime, int maxEntryLength)
        {

            // Draw the selected entry in yellow, otherwise white.
            Color color = isSelected ? Color.Yellow : Color.White;

            // Pulsate the size of the selected menu entry.
            //double time = gameTime.TotalGameTime.TotalSeconds;

            //float pulsate = (float)Math.Sin(time * 6) + 1;
            //float scale = 1 + pulsate * 0.05f * selectionFade;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;

            // Draw text, centered on the middle of each line.
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;
            
            Vector2 origin = new Vector2(5, font.LineSpacing / 2);

            Rectangle boundaries = new Rectangle((int)position.X, (int)(position.Y - font.MeasureString(text).Y), maxEntryLength, (int)font.MeasureString(text).Y);
            //DrawString(screen, isSelected, text, boundaries);
            //spriteBatch.DrawString(font, text, position, color, 0,
                   //                origin, 1, SpriteEffects.None, 0);
            DrawString(screen, isSelected, text, boundaries);

            boundaries.Y -= (int) font.MeasureString(text).Y / 2;
            
            // At the top of your class:
            Texture2D pixel;

            pixel = new Texture2D(screenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it

            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(boundaries.X, boundaries.Y, boundaries.Width + 10, 2), color);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(boundaries.X, boundaries.Y, 2, boundaries.Height), color);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle(boundaries.X + boundaries.Width + 9,
                                            boundaries.Y,
                                            2,
                                            boundaries.Height), color);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(boundaries.X,
                                            boundaries.Y + boundaries.Height - 1,
                                            boundaries.Width + 10,
                                            2), color);
             
        }

        /// <summary>
        /// Draws the given string as large as possible inside the boundaries Rectangle without going
        /// outside of it.  This is accomplished by scaling the string (since the SpriteFont has a specific
        /// size).
        /// 
        /// If the string is not a perfect match inside of the boundaries (which it would rarely be), then
        /// the string will be absolutely-centered inside of the boundaries.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="strToDraw"></param>
        /// <param name="boundaries"></param>
        static public void DrawString(CharacterListScreen screen, bool isSelected, string strToDraw, Rectangle boundaries)
        {
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 size = font.MeasureString(strToDraw);

            float xScale = (boundaries.Width / size.X);
            float yScale = (boundaries.Height / size.Y);

            // Taking the smaller scaling value will result in the text always fitting in the boundaires.
            float scale = Math.Min(xScale, yScale);

            // Figure out the location to absolutely-center it in the boundaries rectangle.
            int strWidth = (int)Math.Round(size.X * scale);
            int strHeight = (int)Math.Round(size.Y * scale);
            Vector2 position = new Vector2();
            position.X = (((boundaries.Width - strWidth) / 2) + boundaries.X + 5);
            position.Y = (((boundaries.Height - strHeight) / 2) + boundaries.Y);

            // A bunch of settings where we just want to use reasonable defaults.
            float rotation = 0.0f;
            Vector2 origin = new Vector2(0, font.LineSpacing / 2);
            float spriteLayer = 0.0f; // all the way in the front
            SpriteEffects spriteEffects = new SpriteEffects();

            Color color = isSelected ? Color.Yellow : Color.White;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;
            scale = ENTRY_SIZE_SCALE;
            // Draw the string to the sprite batch!
            spriteBatch.DrawString(font, strToDraw, position, color, rotation, origin, scale, SpriteEffects.None, 0);
        } // end DrawString()


        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(CharacterListScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }

        public virtual int GetWidth(CharacterListScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}



