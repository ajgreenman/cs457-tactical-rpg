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
        Effect_Section effectSection;
        Action_Button actionB;
        Wait_Button waitB;
        Item_Button itemB;
        Attack_Button attackB;
        Ability_Button abilityB;
        Exert_Button exertB;
        
        //Hides the HUD
        private Boolean hidden;
        //Shows HUD with stats and buttons
        private Boolean showStat;
        //Shows HUD with bars and buttons
        private Boolean showBars;
        //Shows HUD without buttons
        private Boolean showOriginal;
        //Show Action Buttons
        private Boolean showActionButtons;
        //Show Ability Buttons
        private Boolean showAbilityButtons;
        //Show Inventory
        private Boolean showInventory;

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
            effectSection = new Effect_Section();

            //Init Buttons
            actionB = new Action_Button();
            waitB = new Wait_Button();
            itemB = new Item_Button();
            attackB = new Attack_Button();
            abilityB = new Ability_Button();
            exertB = new Exert_Button();

            hidden = true;
            showOriginal = true;
            showStat = false;
            showBars = false;
            showActionButtons = false;
            showAbilityButtons = false;
        }

        public void LoadContent(ContentManager Content)
        {
            hudText = Content.Load<Texture2D>("brown-rectangle");

            //Loading Bars and Stat Section
            manaBar.LoadContent(Content);
            experienceBar.LoadContent(Content);
            healthBar.LoadContent(Content);
            statSection.LoadContent(Content);
            effectSection.LoadContent(Content);

            //Loading Buttons
            actionB.LoadContent(Content);
            waitB.LoadContent(Content);
            itemB.LoadContent(Content);
            attackB.LoadContent(Content);
            abilityB.LoadContent(Content);
            exertB.LoadContent(Content);
        }

        public void characterSelect(Character c)
        {
            //Updating Bars and Stat Section
            experienceBar.characterSelect(c);
            healthBar.characterSelect(c);
            manaBar.characterSelect(c);
            statSection.characterSelect(c);
            effectSection.characterSelect(c);
        }

        public void Draw(SpriteBatch spriteBatch)
        {   
            //Updating Stat Section Positions for Draw
            statSection.updatePositions(showOriginal);
            
            spriteBatch.Draw(hudText, hudRec, Color.MidnightBlue);
            if (hidden)
            {
                //Draws Buttons and Stat Section
                if (showStat)
                {
                    actionB.Draw(spriteBatch);
                    waitB.Draw(spriteBatch);
                    itemB.Draw(spriteBatch);
                    statSection.Draw(spriteBatch);
                    if (showActionButtons)
                    {
                        attackB.Draw(spriteBatch);
                        abilityB.Draw(spriteBatch);
                        exertB.Draw(spriteBatch);
                    }
                }

                //Draws Bars and Buttons
                if (showBars)
                {
                    experienceBar.Draw(spriteBatch);
                    manaBar.Draw(spriteBatch);
                    healthBar.Draw(spriteBatch);
                    actionB.Draw(spriteBatch);
                    waitB.Draw(spriteBatch);
                    itemB.Draw(spriteBatch);
                    if (showActionButtons)
                    {
                        attackB.Draw(spriteBatch);
                        abilityB.Draw(spriteBatch);
                        exertB.Draw(spriteBatch);
                    }
                }

                //Draws Bars and Stat Section
                if (showOriginal)
                {
                    experienceBar.Draw(spriteBatch);
                    manaBar.Draw(spriteBatch);
                    healthBar.Draw(spriteBatch);
                    statSection.Draw(spriteBatch);
                    effectSection.Draw(spriteBatch);
                }
            }
        }

        public Boolean Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public Boolean ShowBars
        {
            get { return showBars; }
            set { showBars = value; }
        }

        public Boolean ShowStat
        {
            get { return showStat; }
            set { showStat = value; }
        }

        public Boolean ShowOriginal
        {
            get { return showOriginal; }
            set { showOriginal = value; }
        }
    }
}
