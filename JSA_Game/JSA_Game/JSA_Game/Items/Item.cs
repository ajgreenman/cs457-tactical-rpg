using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    [Serializable]
    public abstract class Item
    {
        protected String name;
        protected int worth;

        public Item(String name, int worth)
        {
            this.name = name;
            this.worth = worth;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Worth
        {
            get { return worth; }
            set { worth = value; }
        }
    }
}
