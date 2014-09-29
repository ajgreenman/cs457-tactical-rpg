using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game
{
    class Action
    {
        private String name, statTarget;
        private Boolean ranged, spell, piercing, friendly;
        int power, mpCost;

        enum ActionType
        {
            Physical,
            Magical,
            Ranged
        }

        /// <summary>
        /// Describes any action that can be taken during a battle.
        /// </summary>
        /// <param name="name">Name of the action.</param>
        /// <param name="statTarget">The stat the action targets.</param>
        /// <param name="ranged">Whether or not the attack is ranged.</param>
        /// <param name="spell">Whether or not the attack is a spell.</param>
        /// <param name="piercing">Whether or not the attack ignores armor.</param>
        /// <param name="power">Power of the action.</param>
        /// <param name="mpCost">Mp cost, if any, of the action.</param>
        public Action(String name, String statTarget, Boolean ranged, Boolean spell, Boolean piercing, int power, int mpCost)
        {
            this.name = name;
            this.statTarget = statTarget;
            this.ranged = ranged;
            this.spell = spell;
            this.piercing = piercing;
            this.power = power;
            this.mpCost = mpCost;
        }

        // GETTERS AND SETTERS

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String StatTarget
        {
            get { return statTarget; }
            set { statTarget = value; }
        }

        public Boolean Ranged
        {
            get { return ranged; }
            set { ranged = value; }
        }

        public Boolean Spell
        {
            get { return spell; }
            set { spell = value; }
        }

        public Boolean Piercing
        {
            get { return piercing; }
            set { piercing = value; }
        }

        public Boolean Friendly
        {
            get { return friendly; }
            set { friendly = value; }
        }

        public int Power
        {
            get { return power; }
            set { power = value; }
        }

        public int MpCost
        {
            get { return mpCost; }
            set { mpCost = value; }
        }
    }
}
