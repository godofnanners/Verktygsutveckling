using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class Enemy : Character
    {
        public Enemy(int[] aPos, char aChar) : base(aPos, aChar)
        {

        }

        public void HuntPlayer(int[] aPos)
        {
            int distanceX = Math.Abs(aPos[0]) + Math.Abs(myPos[0]);
            int distanceY = Math.Abs(aPos[1]) + Math.Abs(myPos[1]);

            if (distanceX>distanceY)
            {
                if (aPos[0] < myPos[0])
                {
                    Move(Directions.Left);
                }
                else if (aPos[0] > myPos[0])
                {
                    Move(Directions.Right);
                }
            }
            else
            {
                if (aPos[1] < myPos[1])
                {
                    Move(Directions.Down);
                }
                else if (aPos[1] > myPos[1])
                {
                    Move(Directions.Up);
                }
            }
        }
    }
}
