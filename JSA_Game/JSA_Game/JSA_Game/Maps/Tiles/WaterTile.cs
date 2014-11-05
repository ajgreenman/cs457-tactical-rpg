using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Maps.Tiles
{
    class WaterTile : Tile
    {
        public WaterTile()
        {
            LandType = "water";
            IsOccupied = false;
            IsWalkable = false;
            IsAttackThroughable = true;
            IsDestructible = false;
        }
    }
}
