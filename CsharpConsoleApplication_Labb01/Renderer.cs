using System;
using System.Collections.Generic;
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

        public static void Render(char aGameObject,int[] aPos)
        {
            myRenderString[aPos[0]+aPos[1]*Level.Width] = aGameObject;
        }

        public void RenderCall()
        {
            Console.Write(myRenderString);
        }
    }
}
