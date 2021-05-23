using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class ReaperEnemy:Enemy
    {
        public ReaperEnemy()
        {
            creatureName = "Reaper Husk";
            maxHealth = 300;
            Health = 300;
            points = 80;
            gold = 200;

            abilityName = "Life Reaper";
            abilityDescription[0] = "Every attack heals the husk";
            abilityDescription[1] = "by the amount of damage dealt";
            abilityDescription[2] = "";

            Roll();

            CreatureDisplayLines[0] = @$"                         ,____           ";
            CreatureDisplayLines[1] = @$"                         |---.\          ";
            CreatureDisplayLines[2] = @$"                 ___     |    `          ";
            CreatureDisplayLines[3] = @$"                / .-\  ./=)              ";
            CreatureDisplayLines[4] = @$"               |  |'|_/\/|               ";
            CreatureDisplayLines[5] = @$"               ;  |-;| /_|               ";
            CreatureDisplayLines[6] = @$"              / \_| |/ \ |               ";
            CreatureDisplayLines[7] = @$"             /      \/\( |               ";
            CreatureDisplayLines[8] = @$"             |   /  |` ) |               ";
            CreatureDisplayLines[9] = @$"             /   \ _/    |               ";
            CreatureDisplayLines[10] = @$"            /--._/  \    |               ";
            CreatureDisplayLines[11] = @$"            `/|)    |    /               ";
            CreatureDisplayLines[12] = @$"              /     |   |                ";
            CreatureDisplayLines[13] = @$"            .'      |   |                ";
            CreatureDisplayLines[14] = @$"           /         \  |                ";
            CreatureDisplayLines[15] = $@"          (_.-.__.__./  /                ";
        }

        public override void Roll()
        {
            var random = new Random();

            for (int round = 0; round < 3; round++)
            {
                switch (random.Next(0, 2))
                {
                    case 0:
                        roundActions[round] = "Defending"; 
                        roundValues[round] = random.Next(20, 60);
                        break;

                    case 1:
                        roundActions[round] = "Attacking"; 
                        roundValues[round] = random.Next(100, 140);
                        break;                
                }
            }
        }

        public override void Combat(Character character, int round, bool miss)
        {
            switch (roundActions[round - 1])
            {
                case "Attacking":
                    int characterHealth = character.Health;

                    character.Damage(roundValues[round - 1], miss);

                    Health += characterHealth - character.Health;

                    if (character.Attacking)
                        Damage(character, 0);
                    break;

                case "Defending":
                    if (character.Attacking)
                        Damage(character, roundValues[round - 1]);
                    break;
            }
        }
    }
}
