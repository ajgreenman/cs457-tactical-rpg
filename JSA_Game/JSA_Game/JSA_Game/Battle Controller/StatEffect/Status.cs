using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;

namespace JSA_Game.Battle_Controller.StatEffect
{
    public class Status : StatusEffect
    {
        public Status(string name, string descrip, int dur, Level level, StatType[] affected, int[] amount, string img, bool friendly, bool turnByTurn) :
            base(name, descrip, dur, level,  affected, amount, img, friendly, turnByTurn)
        {

        }
    }
}
