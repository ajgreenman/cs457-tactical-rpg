using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JSA_Game.CharClasses;


namespace JSA_Game.HUD
{
    class HUD_Controller
    {
        const int GRAPHIC_HEIGHT = 100;
        const int GRAPHIC_WIDTH = 500;

        Texture2D hud;
        Texture2D gameBorder;

        Health_Bar healthBar;
        Mana_Bar manaBar;
        Experience_Bar experienceBar;
        Stat_Section statSection;

        private Boolean hidden;

        public HUD_Controller()
        {
            healthBar = new Health_Bar();
            manaBar = new Mana_Bar();
            experienceBar = new Experience_Bar();
            statSection = new Stat_Section();

            hidden = true;
        }

        public void LoadContent(ContentManager Content)
        {
            gameBorder = Content.Load<Texture2D>("Border");
            manaBar.LoadContent(Content);
            experienceBar.LoadContent(Content);
            healthBar.LoadContent(Content);
            statSection.LoadContent(Content);
            hud = Content.Load<Texture2D>("brown-rectangle");
        }

        public void characterSelect(Character c)
        {
            experienceBar.characterSelect(c);
            healthBar.characterSelect(c);
            manaBar.characterSelect(c);        
            statSection.characterSelect(c);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hud, new Rectangle(0, 500, GRAPHIC_WIDTH, GRAPHIC_HEIGHT), Color.White);
            if (hidden)
            {
                experienceBar.Draw(spriteBatch);
                manaBar.Draw(spriteBatch);
                healthBar.Draw(spriteBatch);
                statSection.Draw(spriteBatch);
            }

        }

        public Boolean Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }
    }
}
