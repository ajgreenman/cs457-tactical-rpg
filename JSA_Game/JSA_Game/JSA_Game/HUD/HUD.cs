using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JSA_Game.CharClasses;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


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
        Bar_Section barSection;
        Stat_Section statSection;
        Effect_Section effectSection;
        Button_Section buttonSection;

        //Hides the HUD
        private Boolean hidden;
        //Shows HUD with stats and buttons
        private Boolean showStat;
        //Shows HUD with bars and buttons
        private Boolean showBars;
        //Shows HUD without buttons
        private Boolean showOriginal;

        private Character selectedChar;

        public HUD_Controller()
        {
            //Init Graphic Size
            hudSize = new Vector2(500, 100);
            hudPos = new Vector2(0, 500);
            hudRec = new Rectangle((int)hudPos.X, (int)hudPos.Y, (int)hudSize.X, (int)hudSize.Y);

            //Init Bars, Buttons, and Stat Section
            barSection = new Bar_Section();
            statSection = new Stat_Section();
            effectSection = new Effect_Section();
            buttonSection = new Button_Section();

            //INIT Display Values
            hidden = true;
            showOriginal = true;
            showStat = false;
            showBars = false;
        }

        public void LoadContent(ContentManager Content)
        {
            hudText = Content.Load<Texture2D>("brown-rectangle");

            //Loading Bars, Buttons and Stat/Effect Section
            barSection.LoadContent(Content);
            statSection.LoadContent(Content);
            effectSection.LoadContent(Content);
            buttonSection.LoadContent(Content);
        }

        public void characterSelect(Character c)
        {
            //Updating Bars, Buttons, and Stat/Effect Section
            barSection.characterSelect(c);
            statSection.characterSelect(c);
            effectSection.characterSelect(c);
            buttonSection.CharacterSelect(c);
            selectedChar = c;
        }

        public void ButtonSelect(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.F1) || !selectedChar.MoveDisabled)
            {
                showOriginal = true;
                showBars = false;
                showStat = false;
            }
            if (keyboard.IsKeyDown(Keys.F2) || selectedChar.MoveDisabled)
            {
                showOriginal = false;
                showBars = true;
                showStat = false;
            }
            if (keyboard.IsKeyDown(Keys.F3))
            {
                showOriginal = false;
                showBars = false;
                showStat = true;
            }
            buttonSection.ButtonSelect(keyboard);
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
                    buttonSection.Draw(spriteBatch);
                    statSection.Draw(spriteBatch);
                }

                //Draws Bars and Buttons
                if (showBars)
                {
                    barSection.Draw(spriteBatch);
                    buttonSection.Draw(spriteBatch);
                }

                //Draws Bars and Stat Section
                if (showOriginal)
                {
                    barSection.Draw(spriteBatch);
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
    }
}
