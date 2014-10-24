using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace JSA_Game.HUD
{
    class Experience_Bar
    {
        const int BAR_SIZE = 125;

        //Experience Bar Graphic
        Texture2D experienceBar;
        Vector2 experienceSize;
        Vector2 experiencePos;
        Rectangle experienceRec;


        private int targetCurrExperience;
        

        public Experience_Bar()
        {
            experienceSize = new Vector2(BAR_SIZE, 25);
            experiencePos = new Vector2(30, 570);
            experienceRec = new Rectangle((int)experiencePos.X, (int)experiencePos.Y, (int)experienceSize.X, (int)experienceSize.Y);
        }

        public void characterSelect(Character c)
        {
            targetCurrExperience = c.CurrExp;
            experienceSize.X = BAR_SIZE * c.expPercent;
            experienceRec.Width = (int)experienceSize.X;
        }

        public void LoadContent(ContentManager Content)
        {
           experienceBar = Content.Load<Texture2D>("bar_base");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(experienceBar, experienceRec, Color.Green);
        }
    }
}

