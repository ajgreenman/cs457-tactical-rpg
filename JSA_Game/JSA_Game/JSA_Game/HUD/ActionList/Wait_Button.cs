using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JSA_Game.HUD
{
    class Wait_Button
    {
        //Button Image
        public Texture2D waitText;
        //Size of Button
        public Vector2 waitSize;
        //Button Position
        public Vector2 waitPos;
        //Button Rectangle
        public Rectangle waitRec;
        //Button Color
        public Color waitCol;
        //Mouse Clicked
        public bool isClicked;
        //Mouse is On
        public bool mouseOn;

        public Wait_Button()
        {
            waitSize = new Vector2(125, 25);
            waitPos = new Vector2(340, 570);
            waitRec = new Rectangle((int)waitPos.X, (int)waitPos.Y,
                 (int)waitSize.X, (int)waitSize.Y);
            waitCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            waitText = Content.Load<Texture2D>("waitButton");
        }

        public bool mouseUpdate(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(waitRec))
            {
                System.Diagnostics.Debug.WriteLine("Mouse is hovering");
                if (waitCol.A == 255) mouseOn = false;
                if (waitCol.A == 0) mouseOn = true;
                if (mouseOn) waitCol.A += 3; else waitCol.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    System.Diagnostics.Debug.WriteLine("Button Clicked");
                    return true;
                }
            }

            else if (waitCol.A < 255)
            {
                waitCol.A += 3;
                isClicked = false;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(waitText, waitRec, waitCol);
        }

       
    }
}
