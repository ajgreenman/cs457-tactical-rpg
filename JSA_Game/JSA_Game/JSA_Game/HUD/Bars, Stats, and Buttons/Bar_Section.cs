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

        //Bar Background
        Texture2D barBackground;
        Rectangle backgroundRec;
        Vector2 backgroundSize;
        Vector2 backgroundPos;

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

        //Mana and Health Bar Color
        Color manaColor;
        Color healthColor;

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
            experiencePos = new Vector2(15, 570);
            experienceRec = new Rectangle((int)experiencePos.X, (int)experiencePos.Y, (int)experienceSize.X, (int)experienceSize.Y);
            experiencef_pos = new Vector2(40, 574);
            
            //Health Bar INIT
            healthSize = new Vector2(BAR_SIZE, 25);
            healthPos = new Vector2(15, 507);
            healthRec = new Rectangle((int)healthPos.X, (int)healthPos.Y, (int)healthSize.X, (int)healthSize.Y);
            healthf_pos = new Vector2(40, 510);
            healthColor = Color.Red;

            //Mana Bar INIT
            manaSize = new Vector2(BAR_SIZE, 25);
            manaPos = new Vector2(15, 538);
            manaRec = new Rectangle((int)manaPos.X, (int)manaPos.Y, (int)manaSize.X, (int)manaSize.Y);
            manaf_pos = new Vector2(40, 542);
            manaColor = Color.Blue;

            //Bar Background INIT
            backgroundSize = new Vector2(155, 500);
            backgroundPos = new Vector2(0, 500);
            backgroundRec = new Rectangle((int)backgroundPos.X, (int)backgroundPos.Y, (int)backgroundSize.X, (int)backgroundSize.Y);
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

            /*
             * Below Contains the Logic for Updating the Bar Color if 
             * affected by a Buff, Debuff, or Both.
             */

            //Updating if Health or Mana Buff
            if (c.Status[0] != null)
            {
                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Hp))
                {
                    healthColor = Color.DarkRed;
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Mp))
                {
                    manaColor = Color.DarkBlue;
                }
            }

            //Updating if Health or Mana Debuff
            if (c.Status[1] != null)
            {
                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Hp))
                {
                    healthColor = Color.Purple;
                }

                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Mp))
                {
                    manaColor = Color.Brown;
                }
            }

            //Comparing Buff and Debuff for Greatest Effect
            if (c.Status[0] != null && c.Status[1] != null)
            {
                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Hp) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Hp))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Hp) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Hp) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { healthColor = Color.Purple; }
                    else { healthColor = Color.DarkRed; }
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Mp) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Mp))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Mp) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Mp) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { manaColor = Color.Brown; }
                    else { manaColor = Color.DarkBlue; }
                }
            }

            /*
             *  Below Contains the logic for resetting Stat Colors when not affected
             *  by a Buff or Debuff
             */

            //If no Effect Present Reset
            if (c.Status[0] == null && c.Status[1] == null)
            {
                healthColor = Color.Red;
                manaColor = Color.Blue;
            }

            //If Debuff Present (Attempt to Reset Correct Values)
            if (c.Status[0] == null && c.Status[1] != null)
            {
                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Hp)) { healthColor = Color.Red; }
                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Mp)) { manaColor = Color.Blue; }
            }

            //If Buff Present (Attempt to Reset Correct Values)
            if (c.Status[0] != null && c.Status[1] == null)
            {
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Hp)) { healthColor = Color.Red; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Mp)) { manaColor = Color.Blue; }
            }

            //If Buff and Debuff Present (Attempt to Reset Correct Values)
            if (c.Status[0] != null && c.Status[1] != null)
            {
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Hp) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Hp)) { healthColor = Color.Red; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Mp) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Mp)) { manaColor = Color.Blue; }
            }
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

           //Background Load
            barBackground = Content.Load<Texture2D>("barBackground");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Background Draw
            spriteBatch.Draw(barBackground, backgroundRec, Color.White);

            //Expereience Draw
            spriteBatch.Draw(experienceBar, experienceRec, Color.Green);
            spriteBatch.DrawString(experienceFont, "XP: " + targetCurrExperience, experiencef_pos, Color.White);
        
            //Health Draw
            spriteBatch.Draw(healthBar, healthRec, healthColor);
            spriteBatch.DrawString(healthFont, "HP: " + targetCurrHealth + "/" + targetMaxHealth, healthf_pos, Color.White);

            //Mana Draw
            spriteBatch.Draw(manaBar, manaRec, manaColor);
            spriteBatch.DrawString(manaFont, "MP: " + targetCurrMana + "/" + targetMaxMana, manaf_pos, Color.White);
        }
    }
}

