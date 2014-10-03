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
        const int GRAPHIC_HEIGHT = 150;
        const int GRAPHIC_WIDTH = 900;

        Texture2D hud;
        Texture2D gameBorder;

        Health_Bar healthBar;
        Mana_Bar manaBar;
        //experienceBar;
        Stat_Section statSection;

        private Boolean hidden;

        public HUD_Controller()
        {


            healthBar = new Health_Bar();
            manaBar = new Mana_Bar();
            //experienceBar = new Experience_Bar()

            statSection = new Stat_Section();

            hidden = true;
        }


        public void LoadContent(ContentManager Content)
        {
            gameBorder = Content.Load<Texture2D>("Border");
            manaBar.LoadContent(Content);
            healthBar.LoadContent(Content);
            statSection.LoadContent(Content);
            hud = Content.Load<Texture2D>("brown-rectangle");
        }

        public void characterSelect(Character c)
        {
            healthBar.characterSelect(c);
            manaBar.characterSelect(c);        
            statSection.characterSelect(c);
        }
        public void update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(gameBorder, new Rectangle(0, 0, 560, 620), Color.White);
            spriteBatch.Draw(hud, new Rectangle(0, 500, GRAPHIC_WIDTH, GRAPHIC_HEIGHT), Color.White);
            if (!hidden)
            {
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
