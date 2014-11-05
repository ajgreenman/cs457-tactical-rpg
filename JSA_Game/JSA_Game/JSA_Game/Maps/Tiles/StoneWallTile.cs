using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Maps.Tiles
{
    class StoneWallTile : Tile
    {
        public StoneWallTile()
        {
            LandType = "stone_wall";
            IsOccupied = false;
            IsWalkable = false;
            IsAttackThroughable = false;
            IsDestructible = false;
        }
    }
}
