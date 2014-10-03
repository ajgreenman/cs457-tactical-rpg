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
        const int STAT_POS = 390; 
        const int STR_POS = 330;
        const int ARMR_POS = 330;
        const int MAG_POS = 450;
        const int RES_POS = 450;
        const int DOD_POS = 390;

        //Vector2 Objects
        Vector2 stat_pos; 
        Vector2 str_pos;
        Vector2 armr_pos;
        Vector2 mag_pos;
        Vector2 res_pos;
        Vector2 dod_pos;

        //Stat Fonts
        SpriteFont fStatHeader;
        SpriteFont fStrength;
        SpriteFont fArmor;
        SpriteFont fMagic;
        SpriteFont fResistance;
        SpriteFont fDodge;

        //Stat Types
        int targetStrength;
        int targetArmor;
        int targetMagic;
        int targetResistance;
        int targetAccuracy;
        int targetDodge;
        int targetMovement;

        //Constructor
        public Stat_Section()
        {
            //Initializing Vector2 Objects for Draw Positions
            stat_pos = new Vector2(STAT_POS, 530);
            str_pos = new Vector2(STR_POS, 550);
            armr_pos = new Vector2(ARMR_POS, 570);
            mag_pos = new Vector2(MAG_POS, 550);
            res_pos = new Vector2(RES_POS, 570);
            dod_pos = new Vector2(DOD_POS, 590);
        }

        public void characterSelect(Character c)
        {
            targetStrength = c.Strength;
            targetArmor = c.Armor;
            targetMagic = c.Magic;
            targetResistance = c.Resist;
            targetAccuracy = c.Accuracy;
            targetDodge = c.Dodge;
            targetMovement = c.Movement;
        }

        //Loading Stat Fonts
        public void LoadContent(ContentManager Content)
        {
            fStatHeader = Content.Load<SpriteFont>("StatFont");
            fStrength = Content.Load<SpriteFont>("StatFont");
            fArmor = Content.Load<SpriteFont>("StatFont");
            fMagic = Content.Load<SpriteFont>("StatFont");
            fResistance = Content.Load<SpriteFont>("StatFont");
            fDodge = Content.Load<SpriteFont>("StatFont");
        }

        //Updating Stat Values and Corresponding Fonts
        public void update(GameTime gameTime)
        {

        }

        //Drawing Stat Fonts
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fStatHeader,"Stats", stat_pos, Color.Blue);
            spriteBatch.DrawString(fStrength,"STR: " + targetStrength, str_pos, Color.Blue);
            spriteBatch.DrawString(fArmor, "ARMR: " + targetArmor, armr_pos ,Color.Blue);
            spriteBatch.DrawString(fMagic, "MAG: " + targetMagic, mag_pos, Color.Blue);
            spriteBatch.DrawString(fResistance, "RES: " + targetResistance, res_pos, Color.Blue);
            spriteBatch.DrawString(fDodge, "Dodge: " + targetDodge, dod_pos, Color.Blue);
        }
    }
}
