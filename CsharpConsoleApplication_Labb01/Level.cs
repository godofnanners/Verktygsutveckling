using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
        }

        public char GetCharAtPos(int[] aPos)
        {
            return myLevel[aPos[0]+aPos[1]*myWidth];
        }
        
        public void Render()
        {
            Renderer.Render(myLevel);
        }
    }
}
