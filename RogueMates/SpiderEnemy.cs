using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class SpiderEnemy:Enemy
    {
        public SpiderEnemy()
        {
            creatureName = "Forest Fang Spider";
            maxHealth = 500;
            Health = 500;
            points = 60;
            gold = 100;

            abilityName = "Piercing Fang";
            abilityDescription[0] = "A low damage attack that";
            abilityDescription[1] = "ignores Defence";
            abilityDescription[2] = "";

            Roll();

            creatureDisplayLines[0] = @$"                   ||                    ";
            creatureDisplayLines[1] = @$"                   ||                    ";
            creatureDisplayLines[2] = @$"                   ||                    ";
            creatureDisplayLines[3] = @$"                   ||                    ";
            creatureDisplayLines[4] = @$"                   ||                    ";
            creatureDisplayLines[5] = @$"             _ /\  ||  /\ _              ";
            creatureDisplayLines[6] = @$"            / X  \.--./  X \             ";
            creatureDisplayLines[7] = @$"           /_/ \/`    `\/ \_\            ";
            creatureDisplayLines[8] = @$"          /|(`-/\_/)(\_/\-`)|\           ";
            creatureDisplayLines[9] = @$"         ( |` (_(.oOOo.)_) `| )          ";
            creatureDisplayLines[10] = @$"         ` |  `//\(  )/\\`  | `          ";
            creatureDisplayLines[11] = @$"           (  // ()\/() \\  )            ";
            creatureDisplayLines[12] = @$"            ` (   \   /   ) `            ";
            creatureDisplayLines[13] = @$"               \         /               ";
            creatureDisplayLines[14] = @$"                `       `                ";
        }

        public override void Roll()
        {
            var random = new Random();

            for (int round = 0; round < 3; round++)
            {
                switch (random.Next(0, 3))
                {
                    case 0: 
                        roundActions[round] = "Defending";
                        roundValues[round] = random.Next(15, 45);
                        break;

                    case 1: 
                        roundActions[round] = "Attacking"; 
                        roundValues[round] = random.Next(90, 110);
                        break;

                    case 2: 
                        roundActions[round] = abilityName; 
                        roundValues[round] = random.Next(60, 90); 
                        break;
                }
            }
        }

        public override void Combat(Character character, int round, bool miss)
        {
            switch (roundActions[round - 1])
            {
                case "Attacking":
                    character.Damage(roundValues[round - 1], miss);

                    if (character.Attacking)
                        Damage(character, 0);
                    break;

                case "Defending":
                    if (character.Attacking)
                        Damage(character, roundValues[round - 1]);
                    break;

                case "Piercing Fang":
                    character.Damage(roundValues[round - 1] + character.Defence, miss);

                    if (character.Attacking)
                        Damage(character, 0);
                    break;
            }
        }
    }
}
