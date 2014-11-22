using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game.Screens
{
    class PlayerIndexEventArgs : EventArgs
    {
        PlayerIndex playerIndex;

        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
        }

        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
        }
    }
}
