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
    class Button_Section
    {
        //Button Image
        public Texture2D waitText;
        public Texture2D itemText;
        public Texture2D actionText;
        //Size of Button
        public Vector2 waitSize;
        public Vector2 itemSize;
        public Vector2 actionSize;
        //Button Position
        public Vector2 waitPos;
        public Vector2 itemPos;
        public Vector2 actionPos;
        //Button Rectangle
        public Rectangle waitRec;
        public Rectangle itemRec;
        public Rectangle actionRec;
        //Button Color
        public Color waitCol;
        public Color itemCol;
        public Color actionCol;

        public Button_Section()
        {
            //Wait Button INIT
            waitSize = new Vector2(125, 25);
            waitPos = new Vector2(355, 548);
            waitRec = new Rectangle((int)waitPos.X, (int)waitPos.Y,
                 (int)waitSize.X, (int)waitSize.Y);
            waitCol = new Color(255, 255, 255, 255);

            //Item Button INIT
            itemSize = new Vector2(125, 25);
            itemPos = new Vector2(175, 548);
            itemRec = new Rectangle((int)itemPos.X, (int)itemPos.Y,
                 (int)itemSize.X, (int)itemSize.Y);
            itemCol = new Color(255, 255, 255, 255);

            //Action Button INIT
            actionSize = new Vector2(125, 25);
            actionPos = new Vector2(265, 507);
            actionRec = new Rectangle((int)actionPos.X, (int)actionPos.Y,
                 (int)actionSize.X, (int)actionSize.Y);
            actionCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            //Wait Button Load
            waitText = Content.Load<Texture2D>("waitButton");

            //Item Button Load
            itemText = Content.Load<Texture2D>("itemButton");

            //Action Button Load
            actionText = Content.Load<Texture2D>("actionButton");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Wait Button Draw
            spriteBatch.Draw(waitText, waitRec, waitCol);

            //Item Button Draw
            spriteBatch.Draw(itemText, itemRec, itemCol);

            //Action Button Draw
            spriteBatch.Draw(actionText, actionRec, actionCol);
        }
    }
}