using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class Game
    {
        private Level myLevel;
        private Player myPlayer;
        private Enemy[] myEnemies;
        private Stopwatch myStopWatch;
        float myLastFrameTime;
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
            myStopWatch = new Stopwatch();

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
            myStopWatch.Start();
            myLastFrameTime = 0;
        }

        public bool Update()
        {

            if (Console.KeyAvailable)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;
                myPlayer.Update(myLevel, keyPressed);
                if (keyPressed == ConsoleKey.Escape)
                {
                    return false;
                }
                Render();
            }


            if (1000 < myStopWatch.ElapsedMilliseconds - myLastFrameTime)
            {
                for (int i = 0; i < myEnemies.Length; i++)
                {
                    myEnemies[i].Update(myLevel);
                }
                myLastFrameTime = myStopWatch.ElapsedMilliseconds;

                Render();
            }





            return true;
        }

        void Render()
        {
            Console.Clear();
            myLevel.Init("Level.txt");
            myLevel.Render();
            foreach (Enemy enemy in myEnemies)
            {
                enemy.Render();
            }
            myPlayer.Render();
            Renderer.RenderCall();
        }
    }
}
