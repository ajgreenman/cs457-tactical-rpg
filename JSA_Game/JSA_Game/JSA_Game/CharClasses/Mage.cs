using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.AI;
using JSA_Game.Maps;

namespace JSA_Game.CharClasses
{
    class Mage : Character
    {
        public Mage(Level level)
        {
            Texture = "enemy";
            AI = new AggressiveAI(this, level);
            Battle_Controller.Action actionAttack = new Battle_Controller.Action();
            Actions[0] = actionAttack;
        }

    }
}