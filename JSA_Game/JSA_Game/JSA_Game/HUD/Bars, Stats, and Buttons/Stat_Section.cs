using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JSA_Game.HUD
{
    class Stat_Section
    {
        //Position Macros
        const int STAT_POSx = 225; 
        const int STR_POSx = 175;
        const int ARMR_POSx = 175;
        const int MAG_POSx = 265;
        const int RES_POSx = 265;
        const int DOD_POSx = 175;
        const int ACC_POSx = 265;
        const int LVL_POSx = 225;

        const int STAT_POSy = 502;
        const int STR_POSy= 520;
        const int ARMR_POSy = 540;
        const int MAG_POSy = 520;
        const int RES_POSy = 540;
        const int DOD_POSy = 560;
        const int ACC_POSy = 560;
        const int LVL_POSy = 580;

        //Stat Background
        Texture2D statBackground;
        Rectangle backgroundRec;
        Vector2 backgroundSize;
        Vector2 backgroundPos;

        //Vector2 Objects
        Vector2 stat_pos; 
        Vector2 str_pos;
        Vector2 armr_pos;
        Vector2 mag_pos;
        Vector2 res_pos;
        Vector2 dod_pos;
        Vector2 acc_pos;
        Vector2 lvl_pos;

        //Stat Fonts
        SpriteFont fStatHeader;
        SpriteFont fStrength;
        SpriteFont fArmor;
        SpriteFont fMagic;
        SpriteFont fResistance;
        SpriteFont fDodge;
        SpriteFont fAccuracy;
        SpriteFont flvl;

        //Stat Colors
        Color statColor;
        Color strColor;
        Color armrColor;
        Color magColor;
        Color resColor;
        Color dodColor;
        Color accColor;

        //Stat Values
        int targetStrength;
        int targetArmor;
        int targetMagic;
        int targetResistance;
        int targetAccuracy;
        int targetDodge;
        int targetlvl;

        //Constructor
        public Stat_Section()
        {
            //Initializing Vector2 Objects for Draw Positions
            stat_pos = new Vector2(STAT_POSx, STAT_POSy);
            str_pos = new Vector2(STR_POSx, STR_POSy);
            armr_pos = new Vector2(ARMR_POSx, ARMR_POSy);
            mag_pos = new Vector2(MAG_POSx, MAG_POSy);
            res_pos = new Vector2(RES_POSx, RES_POSy);
            dod_pos = new Vector2(DOD_POSx, DOD_POSy);
            acc_pos = new Vector2(ACC_POSx, ACC_POSy);
            lvl_pos = new Vector2(LVL_POSx, LVL_POSy);

            //Initializing Stat Colors
            strColor = Color.White;
            statColor = Color.White;
            armrColor = Color.White; 
            magColor = Color.White; 
            resColor = Color.White; 
            dodColor = Color.White; 
            accColor = Color.White;

            //Initializing Stat Background
            backgroundSize = new Vector2(345, 500);
            backgroundPos = new Vector2(155, 500);
            backgroundRec = new Rectangle((int)backgroundPos.X, (int)backgroundPos.Y, (int)backgroundSize.X, (int)backgroundSize.Y);
        }

        //Getting Character Values
        public void characterSelect(Character c)
        {
            targetStrength = c.Strength;
            targetArmor = c.Armor;
            targetMagic = c.Magic;
            targetResistance = c.Resist;
            targetAccuracy = c.Accuracy;
            targetDodge = c.Dodge;
            targetlvl = c.Level;


            /*
             * Below Contains the Logic for Updating the Stat Color if 
             * affected by a Buff, Debuff, or Both.
             */

            //Updating if Buff
            if (c.Status[0] != null)
            {
                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Strength)) { strColor = Color.Green; }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Armor)) { armrColor = Color.Green; }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Magic)) { magColor = Color.Green; }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Resist)) { resColor = Color.Green; }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Dodge)) { dodColor = Color.Green; }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Accuracy)) { accColor = Color.Green; }
            }

            //Updating if Debuff
            if (c.Status[1] != null)
            {
                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Strength)) { strColor = Color.Red; }

                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Armor)) { armrColor = Color.Red; }

                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Magic)) { magColor = Color.Red; }

                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Resist)) { resColor = Color.Red; }

                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Dodge)) { dodColor = Color.Red; }

                if (c.Status[1].AffectedStats.Contains<StatType>(StatType.Accuracy)) { accColor = Color.Red; }
            }

            //Comparing BUff and Debuff for Greatest Effect
            if (c.Status[0] != null && c.Status[1] != null)
            {
                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Strength) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Strength))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Strength) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Strength) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { strColor = Color.Red; }
                    else { strColor = Color.Green; }
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Armor) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Armor))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Armor) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Armor) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { armrColor = Color.Red; }
                    else { armrColor = Color.Green; }
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Magic) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Magic))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Magic) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Magic) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { magColor = Color.Red; }
                    else { magColor = Color.Green; }
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Resist) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Resist))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Resist) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Resist) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { resColor = Color.Red; }
                    else { resColor = Color.Green; }
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Dodge) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Dodge))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Dodge) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Dodge) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { dodColor = Color.Red; }
                    else { dodColor = Color.Green; }
                }

                if (c.Status[0].AffectedStats.Contains<StatType>(StatType.Accuracy) && c.Status[1].AffectedStats.Contains<StatType>(StatType.Accuracy))
                {
                    int BuffPosition = 0;
                    int DebuffPosition = 0;
                    for (int i = 0; i < c.Status[0].AffectedStats.Length; i++) { if (c.Status[0].AffectedStats[i] == StatType.Accuracy) { BuffPosition = i; } }
                    for (int i = 0; i < c.Status[1].AffectedStats.Length; i++) { if (c.Status[1].AffectedStats[i] == StatType.Accuracy) { DebuffPosition = i; } }

                    if (c.Status[0].Amount.ElementAt<int>(BuffPosition) < c.Status[1].Amount.ElementAt<int>(DebuffPosition)) { accColor = Color.Red; }
                    else { accColor = Color.Green; }
                }
            }

            /*
             *  Below Contains the logic for resetting Stat Colors when not affected
             *  by a Buff or Debuff
             */ 

            //If no Effect Present Reset
            if (c.Status[0] == null && c.Status[1] == null)
            {
                strColor = Color.White;
                statColor = Color.White;
                armrColor = Color.White;
                magColor = Color.White;
                resColor = Color.White;
                dodColor = Color.White;
                accColor = Color.White;
            }

            //If Debuff Present (Attempt to Reset Correct Values)
            if (c.Status[0] == null && c.Status[1] != null)
            {
                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Strength)) { strColor = Color.White; }

                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Armor)) { armrColor = Color.White; }

                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Magic)) { magColor = Color.White; }

                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Resist)) { resColor = Color.White; }

                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Dodge)) { dodColor = Color.White; }

                if (!c.Status[1].AffectedStats.Contains<StatType>(StatType.Accuracy)) { accColor = Color.White; }
            }

            //If Buff Present (Attempt to Reset Correct Values)
            if (c.Status[0] != null && c.Status[1] == null)
            {
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Strength)) { strColor = Color.White; }

                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Armor)) { armrColor = Color.White; }

                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Magic)) { magColor = Color.White; }

                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Resist)) { resColor = Color.White; }

                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Dodge)) { dodColor = Color.White; }

                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Accuracy)) { accColor = Color.White; }
            }

            //If Buff and Debuff Present (Attempt to Reset Correct Values)
            if (c.Status[0] != null && c.Status[1] != null)
            {
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Strength) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Strength)) { strColor = Color.White; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Armor) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Armor)) { armrColor = Color.White; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Magic) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Magic)) { magColor = Color.White; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Resist) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Resist)) { resColor = Color.White; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Dodge) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Dodge)) { dodColor = Color.White; }
                if (!c.Status[0].AffectedStats.Contains<StatType>(StatType.Accuracy) && !c.Status[1].AffectedStats.Contains<StatType>(StatType.Accuracy)) { accColor = Color.White; }
            }
        }

        //Loading Stat Fonts and Effect Fonts
        public void LoadContent(ContentManager Content)
        {
            fStatHeader = Content.Load<SpriteFont>("StatFont");
            fStrength = Content.Load<SpriteFont>("StatFont");
            fArmor = Content.Load<SpriteFont>("StatFont");
            fMagic = Content.Load<SpriteFont>("StatFont");
            fResistance = Content.Load<SpriteFont>("StatFont");
            fDodge = Content.Load<SpriteFont>("StatFont");
            fAccuracy = Content.Load<SpriteFont>("StatFont");
            flvl = Content.Load<SpriteFont>("StatFont");

            //Background Load
            statBackground = Content.Load<Texture2D>("statBackground");
        }

        //Drawing Stat Fonts and Effect Fonts
        public void Draw(SpriteBatch spriteBatch)
        {
            //Background Draw
            spriteBatch.Draw(statBackground, backgroundRec, Color.White);

            //Stat Draw
            spriteBatch.DrawString(fStatHeader, "Stats", stat_pos, Color.White);
            spriteBatch.DrawString(fStrength, "STR: " + targetStrength, str_pos, strColor);
            spriteBatch.DrawString(fArmor, "ARM: " + targetArmor, armr_pos, armrColor);
            spriteBatch.DrawString(fMagic, "MAG: " + targetMagic, mag_pos, magColor);
            spriteBatch.DrawString(fResistance, "RES: " + targetResistance, res_pos, resColor);
            spriteBatch.DrawString(fDodge, "DOD: " + targetDodge, dod_pos, dodColor);
            spriteBatch.DrawString(fAccuracy, "ACC: " + targetAccuracy, acc_pos, accColor);
            spriteBatch.DrawString(flvl, "LVL: " + targetlvl, lvl_pos, Color.White);
        }
    }
}
