using System;
using System.IO;
using System.Collections.Generic;

namespace RogueMates
{
    class Program
    {
        private static ConsoleKey input;
        public static bool bossDead = false;
        public static int enemysDefeated = 0;
        public static Ty ty = new Ty();
        public static Ben ben = new Ben();
        public static Tim tim = new Tim();
        private const int displayLines = 34;

        private static Room currentRoom;
        public static Room CurrentRoom
        {
            get { return currentRoom; }
            set { currentRoom = value; }
        }
        private static IGameState currentState;
        public static int points = 0;
        public static string[] information = new string[displayLines];
        private static string[] characterInfo = new string[displayLines];
        private static int gold = 100;

        public static int Gold
        {
            get { return gold; }

            set
            {
                gold = value;

                gold = gold > 999 ? 999 : gold < 0 ? 0 : gold;
            }
        }

        public static Dictionary<int, Potion> potions = new Dictionary<int, Potion>();

        static public void Main()
        {
            currentState = new MainMenuGameState(true);
            currentRoom = new Room();

            while (!bossDead)
            {
                currentState.Display();
                input = Console.ReadKey(true).Key;
                currentState.HandleInput(input);
                currentState = currentState.SwitchToScene(); 
            }
        }

        static public void GameStateDisplay(ConsoleColor leftColor, ConsoleColor rightColor)
        {
            for (int space = 0; space < displayLines; space++)
            {
                if (information[space] == null)
                    information[space] = " ";

                while (information[space].Length < 87)
                {
                    information[space] += " ";
                }
            }

            characterInfo[0] = $"|                              ";
            characterInfo[1] = $"|  Points: {points}                 ";
            characterInfo[2] = $"|                              ";
            characterInfo[3] = $"|  Potions: {potions.Count}                  ";
            characterInfo[4] = $"|                              ";
            characterInfo[5] = $"|  Gold: {gold}                   ";
            characterInfo[6] = $"|______________________________";
            characterInfo[7] = $"|                              ";
            characterInfo[8] = $"|  {ben.name}      ";
            characterInfo[9] = $"|  Health: {ben.Health}/{ben.MaxHealth}             ";
            characterInfo[10] = $"|  Strength: {ben.Strength}                ";
            characterInfo[11] = $"|  Toughness: {ben.Toughness}               ";
            characterInfo[12] = $"|  Defence: {ben.Defence}                 ";
            characterInfo[13] = $"|  Dexterity: {ben.Dexterity}               ";
            characterInfo[14] = $"|  Luck: {ben.Luck}                    ";
            characterInfo[15] = $"|______________________________";
            characterInfo[16] = $"|                              ";
            characterInfo[17] = $"|  {tim.name}  ";
            characterInfo[18] = $"|  Health: {tim.Health}/{tim.MaxHealth}             ";
            characterInfo[19] = $"|  Strength: {tim.Strength}                ";
            characterInfo[20] = $"|  Toughness: {tim.Toughness}               ";
            characterInfo[21] = $"|  Defence: {tim.Defence}                 ";
            characterInfo[22] = $"|  Dexterity: {tim.Dexterity}               ";
            characterInfo[23] = $"|  Luck: {tim.Luck}                    ";
            characterInfo[24] = $"|______________________________";
            characterInfo[25] = $"|                              ";
            characterInfo[26] = $"|  {ty.name}       ";
            characterInfo[27] = $"|  Health: {ty.Health}/{ty.MaxHealth}             ";
            characterInfo[28] = $"|  Strength: {ty.Strength}                ";
            characterInfo[29] = $"|  Toughness: {ty.Toughness}               ";
            characterInfo[30] = $"|  Defence: {ty.Defence}                 ";
            characterInfo[31] = $"|  Dexterity: {ty.Dexterity}               ";
            characterInfo[32] = $"|  Luck: {ty.Luck}                   ";
            characterInfo[33] = $"|                              ";

            for (int line = 0; line < characterInfo.Length; line++)
            {
                Console.ForegroundColor = leftColor;

                Console.Write(" " + information[line]);

                Console.ForegroundColor = rightColor;

                Console.WriteLine(characterInfo[line]);
            }
        }

        public static int ArrowGUI(int arrowPos, ConsoleKey input, int xAxis, int yAxis, int? limit)
        {
            if(limit == null)
                limit = xAxis * yAxis;

            switch (input)
            {
                case ConsoleKey.UpArrow:
                    if (yAxis > 1)
                    {
                        if (arrowPos > xAxis)
                            arrowPos -= xAxis;
                        else if (xAxis == 1)
                            arrowPos = (int)limit;
                    }
                    else
                        arrowPos += 0;
                    break;

                case ConsoleKey.DownArrow:
                    if (yAxis > 1)
                    {
                        if (arrowPos < limit - (xAxis - 1))
                            arrowPos += xAxis;
                        else if (xAxis == 1)
                            arrowPos = 1;
                    }
                    else
                        arrowPos += 0;
                    break;

                case ConsoleKey.LeftArrow:
                    if (xAxis > 1)
                    {
                        if (arrowPos > 1)
                            arrowPos -= 1;
                    }
                    else
                        arrowPos += 0;
                    break;

                case ConsoleKey.RightArrow:
                    if (xAxis > 1)
                    {
                        if (arrowPos < limit)
                            arrowPos += 1;
                    }
                    else
                        arrowPos += 0;
                    break;
            }

            return arrowPos;
        }
    }
}
