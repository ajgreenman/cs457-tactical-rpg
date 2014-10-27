using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game.Battle_Controller.StatEffect
{
    class Status : StatusEffect
    {
        public Status(string name, string descrip, int dur, StatType[] affected, int[] amount, string img, Color overlay) :
            base(name, descrip, dur, affected, amount, img, overlay)
        {

        }
    }
}
