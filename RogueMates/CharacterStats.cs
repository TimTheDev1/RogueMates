using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class CharacterStats
    {
        private int maxHealthCap = 500;

        private int maxHealth;

        public int MaxHealth
        {
            get { return maxHealth; }

            set
            {
                maxHealth = value;

                maxHealth = maxHealth > maxHealthCap ? maxHealthCap : maxHealth;
            }
        }

        private int health;

        public int Health
        {
            get { return health; }

            set
            {
                health = value;

                health = health > maxHealth ? maxHealth : health;
            }
        }

        public int StrengthCap { get { return 145; } }

        private int strength;

        public int Strength
        {
            get { return strength; }

            set
            {
                strength = value;
            }
        }

        public int ToughnessCap { get { return 145; } }

        private int toughness;

        public int Toughness
        {
            get { return toughness; }

            set
            {
                toughness = value;
            }
        }

        public int DefenceCap { get { return 145; } }

        private int defence;

        public int Defence
        {
            get { return defence; }

            set
            {
                defence = value;
            }
        }

        public int DexterityCap { get { return 145; } }

        private int dexterity;

        public int Dexterity
        {
            get { return dexterity; }

            set
            {
                dexterity = value;
            }
        }

        public int LuckCap { get { return 145; } }

        private int luck;

        public int Luck
        {
            get { return luck; }

            set { luck = value; }
        }

        public CharacterStats(int maxHealth, int health, int strength, int toughness, int defence, int dexterity, int luck)
        {
            this.maxHealth = maxHealth;
            this.health = health;
            this.strength = strength;
            this.toughness = toughness;
            this.defence = defence;
            this.dexterity = dexterity;
            this.luck = luck;
        }
    }
}
