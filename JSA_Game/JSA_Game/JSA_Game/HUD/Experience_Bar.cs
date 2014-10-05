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

        const int BAR_HEIGHT = 25;
        const int BAR_WIDTH = 125;

        private int targetCurrExperience;
        Texture2D experienceBar;

        public Experience_Bar()
        {
            
        }

        public void characterSelect(Character c)
        {
            //targetCurrExperience = c.
        }

        public void LoadContent(ContentManager Content)
        {
           experienceBar = Content.Load<Texture2D>("bar_base");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(experienceBar, new Rectangle(30, 570, BAR_WIDTH, BAR_HEIGHT), Color.White);
        }
    }
}

