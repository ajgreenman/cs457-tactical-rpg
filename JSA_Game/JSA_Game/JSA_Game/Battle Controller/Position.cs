using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Battle_Controller
{
    class Position
    {
        int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
