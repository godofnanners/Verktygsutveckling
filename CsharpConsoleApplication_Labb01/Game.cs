using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class Game
    {
        private Level myLevel;
        private Player myPlayer;
        private Enemy[] myEnemies;
        public Game()
        {
            myLevel = new Level();
            myLevel.Init("Level.txt");
        }

        public void Update(ref bool aShouldPlay)
        {
            
            while (true)
            {

            }
        } 
    }
}
