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
        const int MOV_POSx = 220;
        const int EFF_POSx = 375;
        const int BUF_POSx = 358;
        const int DBUF_POSx = 350;

        const int STAT_POSy = 502;
        const int STR_POSy= 520;
        const int ARMR_POSy = 540;
        const int MAG_POSy = 520;
        const int RES_POSy = 540;
        const int DOD_POSy = 560;
        const int ACC_POSy = 560;
        const int MOV_POSy = 580;
        const int EFF_POSy = 502;
        const int BUF_POSy = 525;
        const int DBUF_POSy = 555;

        //Vector2 Objects
        Vector2 stat_pos; 
        Vector2 str_pos;
        Vector2 armr_pos;
        Vector2 mag_pos;
        Vector2 res_pos;
        Vector2 dod_pos;
        Vector2 acc_pos;
        Vector2 mov_pos;
        Vector2 eff_pos;
        Vector2 buf_pos;
        Vector2 dbuf_pos;

        //Stat and Effect Fonts
        SpriteFont fStatHeader;
        SpriteFont fStrength;
        SpriteFont fArmor;
        SpriteFont fMagic;
        SpriteFont fResistance;
        SpriteFont fDodge;
        SpriteFont fAccuracy;
        SpriteFont fMovement;
        SpriteFont fEffectsHeader;
        SpriteFont fBuff;
        SpriteFont fDBuff;

        //Stat and Effect Types
        int targetStrength;
        int targetArmor;
        int targetMagic;
        int targetResistance;
        int targetAccuracy;
        int targetDodge;
        int targetMovement;
        String targetBuff;
        String targetDBuff;

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
            mov_pos = new Vector2(MOV_POSx, MOV_POSy);
            eff_pos = new Vector2(EFF_POSx, EFF_POSy);
            buf_pos = new Vector2(BUF_POSx, BUF_POSy);
            dbuf_pos = new Vector2(DBUF_POSx, DBUF_POSy);
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
            targetMovement = c.Movement;
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
            fMovement = Content.Load<SpriteFont>("StatFont");
            fEffectsHeader = Content.Load<SpriteFont>("StatFont");
            fBuff = Content.Load<SpriteFont>("StatFont");
            fDBuff = Content.Load<SpriteFont>("StatFont");
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
            spriteBatch.DrawString(fMovement, "MOV: " + targetMovement, mov_pos, Color.White);
            spriteBatch.DrawString(fEffectsHeader, "Effects", eff_pos, Color.White);
            spriteBatch.DrawString(fBuff, "BUF: " + targetBuff, buf_pos, Color.White);
            spriteBatch.DrawString(fDBuff, "DBUF: " + targetDBuff, dbuf_pos, Color.White);
        }
    }
}
