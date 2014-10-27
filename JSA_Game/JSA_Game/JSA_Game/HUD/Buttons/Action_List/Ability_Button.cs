using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace JSA_Game.HUD
{
    class Ability_Button
    {
        //Button Image
        public Texture2D abilityText;
        //Size of Button
        public Vector2 abilitySize;
        //Button Position
        public Vector2 abilityPos;
        //Button Rectangle
        public Rectangle abilityRec;
        //Button Color
        public Color abilityCol;
        //Mouse Clicked
        public bool isClicked;
        //Mouse is On
        public bool mouseOn;

        public Ability_Button()
        {
            abilitySize = new Vector2(125, 25);
            abilityPos = new Vector2(175, 548);
            abilityRec = new Rectangle((int)abilityPos.X, (int)abilityPos.Y,
                 (int)abilitySize.X, (int)abilitySize.Y);
            abilityCol = new Color(255, 255, 255, 255);
        }

        public void LoadContent(ContentManager Content)
        {
            abilityText = Content.Load<Texture2D>("attackButton");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(abilityText, abilityRec, abilityCol);
        }
    }
}
