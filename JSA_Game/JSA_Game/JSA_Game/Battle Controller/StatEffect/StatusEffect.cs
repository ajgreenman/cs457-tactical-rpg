using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Battle_Controller;

namespace JSA_Game.Battle_Controller.StatEffect
{
    abstract class StatusEffect
    {
        private string name;
        private string description;
        private int duration;
        private StatType[] affectedStats;
        private int[] amount;
        private string image;
        private bool friendly;

        public StatusEffect(string name, string descrip, int dur, StatType[] affected, int[] amount, string img, bool friendly)
        {
            this.name = name;
            description = descrip;
            duration = dur;
            affectedStats = affected;
            this.amount = amount;
            image = img;
            this.friendly = friendly;
        }



        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public StatType[] AffectedStats
        {
            get { return affectedStats; }
            set { affectedStats = value; }
        }
        public int[] Amount
        {
            get { return amount; }
            set { amount = value; }
 
        }
        public string Image
        {
            get { return image; }
            set { image = value; }

        }
        public bool Friendly
        {
            get { return friendly; }
            set { friendly = value; }
        }
    }
}
