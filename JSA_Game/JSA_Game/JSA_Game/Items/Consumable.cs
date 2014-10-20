using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    class Consumable : Item
    {
        private int uses;
        private Battle_Controller.Action action;

        public Consumable(String name, int worth, int uses, Battle_Controller.Action action) :
            base(name, worth)
        {
            this.uses = uses;
            this.action = action;
        }

        public int Uses
        {
            get { return uses; }
            set { uses = value; }
        }

        public Battle_Controller.Action Action {
            get { return action; }
            set { action = value; }
        }
    }
}
