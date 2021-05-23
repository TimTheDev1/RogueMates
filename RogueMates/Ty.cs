using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class Ty : Character
    {
        public Ty()
        {
            name = "Ty the Terrible Titan";
            nickname = "Ty";

            stats = new CharacterStats(
                250, // MaxHealth
                250, // Health
                30, // Strength
                50, // Toughness
                10, // Defence
                30, // Dexterity
                10); // Luck
        }

        public override void AbilityChosen()
        {
            switch (ability)
            {
                case 1:
                    abilityName = "Titan Shield";
                    abilityDescription[1] = "Increases the Toughness of";
                    abilityDescription[2] = "the party member that fights";
                    abilityDescription[3] = "next round by Ty's Toughness.";
                    abilityActiveMessage = "Titan Shield Ready!";
                    break;
            }

            AbilityActiveMessage();
        }

        public override void CheckAbility(Character character, string callFrom)
        {
            switch (abilityName)
            {
                case "Titan Shield":
                    if (abilityActive && character != this && callFrom == "Damage")
                    {
                        abilityActive = false;
                        character.CalcDefence += Toughness;
                    }
                    break;
            }
        }
    }
}
