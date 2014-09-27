using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace JSA_Game.HUD
{
    public class HUD_Controller
    {
        const int GRAPHIC_HEIGHT = 90;
        const int GRAPHIC_WIDTH = 560;

        Texture2D hud;

        Health_Bar healthBar;
        Mana_Bar manaBar;
        Stat_Section statSection;

        int targetHealth;
        int targetMana;
        int targetStrength;
        int targetArmor;
        int targetMagic;
        int targetResistance;
        int targetDodge;

        public HUD_Controller(int tH, int tMAN, int tS, int tA, int tMAG, int tR, int tD)
        {
            targetHealth = tH;
            targetMana = tMAN;
            targetStrength = tS;
            targetArmor = tA;
            targetMagic = tMAG;
            targetResistance = tR;
            targetDodge = tD;

            healthBar = new Health_Bar(targetHealth);
            manaBar = new Mana_Bar(targetMana);
            statSection = new Stat_Section(targetStrength, targetArmor, targetMagic, targetResistance, targetDodge);
        }

        public void LoadContent(ContentManager Content)
        {
            manaBar.LoadContent(Content);
            healthBar.LoadContent(Content);
            statSection.LoadContent(Content);
            hud = Content.Load<Texture2D>("brown-rectangle");
        }

        public void update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hud, new Rectangle(0, 530, GRAPHIC_WIDTH, GRAPHIC_HEIGHT), Color.White);
            manaBar.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
            statSection.Draw(spriteBatch);
        }
    }
}
