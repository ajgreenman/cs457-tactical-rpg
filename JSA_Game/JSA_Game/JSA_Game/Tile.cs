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
        private Boolean isHighlighted;
        public Boolean IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; }
        }

        public Tile()
        {
            landType = DEFAULT_LAND;
            isOccupied = false;
            isHighlighted = false;
        }

        public Tile(String lType)
        {
            landType = lType;
            isOccupied = false;
            isHighlighted = false;
        }
    }
}