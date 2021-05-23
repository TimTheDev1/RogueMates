using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RogueMates
{
    class GargoyleEnemy:Enemy
    {
        public GargoyleEnemy()
        {
            creatureName = "Sky Terror Gargoyle";
            maxHealth = 175;
            Health = 175;
            points = 20;
            gold = 50;

            abilityName = "Aerial Ace";
            abilityDescription[0] = "Gargoyle performs aerial maneuvers ";
            abilityDescription[1] = "to avoid attacks";
            abilityDescription[2] = "Value is out of 100 chance to hit";

            Roll();

            creatureDisplayLines[0] = @$"                                         ";
            creatureDisplayLines[1] = @$"                                         ";
            creatureDisplayLines[2] = @$"                                         ";
            creatureDisplayLines[3] = @$"        ,_                    _,         ";
            creatureDisplayLines[4] = @$"        ) '-._  ,_    _,  _.-' (         ";
            creatureDisplayLines[5] = @$"        )  _.-'.|\\--//|.'-._  (         ";
            creatureDisplayLines[6] = @$"         )'   .'\/o\/o\/'.   `(          ";
            creatureDisplayLines[7] = @$"          ) .' . \====/ . '. (           ";
            creatureDisplayLines[8] = @$"           )  / <<    >> \  (            ";
            creatureDisplayLines[9] = @$"            '-._/``  ``\_.-'             ";
            creatureDisplayLines[10] = @$"              __\\'--'//__               ";
            creatureDisplayLines[11] = @$"             (((''`  `'')))              ";
            creatureDisplayLines[12] = @$"                                         ";
            creatureDisplayLines[13] = @$"                                         ";
            creatureDisplayLines[14] = @$"                                         ";
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
                        roundValues[round] = random.Next(10, 30); 
                        break;

                    case 1: 
                        roundActions[round] = "Attacking"; 
                        roundValues[round] = random.Next(60, 80);
                        break;

                    case 2: 
                        roundActions[round] = abilityName;
                        roundValues[round] = random.Next(10, 33);
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

                case "Aerial Ace":
                    int randomNum = new Random().Next(1, 101);

                    bool hit = Enumerable.Range(1, roundValues[round - 1]).Contains(randomNum);

                    if (hit)
                        Damage(character, 0);
                    break;
            }
        }
    }
}
