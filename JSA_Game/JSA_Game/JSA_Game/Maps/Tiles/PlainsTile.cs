using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Maps.Tiles
{
    class PlainsTile : Tile
    {
        public PlainsTile()
        {
            LandType = "plain";
            IsOccupied = false;
            IsWalkable = true;
            IsAttackThroughable = true;
            IsDestructible = false;
        }
    }
}
