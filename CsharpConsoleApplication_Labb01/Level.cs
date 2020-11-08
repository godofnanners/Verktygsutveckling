using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CsharpConsoleApplication_Labb01
{
    class Level
    {
        private string myLevel;
        int myWidth;
        int myHeight;
        public Level()
        {
        }

        public void Init(string aLevelPath)
        {
            myLevel = File.ReadAllText(aLevelPath);
        }

        public char GetCharAtPos(int[] aPos)
        {
            return myLevel[aPos[0]+aPos[1]*myWidth];
        }
    }
}
