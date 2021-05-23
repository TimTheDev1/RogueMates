using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class DragonEnemy : Enemy
    {
        public DragonEnemy()
        {
            creatureName = "Ferocious Wolf Dragon";
            maxHealth = 750;
            Health = 750;
            points = 100;
            boss = true;

            abilityName = "Fire Breath";
            abilityDescription[0] = "An attack that ignores Toughness";
            abilityDescription[1] = "";
            abilityDescription[2] = "";

            Roll();

            CreatureDisplayLines[0] = @$"                                         ";
            CreatureDisplayLines[1] = @$" <>=======()                             ";
            CreatureDisplayLines[2] = @$"(/\___   /|\\          ()==========<>_   ";
            CreatureDisplayLines[3] = @$"      \_/ | \\        //|\   ______/ \)  ";
            CreatureDisplayLines[4] = @$"        \_|  \\      // | \_/            ";
            CreatureDisplayLines[5] = @$"          \|\/|\_   //  /\/              ";
            CreatureDisplayLines[6] = @$"           (oo)\ \_//  /                 ";
            CreatureDisplayLines[7] = @$"          //_/\_\/ /  |                  ";
            CreatureDisplayLines[8] = @$"         @@/  |=\  \  |                  ";
            CreatureDisplayLines[9] = @$"              \_=\_ \ |                  ";
            CreatureDisplayLines[10] = @$"                \==\ \|\                 ";
            CreatureDisplayLines[11] = @$"             __(\===\( _)                ";
            CreatureDisplayLines[12] = @$"            (((~) __(_/                  ";
            CreatureDisplayLines[13] = @$"                 (((~)                   ";
            CreatureDisplayLines[14] = @$"                                         ";
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
                        roundValues[round] = random.Next(30, 70);
                        break;

                    case 1: 
                        roundActions[round] = "Attacking"; 
                        roundValues[round] = random.Next(120, 150);
                        break;

                    case 2: 
                        roundActions[round] = abilityName; 
                        roundValues[round] = random.Next(120, 150); 
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

                    if(character.Attacking)
                        Damage(character, 0);
                    break;

                case "Defending":
                    if(character.Attacking)
                        Damage(character, roundValues[round - 1]);
                    break;

                case "Fire Breath":
                    character.Damage(roundValues[round - 1] + character.Toughness, miss);
                    
                    if(character.Attacking)
                        Damage(character, 0);
                    break;
            }
        }
    }
}
