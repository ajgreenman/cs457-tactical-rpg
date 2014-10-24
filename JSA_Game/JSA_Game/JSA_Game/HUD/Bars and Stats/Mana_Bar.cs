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
        const int BAR_SIZE = 125;

        //Mana Bar Graphic
        Texture2D manaBar;
        Vector2 manaSize;
        Vector2 manaPos;
        Rectangle manaRec;

        private int targetMaxMana;
        private int targetCurrMana;
        

        public Mana_Bar()
        {
            manaSize = new Vector2(BAR_SIZE, 25);
            manaPos = new Vector2(30, 538);
            manaRec = new Rectangle((int)manaPos.X, (int)manaPos.Y, (int)manaSize.X, (int)manaSize.Y);
        }

        public void characterSelect(Character c)
        {
            targetMaxMana = c.MaxMP;
            targetCurrMana = c.CurrMp;
            manaSize.X = BAR_SIZE * c.mpPercent;
            manaRec.Width = (int)manaSize.X;
        }

        public void LoadContent(ContentManager Content)
        {
            manaBar = Content.Load<Texture2D> ("bar_base");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(manaBar, manaRec, Color.Blue);
        }
    }
}
