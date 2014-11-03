using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace JSA_Game.HUD
{
    class Bar_Section
    {
        //Bar Size Macro
        const int BAR_SIZE = 125;

        //Experience Bar Graphic
        Texture2D experienceBar;
        Vector2 experienceSize;
        Vector2 experiencePos;
        Rectangle experienceRec;
        SpriteFont experienceFont;
        Vector2 experiencef_pos;
        
        //Health Bar Graphic
        Texture2D healthBar;
        Vector2 healthSize;
        Vector2 healthPos;
        Rectangle healthRec;
        SpriteFont healthFont;
        Vector2 healthf_pos;

        //Mana Bar Graphic
        Texture2D manaBar;
        Vector2 manaSize;
        Vector2 manaPos;
        Rectangle manaRec;
        SpriteFont manaFont;
        Vector2 manaf_pos;

        //Target Values
        private int targetCurrExperience;
        private int targetMaxHealth;
        private int targetCurrHealth;
        private int targetMaxMana;
        private int targetCurrMana;
        

        public Bar_Section()
        {
            //Experience Bar INIT
            experienceSize = new Vector2(BAR_SIZE, 25);
            experiencePos = new Vector2(30, 570);
            experienceRec = new Rectangle((int)experiencePos.X, (int)experiencePos.Y, (int)experienceSize.X, (int)experienceSize.Y);
            experiencef_pos = new Vector2(55, 574);
            
            //Health Bar INIT
            healthSize = new Vector2(BAR_SIZE, 25);
            healthPos = new Vector2(30, 507);
            healthRec = new Rectangle((int)healthPos.X, (int)healthPos.Y, (int)healthSize.X, (int)healthSize.Y);
            healthf_pos = new Vector2(55, 510);

            //Mana Bar INIT
            manaSize = new Vector2(BAR_SIZE, 25);
            manaPos = new Vector2(30, 538);
            manaRec = new Rectangle((int)manaPos.X, (int)manaPos.Y, (int)manaSize.X, (int)manaSize.Y);
            manaf_pos = new Vector2(55, 542);
        }

        public void characterSelect(Character c)
        {
            //Experience Select
            targetCurrExperience = c.CurrExp;
            experienceSize.X = BAR_SIZE * c.expPercent;
            experienceRec.Width = (int)experienceSize.X;

            //Health Select
            targetMaxHealth = c.MaxHP;
            targetCurrHealth = c.CurrHp;
            healthSize.X = BAR_SIZE * c.hpPercent;
            healthRec.Width = (int)healthSize.X;

            //Mana Select
            targetMaxMana = c.MaxMP;
            targetCurrMana = c.CurrMp;
            manaSize.X = BAR_SIZE * c.mpPercent;
            manaRec.Width = (int)manaSize.X;

        }

        public void LoadContent(ContentManager Content)
        {
           //Experience Load
           experienceBar = Content.Load<Texture2D>("bar_base");
           experienceFont = Content.Load<SpriteFont>("StatFont");

           //Health Load
           healthBar = Content.Load<Texture2D>("bar_base");
           healthFont = Content.Load<SpriteFont>("StatFont");

           //Mana Load
           manaBar = Content.Load<Texture2D>("bar_base");
           manaFont = Content.Load<SpriteFont>("StatFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Expereience Draw
            spriteBatch.Draw(experienceBar, experienceRec, Color.Green);
            spriteBatch.DrawString(experienceFont, "XP: " + targetCurrExperience, experiencef_pos, Color.White);
        
            //Health Draw
            spriteBatch.Draw(healthBar, healthRec, Color.Red);
            spriteBatch.DrawString(healthFont, "HP: " + targetCurrHealth + "/" + targetMaxHealth, healthf_pos, Color.White);

            //Mana Draw
            spriteBatch.Draw(manaBar, manaRec, Color.Blue);
            spriteBatch.DrawString(manaFont, "MP: " + targetCurrMana + "/" + targetMaxMana, manaf_pos, Color.White);
        }
    }
}

