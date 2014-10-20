using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game.CharClasses
{
    class Warrior : Character
    {
        public Warrior(Level level)
        {
            Texture = "player";
            AI = new AggressiveAI(this, level);
            Battle_Controller.Action actionAttack = new Battle_Controller.Action();
            Actions[0] = actionAttack;
        }

    }
}
