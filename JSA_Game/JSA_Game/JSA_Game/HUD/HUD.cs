using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JSA_Game.HUD
{
    public class HUD
    {
        private int targetHealth;
        private int targetMana;
        private int targetStrength;
        private int targetArmor;
        private int targetResistance;
        private int targetMagic;

        public HUD()
        {
            //Future - Load Player's stats into this constructor for call
            targetHealth = 100;
            targetMana = 100;
            targetStrength = 5;
            targetArmor = 5;
            targetResistance = 5;
            targetMagic = 5;
        }

        public void LoadContent()
        {

        }

        public void update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
