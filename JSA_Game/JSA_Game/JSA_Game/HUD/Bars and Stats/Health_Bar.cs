using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JSA_Game.HUD
{
    class Health_Bar
    {
        const int BAR_SIZE = 125;
        //Health Bar Graphic
        Texture2D healthBar;
        Vector2 healthSize;
        Vector2 healthPos;
        Rectangle healthRec;

        private int targetMaxHealth;
        private int targetCurrHealth;
        private int targetPercentHealth;

        public Health_Bar()
        {
            healthSize = new Vector2(BAR_SIZE, 25);
            healthPos = new Vector2(30, 507);
            healthRec = new Rectangle((int)healthPos.X, (int)healthPos.Y, (int)healthSize.X, (int)healthSize.Y);
        }

        public void characterSelect(Character c)
        {
            targetMaxHealth = c.MaxHP;
            targetCurrHealth = c.CurrHp;
            healthSize.X = BAR_SIZE * c.hpPercent;
            healthRec.Width = (int) healthSize.X;
        }

        public void LoadContent(ContentManager Content)
        {
           healthBar = Content.Load<Texture2D>("bar_base");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBar, healthRec, Color.Red);
        }
    }
}
