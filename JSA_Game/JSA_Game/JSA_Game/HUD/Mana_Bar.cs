using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace JSA_Game.HUD
{
    class Mana_Bar
    {
        const int BAR_HEIGHT = 25;
        const int BAR_WIDTH = 250;
        private int targetMana;
        Texture2D manaBar;

        public Mana_Bar(int currentMana)
        {
            targetMana = currentMana;
        }

        public void LoadContent(ContentManager Content)
        {
            manaBar = Content.Load<Texture2D> ("bar_base");
        }

        public void update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(manaBar, new Rectangle(30, 542, BAR_WIDTH, BAR_HEIGHT), Color.White);
        }
    }
}
