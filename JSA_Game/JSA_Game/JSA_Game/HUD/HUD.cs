using System;
using System.Collections;
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
        ArrayList charList;

        //Assisting Objects
        Bar_Section barSection;
        Stat_Section statSection;
        Effect_Section effectSection;
        Button_Section buttonSection;
        CombatText comText;

        //Hides the HUD
        private Boolean hidden;

        public enum HUDState
        {
            showActions,
            showOriginal
        }

        public HUDState hState;

        private Character selectedChar;

        public HUD_Controller(ArrayList charList)
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
            comText = new CombatText(charList);

            //INIT Display Values
            hidden = true;
            hState = HUDState.showOriginal;

            //INIT List of Characters
            this.charList = charList;
        }

        public void LoadContent(ContentManager Content)
        {
            hudText = Content.Load<Texture2D>("brown-rectangle");

            //Loading Bars, Buttons and Stat/Effect Section
            barSection.LoadContent(Content);
            statSection.LoadContent(Content);
            effectSection.LoadContent(Content);
            buttonSection.LoadContent(Content);
            comText.LoadContent(Content);
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
            if (keyboard.IsKeyDown(Keys.F1)) {hState = HUDState.showOriginal;}
            if (keyboard.IsKeyDown(Keys.F2)) {hState = HUDState.showActions;}
         
            buttonSection.ButtonSelect(keyboard);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hudText, hudRec, Color.MidnightBlue);
            comText.Draw(spriteBatch);

            if (hidden)
            {
                //Draws Bars and Buttons
                if (hState == HUDState.showActions && !selectedChar.IsEnemy)
                {
                    barSection.Draw(spriteBatch);
                    buttonSection.Draw(spriteBatch);
                }

                //Draws Bars and Stat Section
                if (hState == HUDState.showOriginal || selectedChar.IsEnemy)
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
