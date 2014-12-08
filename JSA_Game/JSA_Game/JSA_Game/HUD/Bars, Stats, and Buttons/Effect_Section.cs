using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JSA_Game.HUD
{
    class Effect_Section
    {
        //Position Macros
        const int EFF_POSx = 375;
        const int BUF_POSx = 358;
        const int DBUF_POSx = 350;

        const int EFF_POSy = 502;
        const int BUF_POSy = 525;
        const int DBUF_POSy = 555;

        //Vector2 Objects
        Vector2 eff_pos;
        Vector2 buf_pos;
        Vector2 dbuf_pos;

        //Effect Fonts
        SpriteFont fEffectsHeader;
        SpriteFont fBuff;
        SpriteFont fDBuff;

        //Effect Types
        String targetBuff;
        String targetDBuff;

        public Effect_Section()
        {
            eff_pos = new Vector2(EFF_POSx, EFF_POSy);
            buf_pos = new Vector2(BUF_POSx, BUF_POSy);
            dbuf_pos = new Vector2(DBUF_POSx, DBUF_POSy);

            targetBuff = "None";
            targetDBuff = "None";
        }

        public void characterSelect(Character c)
        {
            if (c.Status[0] != null) { targetBuff = c.Status[0].Name; }
            else { targetBuff = "None"; }
            if (c.Status[1] != null) { targetDBuff = c.Status[1].Name; }
            else { targetDBuff = "None"; }
        }

        public void LoadContent(ContentManager Content)
        {
            fEffectsHeader = Content.Load<SpriteFont>("StatFont");
            fBuff = Content.Load<SpriteFont>("StatFont");
            fDBuff = Content.Load<SpriteFont>("StatFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fEffectsHeader, "Effects", eff_pos, Color.White);
            spriteBatch.DrawString(fBuff, "BUF: " + targetBuff, buf_pos, Color.White);
            spriteBatch.DrawString(fDBuff, "DBUF: " + targetDBuff, dbuf_pos, Color.White);
        }
    }
}
