using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class GameOverGameState : IGameState
    {
        private IGameState nextState;

        public void Display()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(@"   _____                         ____                ");
            Console.WriteLine(@"  / ____|                       / __ \      ");
            Console.WriteLine(@" | |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __ ");
            Console.WriteLine(@" | | |_ |/ _` | '_ ` _ \ / _ \ | |  | \ \ / / _ \ '__|");
            Console.WriteLine(@" | |__| | (_| | | | | | |  __/ | |__| |\ V /  __/ |");
            Console.WriteLine(@"  \_____|\__,_|_| |_| |_|\___|  \____/  \_/ \___|_| ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"                      Points: {Program.points}");
            Console.WriteLine("");
            Console.WriteLine("                       Enter to go back to Main Menu");
        }

        public void HandleInput(ConsoleKey input)
        {
            if (input == ConsoleKey.Enter)
                nextState = new MainMenuGameState(false);
            else
                nextState = new GameOverGameState();
        }

        public IGameState SwitchToScene()
        {
            return nextState;
        }
    }
}
