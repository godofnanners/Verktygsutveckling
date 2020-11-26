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
        private static int myHeight;
        public Level()
        {
        }

        public static int Width
        {
            get { return myWidth; }
        }

        public static int Height
        {
            get { return myWidth; }
        }

        public int LevelSize
        {
            get { return myLevel.Length; }
        }

        public void Init(string aLevelPath)
        {
            myLevel = File.ReadAllText(aLevelPath).ToCharArray();
            myWidth = 55;
            myHeight = 21;
        }

        public char GetCharAtPos(Vector2 aPos)
        {
            return myLevel[(int)aPos.X+(int)aPos.Y*myWidth];
        }
        
        public char GetCharAtIndex(int aIndex)
        {
            return myLevel[aIndex];
        }

        public void Render()
        {
            Renderer.Render(myLevel);
        }
    }
}
