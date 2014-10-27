using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace JSA_Game.HUD
{
    class Exert_Button
    {
         //Button Image
        public Texture2D exertText;
        //Size of Button
        public Vector2 exertSize;
        //Button Position
        public Vector2 exertPos;
        //Button Rectangle
        public Rectangle exertRec;
        //Button Color
        public Color exertCol;
        //Mouse Clicked
        public bool isClicked;
        //Mouse is On
        public bool mouseOn;

        public Exert_Button()
        {
            exertSize = new Vector2(125, 25);
            exertPos = new Vector2(355, 548);
            exertRec = new Rectangle((int)exertPos.X, (int)exertPos.Y,
                 (int)exertSize.X, (int)exertSize.Y);
            exertCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            exertText = Content.Load<Texture2D>("attackButton");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(exertText, exertRec, exertCol);
        }
    }
}
