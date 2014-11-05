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
        
        //Position Update Macros
        const int STAT_POSx2 = 75;
        const int STR_POSx2 = 25;
        const int ARMR_POSx2 = 25;
        const int MAG_POSx2 = 115;
        const int RES_POSx2 = 115;
        const int DOD_POSx2 = 25;
        const int ACC_POSx2 = 115;
        const int LVL_POSx2 = 75;

        //Vector2 Objects
        Vector2 stat_pos; 
        Vector2 str_pos;
        Vector2 armr_pos;
        Vector2 mag_pos;
        Vector2 res_pos;
        Vector2 dod_pos;
        Vector2 acc_pos;
        Vector2 lvl_pos;

        //Stat and Effect Fonts
        SpriteFont fStatHeader;
        SpriteFont fStrength;
        SpriteFont fArmor;
        SpriteFont fMagic;
        SpriteFont fResistance;
        SpriteFont fDodge;
        SpriteFont fAccuracy;
        SpriteFont flvl;

        //Stat and Effect Types
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
        }

        public void updatePositions(Boolean original)
        {
            if (!original)
            {
                stat_pos.X = STAT_POSx2;
                str_pos.X = STR_POSx2;
                armr_pos.X = ARMR_POSx2;
                mag_pos.X = MAG_POSx2;
                res_pos.X = RES_POSx2;
                dod_pos.X = DOD_POSx2;
                acc_pos.X = ACC_POSx2;
                lvl_pos.X = LVL_POSx2;
            }

            else
            {
                stat_pos.X = STAT_POSx;
                str_pos.X = STR_POSx;
                armr_pos.X = ARMR_POSx;
                mag_pos.X = MAG_POSx;
                res_pos.X = RES_POSx;
                dod_pos.X = DOD_POSx;
                acc_pos.X = ACC_POSx;
                lvl_pos.X = LVL_POSx;
            }
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
            //targetBuff = c.Buff;
            //targetDBuff = c.DBuff;
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
        }

        //Drawing Stat Fonts and Effect Fonts
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fStatHeader, "Stats", stat_pos, Color.White);
            spriteBatch.DrawString(fStrength, "STR: " + targetStrength, str_pos, Color.White);
            spriteBatch.DrawString(fArmor, "ARM: " + targetArmor, armr_pos, Color.White);
            spriteBatch.DrawString(fMagic, "MAG: " + targetMagic, mag_pos, Color.White);
            spriteBatch.DrawString(fResistance, "RES: " + targetResistance, res_pos, Color.White);
            spriteBatch.DrawString(fDodge, "DOD: " + targetDodge, dod_pos, Color.White);
            spriteBatch.DrawString(fAccuracy, "ACC: " + targetAccuracy, acc_pos, Color.White);
            spriteBatch.DrawString(flvl, "LVL: " + targetlvl, lvl_pos, Color.White);
        }
    }
}
