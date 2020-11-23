using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
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
            Vector2 playerPos = new Vector2(2, 2);
            
            bool shouldPlay = true;
            myLevel = new Level();
            myLevel.Init("Level.txt");
            myPlayer = new Player(playerPos, 'P');
            myEnemies = new Enemy[2];
            myEnemies[0] = new Enemy(new Vector2(40, 14), 'E');
            myEnemies[1] = new Enemy(new Vector2(41, 18), 'E');

            foreach (Enemy enemy in myEnemies)
            {
                enemy.Init(myPlayer);
            }
            
            myLevel.Render();
            foreach (Enemy enemy in myEnemies)
            {
                enemy.Render();
            }
            myPlayer.Render();
            Renderer.RenderCall();
        }

        public bool Update()
        {
            ConsoleKey keyPressed = Console.ReadKey().Key;
            if (myPlayer.Update(myLevel,keyPressed))
            {
                for (int i = 0; i < myEnemies.Length; i++)
                {
                    myEnemies[i].Update(myLevel);
                }
            }

            Console.Clear();
            myLevel.Init("Level.txt");
            myLevel.Render();
            foreach (Enemy enemy in myEnemies)
            {
                enemy.Render();
            }
            myPlayer.Render();
            Renderer.RenderCall();

            if (keyPressed == ConsoleKey.Escape)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
