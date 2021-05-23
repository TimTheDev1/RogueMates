using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    interface IGameState
    {
        void Display();

        void HandleInput(ConsoleKey input);

        IGameState SwitchToScene();
    }
}
