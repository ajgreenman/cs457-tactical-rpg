using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JSA_Game.CharClasses;
using Microsoft.Xna.Framework.Input;


namespace JSA_Game.HUD
{
    class HUD_Controller
    {
        const int GRAPHIC_HEIGHT = 100;
        const int GRAPHIC_WIDTH = 500;

        Texture2D hud;

        Health_Bar healthBar;
        Mana_Bar manaBar;
        Experience_Bar experienceBar;
        Stat_Section statSection;
        Attack_Button attackB;
        Item_Button itemB;
        Wait_Button waitB;

        private Boolean hidden;

        public HUD_Controller()
        {
            //Init Bars and Stat Section
            healthBar = new Health_Bar();
            manaBar = new Mana_Bar();
            experienceBar = new Experience_Bar();
            statSection = new Stat_Section();

            //Init Buttons
            attackB = new Attack_Button();
            itemB = new Item_Button();
            waitB = new Wait_Button();

            hidden = true;
        }

        public void LoadContent(ContentManager Content)
        {
            hud = Content.Load<Texture2D>("brown-rectangle");

            //Loading Bars and Stat Section
            manaBar.LoadContent(Content);
            experienceBar.LoadContent(Content);
            healthBar.LoadContent(Content);
            statSection.LoadContent(Content);


            //Loading Buttons
            attackB.LoadContent(Content);
            itemB.LoadContent(Content);
            waitB.LoadContent(Content);
        }

        public bool mouseUpdate()
        {
            MouseState mouse = Mouse.GetState();

            //Updating if Button Pressed
            if (attackB.mouseUpdate(mouse) || itemB.mouseUpdate(mouse) || waitB.mouseUpdate(mouse)) return true;

            return false;
        }

        public void characterSelect(Character c)
        {
            //Updating Bars and Stat Section
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
                //Drawing Bars and Stat Section
                experienceBar.Draw(spriteBatch);
                manaBar.Draw(spriteBatch);
                healthBar.Draw(spriteBatch);
                statSection.Draw(spriteBatch);

                //Drawing Buttons
                attackB.Draw(spriteBatch);
                itemB.Draw(spriteBatch);
                waitB.Draw(spriteBatch);
            }

        }

        public Boolean Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }
    }
}
