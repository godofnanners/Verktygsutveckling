using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
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

        protected Character(Vector2 aPos, char aChar) : base(aPos, aChar)
        {

        }

        public Vector2 Pos
        {
            get { return myPos; }
            set { myPos = value; }
        }

        protected void Move(Directions aDirection)
        {
            switch (aDirection)
            {
                case Directions.Up:
                    myPos.Y -= 1;
                    break;
                case Directions.Down:
                    myPos.Y += 1;
                    break;
                case Directions.Left:
                    myPos.X -= 1;
                    break;
                case Directions.Right:
                    myPos.X += 1;
                    break;
            }
        }
    }
}
