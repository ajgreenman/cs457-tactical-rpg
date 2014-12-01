using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;

namespace JSA_Game.AI
{
    public interface iAI
    {
        void move(GameTime gameTime);
        void attack();
    }
}
