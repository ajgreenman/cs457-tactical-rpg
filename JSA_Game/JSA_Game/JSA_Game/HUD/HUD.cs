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
        //HUD Graphic 
        Vector2 hudSize;
        Vector2 hudPos;
        Rectangle hudRec;
        Texture2D hudText;

        //Assisting Objects
        Health_Bar healthBar;
        Mana_Bar manaBar;
        Experience_Bar experienceBar;
        Stat_Section statSection;
        Action_Button actionB;
        Wait_Button waitB;
        Item_Button itemB;
        

        private Boolean hidden;
        private Boolean statPage;
        private Boolean buttonPage;

        public HUD_Controller()
        {
            //Init Graphic Size
            hudSize = new Vector2(500, 100);
            hudPos = new Vector2(0, 500);
            hudRec = new Rectangle((int)hudPos.X, (int)hudPos.Y, (int)hudSize.X, (int)hudSize.Y);

            //Init Bars and Stat Section
            healthBar = new Health_Bar();
            manaBar = new Mana_Bar();
            experienceBar = new Experience_Bar();
            statSection = new Stat_Section();

            //Init Buttons
            actionB = new Action_Button();
            waitB = new Wait_Button();
            itemB = new Item_Button();

            hidden = true;
            statPage = true;
            buttonPage = false;
        }

        public void LoadContent(ContentManager Content)
        {
            hudText = Content.Load<Texture2D>("brown-rectangle");

            //Loading Bars and Stat Section
            manaBar.LoadContent(Content);
            experienceBar.LoadContent(Content);
            healthBar.LoadContent(Content);
            statSection.LoadContent(Content);

            //Loading Buttons
            actionB.LoadContent(Content);
            waitB.LoadContent(Content);
            itemB.LoadContent(Content);
        }

        public void characterSelect(Character c)
        {
            //Updating Bars and Stat Section
            experienceBar.characterSelect(c);
            healthBar.characterSelect(c);
            manaBar.characterSelect(c);
            statSection.characterSelect(c);
            //Updating Buttons
            actionB.characterSelect(c);
            waitB.characterSelect(c);
            itemB.characterSelect(c);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hudText, hudRec, Color.MidnightBlue);
            if (hidden)
            {
                if (statPage)
                {
                    //Drawing Bars and Stat Section
                    experienceBar.Draw(spriteBatch);
                    manaBar.Draw(spriteBatch);
                    healthBar.Draw(spriteBatch);
                    statSection.Draw(spriteBatch);
                }

                if (buttonPage)
                {
                    //Drawing Buttons
                    actionB.Draw(spriteBatch);
                    waitB.Draw(spriteBatch);
                    itemB.Draw(spriteBatch);
                }
            }
        }

        public Boolean Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public Boolean StatPage
        {
            get { return statPage; }
            set { statPage = value; }
        }

        public Boolean ButtonPage
        {
            get { return buttonPage; }
            set { buttonPage = value; }
        }
    }
}
