using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RogueMates
{
    abstract class Character
    {
        public string name;
        public string nickname;
        protected CharacterStats stats;

        public CharacterStats Stats { get { return stats; } set { stats = value; } }

        

        public int MaxHealth { get { return stats.MaxHealth; } set { stats.MaxHealth = value; } }

        public int Health { get { return stats.Health; } set { stats.Health = value; } }

        public int Strength
        {
            get
            {
                int returnValue = stats.Strength + equipment.statValue;

                if (equipment.stat == "Strength:")
                    returnValue = returnValue > stats.StrengthCap ? stats.StrengthCap : returnValue < 0 ? 0 : returnValue;
                else
                    returnValue = stats.Strength > stats.StrengthCap ? 99 : stats.Strength < 0 ? 0 : stats.Strength;

                return returnValue;
            }

            set
            {
                stats.Strength = value;

                stats.Strength = stats.Strength > stats.StrengthCap ? 99 : stats.Strength < 0 ? 0 : stats.Strength;
            }
        }

        public int Defence
        {
            get
            {
                int returnValue = stats.Defence + equipment.statValue;

                if (equipment.stat == "Defence:")
                    returnValue = returnValue > stats.DefenceCap ? stats.DefenceCap : returnValue < 0 ? 0 : returnValue;
                else
                    returnValue = stats.Defence > stats.DefenceCap ? stats.DefenceCap : stats.Defence < 0 ? 0 : stats.Defence;

                return returnValue;
            }

            set
            {
                stats.Defence = value;

                stats.Defence = stats.Defence > stats.DefenceCap ? stats.DefenceCap : stats.Defence < 0 ? 0 : stats.Defence;
            }
        }

        public int Dexterity { get { return stats.Dexterity; } set { stats.Dexterity = value; } }

        public int Luck
        {
            get
            {
                int returnValue = stats.Luck + equipment.statValue;

                if (equipment.stat == "Luck:")
                    returnValue = returnValue > stats.LuckCap ? stats.LuckCap : returnValue < 0 ? 0 : returnValue;
                else
                    returnValue = stats.Luck > stats.LuckCap ? stats.LuckCap : stats.Luck < 0 ? 0 : stats.Luck;

                return returnValue;
            }

            set
            {
                stats.Luck = value;

                stats.Luck = stats.Luck > stats.LuckCap ? stats.LuckCap : stats.Luck < 0 ? 0 : stats.Luck;
            }
        }

        public int Toughness
        {
            get
            {
                int returnValue = stats.Toughness + equipment.statValue;

                if (equipment.stat == "Toughness:")
                    returnValue = returnValue > stats.ToughnessCap ? stats.ToughnessCap : returnValue < 0 ? 0 : returnValue;
                else
                    returnValue = stats.Toughness > stats.ToughnessCap ? stats.ToughnessCap : stats.Toughness < 0 ? 0 : stats.Toughness;

                return returnValue;
            }

            set
            {
                stats.Toughness = value;

                stats.Toughness = stats.Toughness > stats.ToughnessCap ? stats.ToughnessCap : stats.Toughness < 0 ? 0 : stats.Toughness;
            }
        }
        
        

        protected bool defending;
        public bool Defending { get { return defending; } set { defending = value; } }

        protected bool attacking;
        public bool Attacking { get { return attacking; } set { attacking = value; } }

        protected bool crit;

        public bool Crit {  get { return crit; } set { crit = value; } }

        protected bool usingAbility;

        public bool UsingAbility { get { return usingAbility; } set { usingAbility = value; } }

        protected bool usingPotion;

        public bool UsingPotion { get { return usingPotion; } set { usingPotion = value; } }

        protected int calcDefence;

        public int CalcDefence { get { return calcDefence; } set { calcDefence = value; } }

        protected int calcAttack;

        public int CalcAttack { get { return calcAttack; } set { calcAttack = value; } }

        protected int ability = 1;

        public int Ability { get { return ability; } set { ability = value; } }

        public bool abilityActive = false;

        public bool AbilityActive { get { return abilityActive; } set { abilityActive = value; } }

        public bool hasFought;
        protected int stat;
        public Equipment equipment = new Equipment();
        public string ready = "Ready!";
        public string abilityName;
        public string[] abilityDescription = { "", "", "", "", "" };
        protected string abilityActiveMessage = "Ability Active!";
        protected string abilityDisabledMessage = "Ability Disabled!";
        public bool alive = true;
        protected Potion activePotion;

        

        public virtual void Damage(int damage, bool miss)
        {
            calcDefence = Toughness;

            calcDefence += defending ? Defence : 0;

            Character[] characters = { Program.ben, Program.tim, Program.ty };

            foreach (Character character in characters)
            {
                character.CheckAbility(this, "Damage");
            }

            if(!miss)
                Health -= (damage - calcDefence);

            alive = stats.Health <= 0 ? false : true;
        }

        public int EnemyDamage(int enemyDefence)
        {
            calcAttack = attacking ? Strength : 0;

            crit = LuckRoll(15);

            Character[] characters = { Program.ben, Program.tim, Program.ty };

            foreach (Character character in characters)
            {
                character.CheckAbility(this, "Enemy Damage");
            }

            calcAttack += crit ? calcAttack/2 : 0;

            int enemyDamage = calcAttack - enemyDefence;

            enemyDamage = enemyDamage < 0 ? 0 : enemyDamage;
                
            return enemyDamage;
        }

        public bool LuckRoll(int rangeCeiling)
        {
            Random random = new Random();

            int rollCap = 174 - Luck;

            bool outcome = Enumerable.Range(1, rangeCeiling).Contains(random.Next(1, rollCap));

            return outcome;
        }

        public void Ready()
        {
            ready = !alive ? "Dead" : hasFought ? "Not Ready!" : "Ready!";

            while (ready.Length < 19)
            {
                ready += " ";
            }
        }

        public void ConsumePotion(Potion potion)
        {
            activePotion = potion;

            switch (activePotion.stat)
            {
                case "Strength": stat = Strength; Strength += potion.statValue; break;
                case "Toughness": stat = Toughness; Toughness += potion.statValue; break;
                case "Defence": stat = Defence; Defence += potion.statValue; break;
                case "Luck": stat = Luck; Luck += potion.statValue; break;
            }
        }

        public void PotionWearOff()
        {
            if(activePotion != null)
            {
                switch (activePotion.stat)
                {
                    case "Strength": Strength = stat; break;
                    case "Toughness": Toughness = stat; break;
                    case "Defence": Defence = stat; break;
                    case "Luck": Luck = stat; break;
                }
            }
        }

        public void ActivateAbility()
        {
            if (!abilityActive)
                abilityActive = true;

            AbilityActiveMessage();
        }

        public void AbilityActiveMessage()
        {
            switch (abilityActive)
            {
                case true: abilityDescription[4] = abilityActiveMessage; break;
                case false: abilityDescription[4] = ""; break;
            }

            abilityDescription[0] = $"Ability: {abilityName}";

            for (int line = 0; line < 5; line++)
            {
                while(abilityDescription[line].Length < 27)
                {
                    abilityDescription[line] += " ";
                }
            }
        }

        public abstract void AbilityChosen();

        public abstract void CheckAbility(Character character, string callFrom);
    }
}
