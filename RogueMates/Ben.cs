using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class Ben : Character
    {
        public Ben()
        {
            name = "Ben the Bitchin' Beast";
            nickname = "Ben";

            stats = new CharacterStats(
            100, //MaxHealth
            100, //Health
            50, //Strength
            10, //Toughness
            30, //Defence
            50, //Dexterity
            30 //Luck
            ); 
        }

        public override void AbilityChosen()
        {
            switch (ability)
            {
                case 1:
                    abilityName = "Reckless Defence";
                    abilityDescription[1] = "Doubles Ben's Defence.";
                    abilityDescription[2] = "Ben can't use Defend or";
                    abilityDescription[3] = "Reckless Defence next turn.";
                    abilityActiveMessage = "Reckless Defence Cooldown";
                    break;
            }

            AbilityActiveMessage();
        }

        public override void CheckAbility(Character character, string callFrom)
        {
            switch (abilityName)
            {
                case "Reckless Defence":
                    if(character == this)
                    {
                        if (defending && abilityActive && callFrom == "Combat Game State")
                            defending = false;
                        else if (UsingAbility && abilityActive && callFrom == "Combat Game State")
                            UsingAbility = false;
                        else if (UsingAbility && callFrom == "Damage" && abilityActive)
                            calcDefence += Defence * 2;
                        else if(!UsingPotion)
                            abilityActive = false;
                    }
                    break;
            }
        }
    }
}
