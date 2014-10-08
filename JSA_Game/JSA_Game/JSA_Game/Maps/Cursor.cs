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

        Texture2D cursor;
        int cursorFrames = 0;

        Vector2 cursorPos;
        public Vector2 CursorPos
        {
            get { return cursorPos; }
            set { cursorPos = value; }
        }
        Rectangle cursorSourceRect;
        String cursorAnimation;

        float elapsedTime;
        float animateDelay = 500f;
        float cursorTimeElapsed;
        float cursorMoveDelay = 100;


        public Cursor()
        {
            cursorAnimation = "cursorAnim";

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
            cursorSourceRect = new Rectangle(cursor.Width / 2 * cursorFrames, 0, cursor.Width / 2, cursor.Height);
        }

        public void loadContent(ContentManager content)
        {
            cursor = content.Load<Texture2D>(cursorAnimation);
        }

        public void moveCursor(GameTime gameTime, Level level, Boolean selected)
        {

            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);
            cursorTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!selected && cursorTimeElapsed >= cursorMoveDelay)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && cursorPos.X != 0)
                {
                    cursorPos.X--;
                }
                else if (keyboard.IsKeyDown(Keys.Right) && cursorPos.X != level.BoardWidth - 1)
                {
                    cursorPos.X++;
                }
                else if (keyboard.IsKeyDown(Keys.Up) && cursorPos.Y != 0)
                {
                    cursorPos.Y--;
                }
                else if (keyboard.IsKeyDown(Keys.Down) && cursorPos.Y != level.BoardHeight - 1)
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
        public void draw(SpriteBatch spriteBatch, int startW, int startH, int tileSize)
        {
            spriteBatch.Draw(cursor, new Rectangle(startW + tileSize * (int)cursorPos.X, startH + tileSize * (int)cursorPos.Y, cursor.Width / 2, cursor.Height),
                                     cursorSourceRect, Color.White);

        }
    }
}