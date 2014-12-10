using System;
using System.Collections.Generic;
using System.IO;
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
        string text;
        string charName, className, charLevel;
        Character c;
        Texture2D charImage;
       
        Vector2 position;
        public event EventHandler<PlayerIndexEventArgs> Selected;

        public CharacterEntry(GraphicsDevice graphicsDevice, Character c)
        {
            text = c.Name+ " Lv " + c.Level;
            charName = c.Name;
            className = c.ClassName;
            charLevel =  "Lv " + c.Level;
            this.c = c;

            charImage = Texture2D.FromStream(graphicsDevice,
                     File.OpenRead("..\\..\\..\\..\\JSA_GameContent\\player" + className + ".png"));
        }

        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }

        public virtual void Update(CharacterListScreen screen, bool isSelected, GameTime gameTime)
        {
 
        }

        public virtual void Draw(CharacterListScreen screen, bool isSelected, GameTime gameTime, int maxEntryLength)
        {

            // Draw the selected entry in yellow, otherwise white.
            Color color = isSelected ? Color.Yellow : Color.White;
            if (c.IsPlaced && !isSelected)
                color = Color.OrangeRed;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;

            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;
            
            Vector2 origin = new Vector2(5, font.LineSpacing / 2);

            Rectangle boundaries = new Rectangle((int)position.X, (int)(position.Y - font.MeasureString(text).Y), maxEntryLength, (int)font.MeasureString(text).Y);

            DrawContents(screen, isSelected, boundaries, maxEntryLength);

            boundaries.Y -= (int) font.MeasureString(text).Y / 2;
            

            //Border
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

        public void DrawContents(CharacterListScreen screen, bool isSelected, Rectangle boundaries, int maxEntryLength)
        {
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 charNameSize = font.MeasureString(charName);

            int strHeight = (int)Math.Round(charNameSize.Y);
            Vector2 position = new Vector2();
            position.X = boundaries.X + 0.25f * maxEntryLength + 5;
            position.Y = boundaries.Height - strHeight + boundaries.Y;

            Vector2 nameOrigin = new Vector2(0, font.LineSpacing);
            Vector2 classLevelOrigin = new Vector2(0, strHeight / 8);

            Color color = isSelected ? Color.Yellow : Color.White;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;


            int imageSquareSideLength = (int)(font.LineSpacing) - 5;
            Rectangle rect = new Rectangle(boundaries.X+5, boundaries.Y - strHeight/2+5,
                                            imageSquareSideLength, imageSquareSideLength);
            spriteBatch.Draw(charImage, rect, Color.White);
            spriteBatch.DrawString(font, charName, position, color, 0, nameOrigin, .49f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, className + "  " + charLevel, position, color, 0, classLevelOrigin, .49f, SpriteEffects.None, 0);
        } 




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



