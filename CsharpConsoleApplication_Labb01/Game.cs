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
            int[] playerPos = new int[] { 2,2 };
            List<GameObject> gameObjects = new List<GameObject>();

            myLevel = new Level();
            myLevel.Init("Level.txt");
            myPlayer = new Player(playerPos,'P');


        }

        public void Update(ref bool aShouldPlay)
        {
            
            while (true)
            {

            }
        } 
    }
}
