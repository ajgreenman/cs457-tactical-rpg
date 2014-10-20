using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game
{
    class Tile
    {
        private const String DEFAULT_LAND = "plain";
        private String landType;
        //private String moveImage;
        private Boolean isOccupied;
        private Boolean isHighlighted;
        private Boolean isSelected;


        //Variables used in pathfinding
        private int fScore;
        private int gScore;
        private Vector2 pathParent;
        
        public Tile()
        {
            landType = DEFAULT_LAND;
            initialize();
        }

        public Tile(String lType)
        {
            landType = lType;
            initialize();
        }

        private void initialize()
        {
            isOccupied = false;
            isHighlighted = false;
            isSelected = false;
            //moveImage = "";
            
        }

        public String LandType
        {
            get { return landType; }
            set { landType = value; }
        }
        //public String MoveImage
        //{
        //    get { return moveImage; }
        //    set { moveImage = value; }
        //}
        public Boolean IsOccupied
        {
            get { return isOccupied; }
            set { isOccupied = value; }
        }
        public Boolean IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; }
        }
        public Boolean IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        public int FScore
        {
            get { return fScore; }
            set { fScore = value; }
        }
        public int GScore
        {
            get { return gScore; }
            set { gScore = value; }
        }
        public Vector2 PathParent
        {
            get { return pathParent; }
            set { pathParent = value; }
        }
        
    }
}