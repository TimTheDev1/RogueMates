using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class VictoryGameState : IGameState
    {
        private Enemy enemy;
        private IGameState nextState;

        public VictoryGameState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void Display()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Clear();

            Console.WriteLine(@" __      _______ _____ _______ ____  _______     __");
            Console.WriteLine(@" \ \    / /_   _/ ____|__   __/ __ \|  __ \ \   / /");
            Console.WriteLine(@"  \ \  / /  | || |       | | | |  | | |__) \ \_/ /");
            Console.WriteLine(@"   \ \/ /   | || |       | | | |  | |  _  / \   / ");
            Console.WriteLine(@"    \  /   _| || |____   | | | |__| | | \ \  | | ");
            Console.WriteLine(@"     \/   |_____\_____|  |_|  \____/|_|  \_\ |_|");
            Console.WriteLine("");
            Console.WriteLine($"         You defeated the {enemy.creatureName}!");
            Console.WriteLine("");
            Console.WriteLine($"                      Gold: {Program.Gold}");
            Console.WriteLine($"                      Points: {Program.points}");
            Console.WriteLine("");
            Console.WriteLine($"                      Enter to continue");
        }

        public void HandleInput(ConsoleKey input)
        {
            if (input == ConsoleKey.Enter)
            {
                if (enemy.boss)
                    nextState = new MainMenuGameState(false);
                else
                    nextState = new ExplorationGameState();
            }
            else
                nextState = new VictoryGameState(enemy);
        }

        public IGameState SwitchToScene()
        {
            return nextState;
        }
    }
}
