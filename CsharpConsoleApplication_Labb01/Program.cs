using System;

namespace CsharpConsoleApplication_Labb01
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            bool shouldPlay=true;
            while (shouldPlay)
            {
               shouldPlay = game.Update();
            }
        }
    }
}
