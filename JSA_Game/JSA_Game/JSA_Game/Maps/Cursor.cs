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
        Rectangle[] cursorSourceRects;
        Rectangle[] cursorDestRects;

        float elapsedTime;
        float animateDelay = 500f;
        //float cursorTimeElapsed;
        //float cursorMoveDelay = 100;

        //Location variables for drawing
        int widthOffset;
        int heighOffset;
        int areaWidth;
        int areaHeight;
        int width;
        int height;

        //array to move over (optional)
        int[][] array = null;


        /// <summary>
        /// Cursor constructor.
        /// </summary>
        /// <param name="level">Level to use the cursor</param>

        public Cursor(Level level)
        {
            widthOffset = Level.MAP_START_W;
            heighOffset = Level.MAP_START_H;
            areaWidth = Level.TILE_SIZE;
            areaHeight = Level.TILE_SIZE;
            width = level.NumTilesShowing;
            height = level.NumTilesShowing;         //!!CHANGE
            cursorParts = new Texture2D[4];
            cursorSourceRects = new Rectangle[4];
            cursorDestRects = new Rectangle[4];
            cursorPos = new Vector2(0, 0);
        }


        /// <summary>
        /// Cursor constructor.  A cursor is meant to work on items in a list
        /// or an array where the edges of the items are touching.
        /// Calling this constructor means the caller needs to handle array boundaries for the cursor.
        /// </summary>
        /// <param name="wOffset">x-value offset for the location of the container</param>
        /// <param name="hOffset">y-value offset for the location of the container</param>
        /// <param name="w">width of the container</param>
        /// <param name="h">height of the container</param>
        /// <param name="areaW">width of item in container</param>
        /// <param name="areaH">height of item in container</param>
        public Cursor(int wOffset, int hOffset, int w, int h, int areaW, int areaH)
        {
            widthOffset = wOffset;
            heighOffset = hOffset;
            areaWidth = areaW;
            areaHeight = areaH;
            width = w;
            height = h;
            cursorParts = new Texture2D[4];
            cursorSourceRects = new Rectangle[4];
            cursorDestRects = new Rectangle[4];
            cursorPos = new Vector2(0, 0);
        }

        /// <summary>
        /// Cursor constructor.  A cursor is meant to work on items in a list
        /// or an array where the edges of the items are touching.
        /// Boundary checking is handled with this cursor constructor.
        /// </summary>
        /// <param name="array">Integer array the cursor is to move over</param>
        /// <param name="wOffset">x-value offset for the location of the container</param>
        /// <param name="hOffset">y-value offset for the location of the container</param>
        /// <param name="areaW">width of item in container</param>
        /// <param name="areaH">height of item in container</param>
        public Cursor(int[][] arr, int wOffset, int hOffset, int areaW, int areaH)
        {
            array = arr;
            widthOffset = wOffset;
            heighOffset = hOffset;
            width = arr.GetLength(0);
            height = arr.GetLength(1);
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

        /// <summary>
        /// Listens to user input to move cursor
        /// If the cursor was created with the array parameter, this method
        /// will check if the cursor is within the bounds of the array.
        /// </summary>
        /// <param name="gameTime">GameTime sent from main class</param>
        public bool moveCursor(GameTime gameTime)
        {

            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);
            //cursorTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //if (cursorTimeElapsed >= cursorMoveDelay)
            //{
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && cursorPos.X != 0)
                {
                    Game1.PlaySound("cursor");
                    cursorPos.X--;
                    return true;
                }
                else if (keyboard.IsKeyDown(Keys.Right) && cursorPos.X != width - 1)
                {
                    Game1.PlaySound("cursor");
                    cursorPos.X++;
                    return true;
                }
                else if (keyboard.IsKeyDown(Keys.Up) && cursorPos.Y != 0)
                {
                    Game1.PlaySound("cursor");
                    cursorPos.Y--;
                    return true;
                }
                else if (keyboard.IsKeyDown(Keys.Down) && cursorPos.Y != height - 1)
                {
                    Game1.PlaySound("cursor");
                    cursorPos.Y++;
                    return true;
                }
                return false;
               // cursorTimeElapsed = 0;
           // }
        }

        /// <summary>
        /// Moves the cursor in a given direction
        /// </summary>
        /// <param name="dir">Direction to move the cursor: l = left, r = right, u = up, d = down</param>
        public void moveCursorDir(char dir)
        {
            if (dir != '0')
            {
                Game1.PlaySound("cursor");
            }
            switch(dir)
            {
                case 'l': cursorPos.X--; break;
                case 'r': cursorPos.X++; break;
                case 'u': cursorPos.Y--; break;
                case 'd': cursorPos.Y++; break;
            }
        }

        /// <summary>
        /// Draws the cursor given the main spriteBatch
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from main class</param>
        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < cursorParts.Length; i++)
            {
                spriteBatch.Draw(cursorParts[i], cursorDestRects[i], cursorSourceRects[i], Color.White);
            }

        }
    }
}