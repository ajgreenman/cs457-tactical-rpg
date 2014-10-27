using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JSA_Game.HUD
{
    class Item_Button
    {
        //Button Image
        public Texture2D itemText;
        //Size of Button
        public Vector2 itemSize;
        //Button Position
        public Vector2 itemPos;
        //Button Rectangle
        public Rectangle itemRec;
        //Button Color
        public Color itemCol;
        //Mouse Clicked
        public bool isClicked;
        //Mouse is On
        public bool mouseOn;

        public Item_Button()
        {
            itemSize = new Vector2(125, 25);
            itemPos = new Vector2(175, 548);
            itemRec = new Rectangle((int)itemPos.X, (int)itemPos.Y,
                 (int)itemSize.X, (int)itemSize.Y);
            itemCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            itemText = Content.Load<Texture2D>("itemButton");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemText, itemRec, itemCol);
        }

    }
}
