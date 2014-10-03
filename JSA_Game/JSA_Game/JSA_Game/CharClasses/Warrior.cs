using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.CharClasses
{
    class Warrior : Character
    {
        
        public Warrior()
        {
            Texture = "player";

            Battle_Controller.Action actionAttack = new Battle_Controller.Action();
            actions[0] = actionAttack;
        }

        
    }
}
