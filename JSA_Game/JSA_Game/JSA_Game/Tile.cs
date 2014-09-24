using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game
{
    class Tile
    {
        private const String DEFAULT_LAND = "plain";
        private String landType;
        private Boolean isOccupied;
        public Boolean IsOccupied
        {
            get { return isOccupied; }
            set { isOccupied = value; }
        }

        public Tile()
        {
            landType = DEFAULT_LAND;
            isOccupied = false;
        }

        public Tile(String lType)
        {
            landType = lType;
            isOccupied = false;
        }
    }
}
