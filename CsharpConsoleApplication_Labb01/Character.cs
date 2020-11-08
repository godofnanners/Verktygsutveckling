using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class Character :  GameObject
    {
        public enum Directions
        {
            Up,
            Down,
            Right,
            Left
        }

        protected Character(int[] aPos, char aChar) : base(aPos, aChar)
        {

        }

        protected void Move(Directions aDirection)
        {
            switch (aDirection)
            {
                case Directions.Up:
                    myPos[1] -= 1;
                    break;
                case Directions.Down:
                    myPos[1] += 1;
                    break;
                case Directions.Left:
                    myPos[0] -= 1;
                    break;
                case Directions.Right:
                    myPos[0] += 1;
                    break;
            }
        }
    }
}
