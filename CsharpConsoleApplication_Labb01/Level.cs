using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;

namespace CsharpConsoleApplication_Labb01
{
    class Level
    {
        private char[] myLevel;
        private static int myWidth;
        int myHeight;
        public Level()
        {
        }

        public static int Width
        {
            get { return myWidth; }
        }

        public void Init(string aLevelPath)
        {
            myLevel = File.ReadAllText(aLevelPath).ToCharArray();
            myWidth = 55;
        }

        public char GetCharAtPos(Vector2 aPos)
        {
            return myLevel[(int)aPos.X+(int)aPos.Y*myWidth];
        }
        
        public void Render()
        {
            Renderer.Render(myLevel);
        }
    }
}
