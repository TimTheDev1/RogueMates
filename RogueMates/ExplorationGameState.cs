using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class ExplorationGameState: IGameState
    {
        private IGameState nextState;
        private Room room;

        public ExplorationGameState()
        {
            room = new Room();
        }

        public ExplorationGameState(Room room)
        {
            this.room = room;
        }

        public void Display()
        {
            bool canGoUp = room.North == null ? false : true;
            bool canGoDown = room.South == null ? false : true;
            bool canGoRight = room.East == null ? false : true;
            bool canGoLeft = room.West == null ? false : true;

            if (room.hasEvent)
            {
                EventIdentifier(room);
            }

            string[] information = new string[Program.information.Length];

            Console.Clear();

            if (canGoUp)
            { 
                information[0] = $"           |           |          ";                 
                information[1] = $"           |           |          ";                 
                information[2] = $"           |     {room.North.eventSymbol}     |          "; 
                information[3] = $"           |           |          "; 
            }
            else
            {
                information[0] = $"                                  "; 
                information[1] = $"                                  "; 
                information[2] = $"                                  "; 
                information[3] = $"            ___________           "; 
            }

            if (canGoLeft && canGoRight)
            {
                information[4] = $"___________|           |____________";
                information[5] = $"                                    ";
                information[6] = $"     {room.West.eventSymbol}                       {room.East.eventSymbol}    ";
                information[7] = $"                                    ";
                information[8] = $"___________             ____________";
            }
            else if (canGoLeft && !canGoRight)
            {
                information[4] = $"___________|           |          ";
                information[5] = $"                       |          ";
                information[6] = $"     {room.West.eventSymbol}                 |          ";
                information[7] = $"                       |          ";
                information[8] = $"___________            |          ";
            }
            else if (!canGoLeft && canGoRight)
            {
                information[4] = $"           |           |___________";
                information[5] = $"           |                       ";
                information[6] = $"           |                 {room.East.eventSymbol}    ";
                information[7] = $"           |                       ";
                information[8] = $"           |            ___________";
            }
            else
            {
                information[4] = $"           |           |          ";
                information[5] = $"           |           |          ";
                information[6] = $"           |           |          ";
                information[7] = $"           |           |          ";
                information[8] = $"           |           |          ";
            }

            if (canGoDown)
            {
                information[9] = $"           |           |          "; 
                information[10] = $"           |           |          ";
                information[11] = $"           |     {room.South.eventSymbol}     |          "; 
                information[12] = $"           |           |          ";
                information[13] = $"           |           |          ";
            }
            else
            {
                information[9] = $"           |___________|          ";
                information[10] = $"                                  ";
                information[11] = $"                                  "; 
                information[12] = $"                                  ";
                information[13] = $"                                  ";
            }

            information[14] = " ";
            information[15] = " Legend: ";
            information[16] = " X = Combat";
            information[17] = " ? = Random Event";
            information[18] = " $ = Treasure";
            information[19] = " P = Potion Store";
            information[20] = " ";
            information[21] = " ";
            information[22] = " Use the arrow keys to move. Enter to select commands.";

            Program.information = information;

            Program.GameStateDisplay(ConsoleColor.White, ConsoleColor.White);
        }

        private void EventIdentifier(Room room)
        {
            bool[] directionEvents = new bool[4];
            directionEvents[0] = room.northEvent;
            directionEvents[1] = room.southEvent;
            directionEvents[2] = room.eastEvent;
            directionEvents[3] = room.westEvent;

            int direction = 0;

            Room[] rooms = new Room[4];
            rooms[0] = room.North;
            rooms[1] = room.South;
            rooms[2] = room.East;
            rooms[3] = room.West;

            foreach(Room eventRoom in rooms)
            {
                var random = new Random();

                if(eventRoom != null)
                {
                    if (directionEvents[direction] && eventRoom.eventSymbol == " ")
                    {
                        switch (random.Next(0, 4))
                        {
                            case 0: rooms[direction].eventSymbol = "?"; break;
                            case 1: rooms[direction].eventSymbol = "X"; break;
                            case 2: rooms[direction].eventSymbol = "$"; break;
                            case 3: rooms[direction].eventSymbol = "P"; break;
                        }
                    }
                }

                direction += 1;
            }
        }

        public void HandleInput(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    if (room.North != null)
                    {
                        if (room.northEvent)
                        {
                            GameStateSwitch(room.North.eventSymbol);
                        }

                        Program.CurrentRoom = room.North;
                    }
                    else
                        nextState = new ExplorationGameState(room);
                    break;

                case ConsoleKey.DownArrow:
                    if (room.South != null)
                    {
                        if (room.southEvent)
                        {
                            GameStateSwitch(room.South.eventSymbol);
                        }

                        Program.CurrentRoom = room.South;
                    }
                    else
                        nextState = new ExplorationGameState(room);
                    break;

                case ConsoleKey.RightArrow:
                    if (room.East != null)
                    {
                        if (room.eastEvent)
                            GameStateSwitch(room.East.eventSymbol);

                        Program.CurrentRoom = room.East;
                    }
                    else
                        nextState = new ExplorationGameState(room);
                    break;

                case ConsoleKey.LeftArrow:
                    if (room.West != null)
                    {
                        if (room.westEvent)
                        {
                            GameStateSwitch(room.West.eventSymbol);
                        }

                        Program.CurrentRoom = room.West;
                    }
                    else
                        nextState = new ExplorationGameState(room);
                    break;
            }

            if (nextState == null)
            {
                nextState = new ExplorationGameState(Program.CurrentRoom);
            }
        }

        private void GameStateSwitch(string eventSymbol)
        {
            switch (eventSymbol)
            {
                case "X": nextState = new CombatGameState(); break;

                case "?":
                    var random = new Random();

                    switch (random.Next(0, 3))
                    {
                        case 0: nextState = new PotionStoreGameState(); break;
                        case 1: nextState = new TreasureGameState(); break;
                        case 2: nextState = new CombatGameState(); break;
                    }
                    break;

                case "$": nextState = new TreasureGameState(); break;
                case "P": nextState = new PotionStoreGameState(); break;
            }
        }

        public IGameState SwitchToScene()
        {
            return nextState;
        }
    }
}
