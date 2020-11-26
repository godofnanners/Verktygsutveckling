using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.IO;

namespace CsharpConsoleApplication_Labb01
{
    class Game
    {
        private Level myLevel;
        private Player myPlayer;
        private List<Enemy> myEnemies;
        private List<PickUp> myPickUps;
        private Stopwatch myStopWatch;
        float myLastFrameTime;
        public Game()
        {
            Vector2 playerPos = new Vector2(2, 2);

            bool shouldPlay = true;
            myLevel = new Level();
            myLevel.Init("Level.txt");
            myPlayer = new Player(playerPos, 'P');
            myEnemies = new List<Enemy>();
            myPickUps = new List<PickUp>();
            myEnemies.Add(new Enemy(new Vector2(40, 14), 'E'));
            myEnemies.Add(new Enemy(new Vector2(41, 18), 'E'));
            myPickUps.Add(new PickUp(new Vector2(20, 15), 'O'));
            myPickUps.Add(new PickUp(new Vector2(20, 20), 'O'));
            myPickUps.Add(new PickUp(new Vector2(10, 10), 'O'));
            myPickUps.Add(new PickUp(new Vector2(50, 2), 'O'));
            myPickUps.Add(new PickUp(new Vector2(30, 10), 'O'));


            myStopWatch = new Stopwatch();

            foreach (Enemy enemy in myEnemies)
            {
                enemy.Init(myPlayer);
            }

            Render();
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
                else if (keyPressed == ConsoleKey.S)
                {
                    File.WriteAllText("SavedGame.txt", new string(Renderer.RenderString));
                }
                else if (keyPressed == ConsoleKey.L)
                {
                    Load();
                }
                Render();
            }


            if (1000 < myStopWatch.ElapsedMilliseconds - myLastFrameTime)
            {
                for (int i = 0; i < myEnemies.Count; i++)
                {
                    myEnemies[i].Update(myLevel);
                }
                myLastFrameTime = myStopWatch.ElapsedMilliseconds;

                Render();
            }

            for (int i = 0; i < myPickUps.Count; i++)
            {
                if (myPickUps[i].Pos==myPlayer.Pos)
                {
                    myPickUps.RemoveAt(i);
                    i--;
                }
            }

            if (myPickUps.Count==0)
            {
                Console.Clear();
                Console.WriteLine("Win!");
                return false;
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
            foreach (PickUp pickup in myPickUps)
            {
                pickup.Render();
            }
            myPlayer.Render();
            Renderer.RenderCall();
        }

        void Load()
        {
            myPickUps.Clear();
            myEnemies.Clear();
            myLevel.Init("SavedGame.txt");
            for (int i = 0; i < myLevel.LevelSize; i++)
            {
                if (myLevel.GetCharAtIndex(i)=='P')
                {
                    int yPos = i / Level.Width;
                    int xPos = i % Level.Width;

                    myPlayer.Pos = new Vector2(xPos, yPos);
                }
                else if (myLevel.GetCharAtIndex(i) == 'E')
                {
                    int yPos = i / Level.Width;
                    int xPos = i % Level.Width;

                    myEnemies.Add(new Enemy(new Vector2(xPos, yPos), 'E'));
                }
                else if (myLevel.GetCharAtIndex(i) == 'O')
                {
                    int yPos = i / Level.Width;
                    int xPos = i % Level.Width;

                    myPickUps.Add(new PickUp(new Vector2(xPos, yPos), 'O'));
                }
            }
            foreach (Enemy enemy in myEnemies)
            {
                enemy.Init(myPlayer);
            }

        }
    }
}
