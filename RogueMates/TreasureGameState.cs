using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RogueMates
{
    class TreasureGameState : IGameState
    {
        private readonly Character ben = Program.ben;
        private readonly Character tim = Program.tim;
        private readonly Character ty = Program.ty;
        private string[] treasureInfoLines = new string[20];
        public Equipment equipment;
        private Character character;
        private bool equipmentTreasure;
        private int charArrowPos = 1;
        private string[] charArrows = { ">", " ", " ", " " };
        private Character[] characters = { Program.ben, Program.tim, Program.ty };
        private IGameState nextState;
        private bool rolled = false;
        private bool treasureState = true;
        private const int gold = 25;

        public TreasureGameState()
        {
            var random = new Random();

            equipment = new Equipment(random);

            if (random.Next(0, 2) == 0)
                EquipmentTreasure();
            else
                GoldTreasure();
        }

        public TreasureGameState(int charArrowPos, string[] charArrows, Equipment equipment, bool rolled, bool equipmentTreasure)
        {
            this.charArrowPos = charArrowPos;
            this.charArrows = charArrows;
            this.equipment = equipment;
            this.rolled = rolled;
            this.equipmentTreasure = equipmentTreasure;

            if (this.equipmentTreasure)
                EquipmentTreasure();
            else
                GoldTreasure();
        }

        public void Display()
        {
            Console.Clear();

            string[] information = new string[Program.information.Length];

            information[0] = "";
            information[1] = @"        __________        ";
            information[2] = @"       /\____;;___\       "; 
            information[3] = @"      | /         /       ";
            information[4] = @"      `. ())oo() .        ";
            information[5] = @"      %| |-%-------|      ";
            information[6] = @"     % \ | %  ))   |      ";
            information[7] = @"     %  \|%________|      ";
            information[8] = "";
            information[9] = treasureInfoLines[0];
            information[10] = treasureInfoLines[1];
            information[11] = treasureInfoLines[2];
            information[12] = treasureInfoLines[3];
            information[13] = treasureInfoLines[4];
            information[14] = treasureInfoLines[5];
            information[15] = treasureInfoLines[6];
            information[16] = treasureInfoLines[7];
            information[17] = treasureInfoLines[8];
            information[18] = treasureInfoLines[9];
            information[19] = treasureInfoLines[10];
            information[20] = treasureInfoLines[11];
            information[21] = treasureInfoLines[12];
            information[22] = treasureInfoLines[13];
            information[23] = treasureInfoLines[14];
            information[24] = treasureInfoLines[15];
            information[25] = treasureInfoLines[16];

            Program.information = information;

            Program.GameStateDisplay(ConsoleColor.Yellow, ConsoleColor.Blue);
        }

        private void EquipmentTreasure()
        {
            equipmentTreasure = true;

            treasureInfoLines[0] = $"";
            treasureInfoLines[1] = $"You Found A Treasure Chest!!!";
            treasureInfoLines[2] = $"";
            treasureInfoLines[3] = $"There's a piece of equipment inside!!";
            treasureInfoLines[4] = $"";
            treasureInfoLines[5] = $"{equipment.name}";
            treasureInfoLines[6] = $"Dexterity required: {(int)equipment.dexterityCost}";
            treasureInfoLines[7] = $"{equipment.statString}";
            treasureInfoLines[8] = $"";
            treasureInfoLines[9] = $"Choose a character to give the equipment to.";
            treasureInfoLines[10] = $"";
            treasureInfoLines[11] = $"{charArrows[0]} {ben.nickname}              {charArrows[1]} {tim.nickname}              {charArrows[2]} {ty.nickname}";
            treasureInfoLines[12] = $"{ben.equipment.name}  {tim.equipment.name}  {ty.equipment.name}";
            treasureInfoLines[13] = $"{ben.equipment.statString}  {tim.equipment.statString}  {ty.equipment.statString}";
            treasureInfoLines[14] = $"";
            treasureInfoLines[15] = $"{charArrows[3]} Go Back";
        }

        private void GoldTreasure()
        {
            treasureInfoLines[0] = $"";
            treasureInfoLines[1] = $"You Found A Treasure Chest!!!";
            treasureInfoLines[2] = $"";
            treasureInfoLines[3] = $"You found {gold} gold!";
            treasureInfoLines[4] = "";
            treasureInfoLines[5] = "> Continue";
        }

        public void HandleInput(ConsoleKey input)
        {
            if (equipmentTreasure)
            {
                treasureState = true;

                charArrowPos = Program.ArrowGUI(charArrowPos, input, 3, 2, 4);

                switch (input)
                {
                    case ConsoleKey.Enter:
                        if(charArrowPos > 3)
                        {
                            treasureState = false;
                        }
                        else
                        {
                            for (int chars = 0; chars < 3; chars++)
                            {
                                if (charArrows[chars] == ">")
                                    character = characters[chars];
                            }

                            if (character.Dexterity >= equipment.dexterityCost)
                            {
                                character.equipment = equipment;
                                treasureState = false;
                            }
                            else
                                treasureState = true;
                        }
                        break;

                    default:
                        treasureState = true;
                        break;
                }

                for(int charArrow = 0; charArrow < charArrows.Length; charArrow++)
                {
                    charArrows[charArrow] = charArrowPos == charArrow + 1 ? ">" : " ";
                }
            }
            else
            {
                if(input == ConsoleKey.Enter)
                {
                    treasureState = false;

                    Program.Gold += gold;
                }
            }
                
            SetUpNextState();
        }

        private void SetUpNextState()
        {
            if (treasureState)
                nextState = new TreasureGameState(charArrowPos, charArrows, equipment, rolled, equipmentTreasure);
            else
                nextState = new ExplorationGameState();
        }

        public IGameState SwitchToScene()
        {
            return nextState;
        }
    }
}
