using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace JSA_Game.HUD
{
    class Action_Button
    {
        //Button Image
        public Texture2D actionText;
        //Size of Button
        public Vector2 actionSize;
        //Button Position
        public Vector2 actionPos;
        //Button Rectangle
        public Rectangle actionRec;
        //Button Color
        public Color actionCol;
        //Mouse Clicked
        public bool isClicked;

        public Action_Button()
        {
            actionSize = new Vector2(125, 25);
            actionPos = new Vector2(265, 507);
            actionRec = new Rectangle((int)actionPos.X, (int)actionPos.Y,
                 (int)actionSize.X, (int)actionSize.Y);
            actionCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            actionText = Content.Load<Texture2D>("actionButton");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(actionText, actionRec, actionCol);
        }
    }
}
