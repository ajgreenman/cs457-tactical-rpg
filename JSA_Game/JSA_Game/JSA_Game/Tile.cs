using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game
{
    class Tile
    {
        private const String DEFAULT_LAND = "Plain";
        private String landType {get; set;}
        private Boolean occupied;

        public Tile()
        {
            landType = DEFAULT_LAND;
        }

        public Tile(String lType)
        {
            landType = lType;
        }
    }
}
