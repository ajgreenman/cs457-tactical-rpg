using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace JSA_Game.Maps
{
    class Cursor
    {

        //Texture2D cursor;
        Texture2D[] cursorParts;
        int cursorFrames = 0;

        Vector2 cursorPos;
        public Vector2 CursorPos
        {
            get { return cursorPos; }
            set { cursorPos = value; }
        }
       // Rectangle cursorSourceRect;
        Rectangle[] cursorSourceRects;
        Rectangle[] cursorDestRects;
        //String cursorAnimation;

        float elapsedTime;
        float animateDelay = 500f;
        float cursorTimeElapsed;
        float cursorMoveDelay = 100;

        //Location variables for drawing
        int widthOffset;
        int heighOffset;
        int areaWidth;
        int areaHeight;


        /// <summary>
        /// Cursor constructor.  A cursor is meant to work on items in a list
        /// or an array where the edges of the items are touching.
        /// </summary>
        /// <param name="wOffset">x-value offset for the location of the container</param>
        /// <param name="hOffset">y-value offset for the location of the container</param>
        /// <param name="areaW">width of item in container</param>
        /// <param name="areaH">height of item in container</param>
        public Cursor(int wOffset, int hOffset, int areaW, int areaH)
        {
            widthOffset = wOffset;
            heighOffset = hOffset;
            areaWidth = areaW;
            areaHeight = areaH;
            cursorParts = new Texture2D[4];
            cursorSourceRects = new Rectangle[4];
            cursorDestRects = new Rectangle[4];
            cursorPos = new Vector2(0, 0);
        }

        public void animate(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= animateDelay)
            {
                if (cursorFrames >= 1)
                {
                    cursorFrames = 0;
                }
                else
                {
                    cursorFrames++;
                }
                elapsedTime = 0;
            }

            //Cursor source rects
            for (int i = 0; i < cursorSourceRects.Length; i++)
            {
                cursorSourceRects[i] = new Rectangle(cursorParts[i].Width / 2 * cursorFrames, 0, cursorParts[i].Width / 2, cursorParts[i].Height);
            }

            //Left side dest
            cursorDestRects[0] = new Rectangle(widthOffset + areaWidth * (int)cursorPos.X,
                heighOffset + areaHeight * (int)cursorPos.Y,
                cursorParts[0].Width / 2, cursorParts[0].Height);
            cursorDestRects[2] = new Rectangle(widthOffset + areaWidth * (int)cursorPos.X,
                heighOffset + areaHeight * (1 + (int)cursorPos.Y) - cursorParts[2].Height,
                cursorParts[2].Width / 2, cursorParts[2].Height);

            //Right side dest
            cursorDestRects[1] = new Rectangle(widthOffset + areaWidth * (1+(int)cursorPos.X) - cursorParts[1].Width/2,
                heighOffset + areaHeight * (int)cursorPos.Y,
                cursorParts[1].Width / 2, cursorParts[1].Height);
            cursorDestRects[3] = new Rectangle(widthOffset + areaWidth * (1 + (int)cursorPos.X) - cursorParts[3].Width/2,
                heighOffset + areaHeight * (1 + (int)cursorPos.Y) - cursorParts[3].Height,
                cursorParts[3].Width / 2, cursorParts[3].Height);
            
        }

        public void loadContent(ContentManager content)
        {
            cursorParts[0] = content.Load<Texture2D>("cursorTLAnim");
            cursorParts[1] = content.Load<Texture2D>("cursorTRAnim");
            cursorParts[2] = content.Load<Texture2D>("cursorBLAnim");
            cursorParts[3] = content.Load<Texture2D>("cursorBRAnim");

        }

        public void moveCursor(GameTime gameTime)
        {

            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);
            cursorTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (cursorTimeElapsed >= cursorMoveDelay)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && cursorPos.X != 0)
                {
                    cursorPos.X--;
                }
                else if (keyboard.IsKeyDown(Keys.Right) && cursorPos.X != areaWidth - 1)
                {
                    cursorPos.X++;
                }
                else if (keyboard.IsKeyDown(Keys.Up) && cursorPos.Y != 0)
                {
                    cursorPos.Y--;
                }
                else if (keyboard.IsKeyDown(Keys.Down) && cursorPos.Y != areaHeight - 1)
                {
                    cursorPos.Y++;
                }
                cursorTimeElapsed = 0;
            }
        }

        public void moveCursorDir(char dir)
        {
            switch(dir)
            {
                case 'l': cursorPos.X--; break;
                case 'r': cursorPos.X++; break;
                case 'u': cursorPos.Y--; break;
                case 'd': cursorPos.Y++; break;
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < cursorParts.Length; i++)
            {
                spriteBatch.Draw(cursorParts[i], cursorDestRects[i], cursorSourceRects[i], Color.White);
            }

        }
    }
}