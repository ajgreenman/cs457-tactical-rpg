using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JSA_Game.HUD
{
    class CombatText
    {
        //Target Position
        Vector2 targetPos;

        //Text Font
        SpriteFont floatingText;

        //Text Position
        Vector2 floatingPos;

        //Text Color
        Color floatingColor;

        //Booleans
        bool missed;
        bool hit;


        public CombatText()
        {
            targetPos = new Vector2(0, 0);
            floatingPos = new Vector2(0, 0);
            floatingColor = Color.White;
        }

        public void characterSelect(Character c)
        {
            targetPos = c.Pos;
        }

        public void LoadContent(ContentManager Content)
        {
            floatingText = Content.Load<SpriteFont>("StatFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
