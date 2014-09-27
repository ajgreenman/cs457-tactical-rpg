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
        const int BAR_HEIGHT = 25;
        const int BAR_WIDTH = 250;

        private int targetHealth;
        Texture2D healthBar;

        public Health_Bar(int currentHealth)
        {
            targetHealth = currentHealth;
        }

        public void LoadContent(ContentManager Content)
        {
           healthBar = Content.Load<Texture2D>("bar_base");
        }

        public void update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBar, new Rectangle(30, 585, BAR_WIDTH, BAR_HEIGHT), Color.White);
        }
    }
}
