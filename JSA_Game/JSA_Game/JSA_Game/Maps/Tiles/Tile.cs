using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps.State;

namespace JSA_Game.Maps.Tiles
{
    public class Tile
    {
        

        private const String DEFAULT_LAND = "plain";
        private String landType;

        //Booleans for tile characteristics
        private Boolean isOccupied;
        private Boolean isWalkable;
        private Boolean isAttackThroughable;
        private Boolean isDestructible;

        //private Boolean isHighlighted;
        private HighlightState hlState;
        private Boolean isSelected;
        private Character occupant;

        //Variables used in pathfinding
        private int fScore;
        private int gScore;
        private Vector2 pathParent;
        
        public Tile()
        {
            hlState = HighlightState.NONE;
            //initialize();
        }

        //public Tile(String lType)
        //{
        //    landType = lType;
        //    initialize();
        //}

        private void initialize()
        {
            isOccupied = false;
            isWalkable = false;
            isAttackThroughable = false;
            isDestructible = false;

            //isHighlighted = false;
            isSelected = false; 
        }

        public String LandType
        {
            get { return landType; }
            set { landType = value; }
        }
        public Boolean IsOccupied
        {
            get { return isOccupied; }
            set { isOccupied = value; }
        }
        //public Boolean IsHighlighted
        //{
        //    get { return isHighlighted; }
        //    set { isHighlighted = value; }
        //}
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
        public Character Occupant
        {
            get { return occupant; }
            set { occupant = value; }
        }
        public Boolean IsWalkable
        {
            get { return isWalkable; }
            set { isWalkable = value; }
        }
        public Boolean IsAttackThroughable
        {
            get { return isAttackThroughable; }
            set { isAttackThroughable = value; }
        }
        public Boolean IsDestructible
        {
            get { return isDestructible; }
            set { isDestructible = value; }
        }

        public HighlightState HlState
        {
            get { return hlState; }
            set { hlState = value; }
        }
    }
}