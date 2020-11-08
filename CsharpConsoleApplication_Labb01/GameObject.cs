using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class GameObject
    {
        protected int[] myPos;
        protected char myChar;
        protected GameObject(int[] aPos, char aChar)
        {
            myPos = aPos;
            myChar = aChar;
        }
    }
}
