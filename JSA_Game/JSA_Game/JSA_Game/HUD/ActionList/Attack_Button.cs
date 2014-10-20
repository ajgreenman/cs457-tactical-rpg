using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace JSA_Game.HUD
{
    class Attack_Button
    {
        //Button Image
        public Texture2D attackText;
        //Size of Button
        public Vector2 attackSize;
        //Button Position
        public Vector2 attackPos;
        //Button Rectangle
        public Rectangle attackRec;
        //Button Color
        public Color attackCol;
        //Mouse Clicked
        public bool isClicked;
        //Mouse is On
        public bool mouseOn;

        public Attack_Button()
        {
            attackSize = new Vector2(125, 25);
            attackPos = new Vector2(340, 507);
            attackRec = new Rectangle((int)attackPos.X, (int)attackPos.Y,
                 (int)attackSize.X, (int)attackSize.Y);
            attackCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            attackText = Content.Load<Texture2D>("attackButton");
        }

        public bool mouseUpdate(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(attackRec))
            {
                System.Diagnostics.Debug.WriteLine("Mouse is hovering");
                if (attackCol.A == 255) mouseOn = false;
                if (attackCol.A == 0) mouseOn = true;
                if (mouseOn) attackCol.A += 3; else attackCol.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    System.Diagnostics.Debug.WriteLine("Button Clicked");
                    isClicked = true;
                    return true;
                }
            }

            else if (attackCol.A < 255)
            {
                attackCol.A += 3;
                isClicked = false;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(attackText, attackRec, attackCol);
        }
    }
}
