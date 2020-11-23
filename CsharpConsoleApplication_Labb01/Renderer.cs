using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class Renderer
    {
        static private char[] myRenderString;
        public Renderer()
        {

        }

        public static void Render(char[] aLevel)
        {
            myRenderString = aLevel;
        }

        public static void Render(char aGameObject,Vector2 aPos)
        {
            myRenderString[(int)aPos.X+(int)aPos.Y*Level.Width] = aGameObject;
        }
        
        public static void RenderCall()
        {
            Console.Write(myRenderString);
        }
    }
}
