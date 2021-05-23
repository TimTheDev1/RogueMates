using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class Tim:Character
    {
        public Tim()
        {
            name = "Tim the Titular Troglodyte";
            nickname = "Tim";

            stats = new CharacterStats(
                175, // MaxHealth
                175, // Health
                10, // Strength
                30, // Toughness
                50, // Defence
                10, // Dexterity
                50); // Luck
        }

        public override void AbilityChosen()
        {
            switch (ability)
            {
                case 1:
                    abilityName = "Planned Strike";
                    abilityDescription[1] = "Makes the next Attack";
                    abilityDescription[2] = "have a 100% chance to";
                    abilityDescription[3] = "Critical Hit the enemy.";
                    abilityActiveMessage = "Planned Strike Ready!";
                    break;
            }

            AbilityActiveMessage();
        }

        public override void CheckAbility(Character character, string callFrom)
        {
            switch (abilityName)
            {
                case "Planned Strike":
                    if (character.Attacking && abilityActive && callFrom == "Enemy Damage")
                    {
                        abilityActive = false;
                        character.Crit = true;
                    }
                    break;
            }
        }
    }
}
