using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class PickUp : GameObject
    {
        public PickUp(Vector2 aPos, char aChar) : base(aPos, aChar)
        {

        }

        public Vector2 Pos
        {
            get { return myPos; }
        }
    }
}
