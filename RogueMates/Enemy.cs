using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    abstract class Enemy
    {
        public string creatureName;
        public int maxHealth;
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

        protected int points;

        public int Points { get { return points; } }

        public string abilityName;
        public string[] abilityDescription = new string[3];
        protected int[] roundValues = new int[3];
    
        public int[] RoundValues { get { return roundValues; } }

        protected string[] roundActions = new string[3];

        public string[] RoundActions { get { return roundActions; } }

        protected string[] creatureDisplayLines = new string[20];

        public string[] CreatureDisplayLines { get { return creatureDisplayLines; } }

        public int gold;
        public bool boss = false;

        public abstract void Roll();

        protected void Damage(Character character, int defence)
        {
                Health -= character.EnemyDamage(defence);
        }

        public abstract void Combat(Character character, int round, bool miss);
    }
}
