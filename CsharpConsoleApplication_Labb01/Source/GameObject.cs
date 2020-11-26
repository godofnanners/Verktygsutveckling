using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class GameObject
    {
        protected Vector2 myPos;
        protected char myChar;
        protected GameObject(Vector2 aPos, char aChar)
        {
            myPos = aPos;
            myChar = aChar;
        }

        public void Render()
        {
            Renderer.Render(myChar, myPos);
        }
    }
}
