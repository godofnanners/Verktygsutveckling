using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CsharpConsoleApplication_Labb01
{
    class Enemy : Character
    {
        Player myPrey;
        public Enemy(Vector2 aPos, char aChar) : base(aPos, aChar)
        {

        }

        public void Init(Player aPrey)
        {
            myPrey = aPrey;
        }

        public void Update(Level aLevel)
        {
            int distanceX = (int)(Math.Abs(myPrey.Pos.X - myPos.X));
            int distanceY = (int)(Math.Abs(myPrey.Pos.Y - myPos.Y));

            CheckCollisionWithPrey();

            if (distanceX > distanceY)
            {
                if (myPrey.Pos.X < myPos.X && (aLevel.GetCharAtPos(new Vector2(myPos.X - 1, myPos.Y)) == ' '))
                {
                    Move(Directions.Left);
                }
                else if (myPrey.Pos.X > myPos.X && (aLevel.GetCharAtPos(new Vector2(myPos.X + 1, myPos.Y)) == ' '))
                {
                    Move(Directions.Right);
                }
            }
            else
            {
                if (myPrey.Pos.Y > myPos.Y && (aLevel.GetCharAtPos(new Vector2(myPos.X, myPos.Y + 1)) == ' '))
                {
                    Move(Directions.Down);
                }
                else if (myPrey.Pos.Y < myPos.Y && (aLevel.GetCharAtPos(new Vector2(myPos.X, myPos.Y - 1)) == ' '))
                {
                    Move(Directions.Up);
                }
            }

            CheckCollisionWithPrey();
        }

        private bool CheckCollisionWithPrey()
        {
            if (myPos==myPrey.Pos)
            {
                Console.Clear();
                Console.WriteLine("Dead!");
                Console.ReadKey();
                Environment.Exit(0);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
