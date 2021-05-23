using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class HorseEnemy:Enemy
    {
        public HorseEnemy()
        {
            creatureName = "Demon Horse";
            maxHealth = 100;
            Health = 100;
            points = 10;
            gold = 25;
            abilityName = "";
            abilityDescription[0] = "";
            abilityDescription[1] = "";
            abilityDescription[2] = "";
 
            Roll();

            creatureDisplayLines[0] = @$"                                         ";
            creatureDisplayLines[1] = @$"                  .-.                    ";
            creatureDisplayLines[2] = @$"            %%%%,/   :-.                 ";
            creatureDisplayLines[3] = @$"            % `%%%, /   `\   _,          ";
            creatureDisplayLines[4] = @$"            |' )`%%|      '-' /          ";
            creatureDisplayLines[5] = @$"            \_/\  %%%/`-.___.'           ";
            creatureDisplayLines[6] = @$"            __/   %%%'--'''-.%,          ";
            creatureDisplayLines[7] = @$"          /`__|   %%         \%%         ";
            creatureDisplayLines[8] = @$"          \\  \   /   |     /'%,         ";
            creatureDisplayLines[9] = @$"           \]  | /----'.   < `%,         ";
            creatureDisplayLines[10] = @$"               ||       `>> >            ";
            creatureDisplayLines[11] = @$"               ||       ///              ";
            creatureDisplayLines[12] = @$"               /(      //(               ";
            creatureDisplayLines[13] = @$"                                         ";
            creatureDisplayLines[14] = @$"                                         ";
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
                        roundValues[round] = random.Next(0, 10); 
                        break;

                    case 1: 
                        roundActions[round] = "Attacking"; 
                        roundValues[round] = random.Next(30, 50);
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
            }
        }
    }
}
