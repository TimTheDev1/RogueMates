using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;


namespace RogueMates
{
    class MainMenuGameState : IGameState
    {
        private readonly Ben ben = Program.ben;
        private readonly Tim tim = Program.tim;
        private readonly Ty ty = Program.ty;
        const string CharacterSaveDataLocation = @"C:\RogueMates\CharacterSaveData.txt";
        const string PointsSaveDataLocation = @"C:\RogueMates\PointsSaveData.txt";
        private string pointsSaveData;
        private string characterSaveData;
        private Dictionary<string, CharacterStats> characterStats = new Dictionary<string, CharacterStats>();
        private string[,] arrows = new string[7, 3];
        private IGameState nextState;
        private int arrowPos = 1;
        private Character[] characters = { Program.ben, Program.tim, Program.ty };
        private bool startGame = false;

        public MainMenuGameState(bool startOfGame)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrows[i, j] = " ";
                }
            }

            arrows[0, 0] = ">";

            pointsSaveData = JsonConvert.SerializeObject(Program.points);

            characterStats.Add("Ben", Program.ben.Stats);
            characterStats.Add("Tim", Program.tim.Stats);
            characterStats.Add("Ty", Program.ty.Stats);

            characterSaveData = JsonConvert.SerializeObject(characterStats);

            if (startOfGame)
            {
                if (!File.Exists(CharacterSaveDataLocation))
                    File.WriteAllText(CharacterSaveDataLocation, characterSaveData);

                if (!File.Exists(PointsSaveDataLocation))
                    File.WriteAllText(PointsSaveDataLocation, pointsSaveData);
            }
            else
            {
                if (characterStats != JsonConvert.DeserializeObject<Dictionary<string, CharacterStats>>(File.ReadAllText(CharacterSaveDataLocation)))
                    File.WriteAllText(CharacterSaveDataLocation, characterSaveData);

                if(Program.points != JsonConvert.DeserializeObject<int>(File.ReadAllText(PointsSaveDataLocation)))
                    File.WriteAllText(PointsSaveDataLocation, pointsSaveData);
            }

            Program.points = JsonConvert.DeserializeObject<int>(File.ReadAllText(PointsSaveDataLocation));

            characterStats = JsonConvert.DeserializeObject<Dictionary<string, CharacterStats>>(File.ReadAllText(CharacterSaveDataLocation));

            Program.ben.Stats = characterStats["Ben"];
            Program.tim.Stats = characterStats["Tim"];
            Program.ty.Stats = characterStats["Ty"];
        }

        public MainMenuGameState(string[,] arrows, int arrowPos)
        {
            this.arrows = arrows;
            this.arrowPos = arrowPos;
        }

        public void Display()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("");
            Console.WriteLine(@"             _____                          __  __       _      ");
            Console.WriteLine(@"            |  __ \                        |  \/  |     | |");
            Console.WriteLine(@"            | |__) |___   __ _ _   _  ___  | \  / | __ _| |_ ___  ___");
            Console.WriteLine(@"            |  _  // _ \ / _` | | | |/ _ \ | |\/| |/ _` | __/ _ \/ __|");
            Console.WriteLine(@"            | | \ \ (_) | (_| | |_| |  __/ | |  | | (_| | |_| __/\__ \");
            Console.WriteLine(@"            |_|  \_\___/ \__, |\__,_|\___| |_|  |_|\__,_|\__\___||___/");
            Console.WriteLine(@"                         __/ |");
            Console.WriteLine(@"                        |___/  ");
            Console.WriteLine("");
            Console.WriteLine($"{arrows[0, 0]} Play");
            Console.WriteLine("");
            Console.WriteLine($"  Points: {Program.points}");
            Console.WriteLine("");
            Console.WriteLine($"  {ben.name}      {tim.name}      {ty.name}");
            Console.WriteLine($"{arrows[0,1]} Max Health: {ben.MaxHealth}           {arrows[0,2]} Max Health: {tim.MaxHealth}               {arrows[1,0]} Max Health: {ty.MaxHealth}");
            Console.WriteLine("");
            Console.WriteLine("  Strength: The amount of combat damage dealt when attacking (Max 99).");
            Console.WriteLine($"{arrows[1,1]} Strength: {ben.Strength}              {arrows[1,2]} Strength: {tim.Strength}                  {arrows[2,0]} Strength: {ty.Strength}");
            Console.WriteLine("");
            Console.WriteLine("  Toughness: The amount of damage reduced without defending (Max 99).");
            Console.WriteLine($"{arrows[2,1]} Toughness: {ben.Toughness}             {arrows[2,2]} Toughness: {tim.Toughness}                 {arrows[3,0]} Toughness: {ty.Toughness}");
            Console.WriteLine("");
            Console.WriteLine("  Defence: The amount of damage reduced when defending (Max 99).");
            Console.WriteLine($"{arrows[3,1]} Defence: {ben.Defence}               {arrows[3,2]} Defence: {tim.Defence}                   {arrows[4,0]} Defence: {ty.Defence}");
            Console.WriteLine("");
            Console.WriteLine("  Dexterity: Stat requirement for equipment (Max 99).");
            Console.WriteLine($"{arrows[4,1]} Dexterity: {ben.Dexterity}             {arrows[4,2]} Dexterity: {tim.Dexterity}                 {arrows[5,0]} Dexterity: {ty.Dexterity}");
            Console.WriteLine("");
            Console.WriteLine("  Luck: Critical hit chance and Miss chance (Max 99).");
            Console.WriteLine($"{arrows[5,1]} Luck: {ben.Luck}                  {arrows[5,2]} Luck: {tim.Luck}                      {arrows[6,0]} Luck: {ty.Luck}");
            Console.WriteLine("");

        }

        public void HandleInput(ConsoleKey input)
        {
            startGame = false;

            switch (input)
            {
                case ConsoleKey.UpArrow:
                    if (Enumerable.Range(2, 3).Contains(arrowPos))
                        arrowPos = 1;
                    else if (arrowPos != 1)
                        arrowPos -= 3;
                    break;

                case ConsoleKey.DownArrow:
                    if (arrowPos == 1)
                        arrowPos += 1;
                    else if (!Enumerable.Range(16, 4).Contains(arrowPos))
                        arrowPos += 3;
                    break;

                case ConsoleKey.LeftArrow:
                    if (arrowPos != 1)
                        arrowPos -= 1;
                    break;

                case ConsoleKey.RightArrow:
                    if (arrowPos != 19)
                        arrowPos += 1;
                    break;

                case ConsoleKey.Enter:
                    if (Enumerable.Range(2, 18).Contains(arrowPos))
                    {
                        int points;

                        Console.WriteLine("How many points do you want to use?");
                        string pointsInput = Console.ReadLine();

                        if(int.TryParse(pointsInput, out points))
                        {
                            if(points <= Program.points)
                            {
                                if(arrowPos < 5)
                                {
                                    switch (arrowPos)
                                    {
                                        case 2: ben.MaxHealth += points; break;
                                        case 3: tim.MaxHealth += points; break;
                                        case 4: ty.MaxHealth += points; break;
                                    }

                                    Program.points -= points;
                                }
                                else
                                {
                                    int[] stats =
                                    {
                                        ben.Strength ,
                                        tim.Strength ,
                                        ty.Strength ,
                                        ben.Toughness ,
                                        tim.Toughness ,
                                        ty.Toughness ,
                                        ben.Defence ,
                                        tim.Defence ,
                                        ty.Defence ,
                                        ben.Dexterity ,
                                        tim.Dexterity ,
                                        ty.Dexterity ,
                                        ben.Luck ,
                                        tim.Luck ,
                                        ty.Luck
                                    };

                                    if (stats[arrowPos - 5] + points <= 99)
                                    {
                                        stats[arrowPos - 5] += points;
                                        Program.points -= points;
                                    }

                                    ben.Strength = stats[0];
                                    tim.Strength = stats[1];
                                    ty.Strength = stats[2];
                                    ben.Toughness = stats[3];
                                    tim.Toughness = stats[4];
                                    ty.Toughness = stats[5];
                                    ben.Defence = stats[6];
                                    tim.Defence = stats[7];
                                    ty.Defence = stats[8];
                                    ben.Dexterity = stats[9];
                                    tim.Dexterity = stats[10];
                                    ty.Dexterity = stats[11];
                                    ben.Luck = stats[12];
                                    tim.Luck = stats[13];
                                    ty.Luck = stats[14];
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach(Character chara in characters)
                        {
                            chara.Ability = 1;
                            chara.AbilityChosen();
                            chara.Ready();
                        }

                        SetUpNextState();
                    }
                    break;
            }

            for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (arrowPos == (i * 3) + j + 1)
                        arrows[i, j] = ">";
                    else
                        arrows[i, j] = " ";
                }
            }
        }

        private void SetUpNextState()
        {
            pointsSaveData = JsonConvert.SerializeObject(Program.points);

            characterStats["Ben"] = Program.ben.Stats;
            characterStats["Tim"] = Program.tim.Stats;
            characterStats["Ty"] = Program.ty.Stats;

            characterSaveData = JsonConvert.SerializeObject(characterStats);

            File.WriteAllText(CharacterSaveDataLocation, characterSaveData);
            File.WriteAllText(PointsSaveDataLocation, pointsSaveData);

            startGame = true;
        }

        public IGameState SwitchToScene()
        {
            if (!startGame)
                nextState = new MainMenuGameState(arrows, arrowPos);
            else
                nextState = new ExplorationGameState();

            return nextState;
        }
    }
}
