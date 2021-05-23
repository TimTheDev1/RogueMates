using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RogueMates
{
    class CombatGameState : IGameState
    {
        private int round = 1;
        private bool enemyDead = false;
        private bool error = false;
        private Character character;
        private string notifications = "R key to reset.";
        private readonly Ben ben = Program.ben;
        private readonly Tim tim = Program.tim;
        private readonly Ty ty = Program.ty;
        private int roundCap = 0;
        private bool characterSelected;
        private int charArrowPos = 1;
        private int actionArrowPos = 1;
        private string[] charArrows = { ">", " ", " " };
        private string[] actionArrows = { ">", " ", " ", " " };
        private Enemy enemy;
        private bool potionAction;
        private string[] potionArrows = new string[4];
        private int potionArrowPos = 1;
        private Potion potion;
        private string[] potionNames = new string[4];
        private int strengthPotions = 0;
        private int toughnessPotions = 0;
        private int defencePotions = 0;
        private int luckPotions = 0;
        private Character[] characters = { Program.ben, Program.tim, Program.ty };
        private Enemy[] enemies = { new HorseEnemy(), new GargoyleEnemy(), new SpiderEnemy(), new ReaperEnemy(), new DragonEnemy() };

        public CombatGameState()
        {
            for(int potionName = 0; potionName < potionNames.Length; potionName++)
            {
                potionNames[potionName] = " ";
            }

            potionArrows[0] = " ";

            foreach(Character chara in characters)
            {
                chara.AbilityActiveMessage();

                roundCap += chara.alive ? 1 : 0;
            }

            var random = new Random();

            if (Program.enemysDefeated < 3)
                enemy = enemies[random.Next(0, Program.enemysDefeated)];
            else if (Program.enemysDefeated < 10)
                enemy = enemies[random.Next(0, Program.enemysDefeated / 2)];
            else
                enemy = enemies[4];
        }

        private CombatGameState(int round, string notifications, string[] charArrows, int charArrowPos, string[] actionArrows, int actionArrowPos, 
            bool characterSelected,  Character character, Enemy enemy, string[] potionArrows, int potionArrowPos, string[] potionNames, bool potionAction)
        {
            this.round = round;
            this.notifications = notifications;
            this.charArrows = charArrows;
            this.charArrowPos = charArrowPos;
            this.actionArrows = actionArrows;
            this.actionArrowPos = actionArrowPos;
            this.characterSelected = characterSelected;
            this.character = character;
            this.enemy = enemy;
            this.potionArrows = potionArrows;
            this.potionArrowPos = potionArrowPos;
            this.potionNames = potionNames;
            this.potionAction = potionAction;

            foreach (Character chara in characters)
            {
                chara.AbilityActiveMessage();

                roundCap += chara.alive ? 1 : 0;
            }
        }

        public void Display()
        {
            Console.Clear();

            string bossEncounter = enemy.boss ? "Boss Encounter!!!" : "";

            string round1 = roundCap >= 1 ? $"Round 1 - {enemy.RoundActions[0]} - {enemy.RoundValues[0]}" : "";
            string round2 = roundCap >= 2 ? $"Round 2 - {enemy.RoundActions[1]} - {enemy.RoundValues[1]}" : "";
            string round3 = roundCap >= 3 ? $"Round 3 - {enemy.RoundActions[2]} - {enemy.RoundValues[2]}" : "";

            string[] information = new string[Program.information.Length];

            information[0] = $"{enemy.CreatureDisplayLines[0]} {bossEncounter}";
            information[1] = $"{enemy.CreatureDisplayLines[1]} You encounter a {enemy.creatureName}!!!";
            information[2] = $"{enemy.CreatureDisplayLines[2]}";
            information[3] = $"{enemy.CreatureDisplayLines[3]} Health: {enemy.Health}/{enemy.maxHealth}";
            information[4] = $"{enemy.CreatureDisplayLines[4]}";
            information[5] = $"{enemy.CreatureDisplayLines[5]} {round1}";
            information[6] = $"{enemy.CreatureDisplayLines[6]} {round2}";
            information[7] = $"{enemy.CreatureDisplayLines[7]} {round3}";
            information[8] = $"{enemy.CreatureDisplayLines[8]}";
            information[9] = $"{enemy.CreatureDisplayLines[9]} Round: {round}";
            information[10] = $"{enemy.CreatureDisplayLines[10]}";
            information[11] = $"{enemy.CreatureDisplayLines[11]} {enemy.abilityName}";
            information[12] = $"{enemy.CreatureDisplayLines[12]} {enemy.abilityDescription[0]}";
            information[13] = $"{enemy.CreatureDisplayLines[13]} {enemy.abilityDescription[1]}";
            information[14] = $"{enemy.CreatureDisplayLines[14]} {enemy.abilityDescription[2]}";
            information[15] = "";
            information[16] = $"{charArrows[0]} Ben - {ben.ready}  {charArrows[1]} Tim - {tim.ready}  {charArrows[2]} Ty - {ty.ready}";
            information[17] = $"{ben.abilityDescription[0]}  {tim.abilityDescription[0]}  {ty.abilityDescription[0]}";
            information[18] = $"{ben.abilityDescription[1]}  {tim.abilityDescription[1]}  {ty.abilityDescription[1]}";
            information[19] = $"{ben.abilityDescription[2]}  {tim.abilityDescription[2]}  {ty.abilityDescription[2]}";
            information[20] = $"{ben.abilityDescription[3]}  {tim.abilityDescription[3]}  {ty.abilityDescription[3]}";
            information[21] = $"{ben.abilityDescription[4]}  {tim.abilityDescription[4]}  {ty.abilityDescription[4]}";
            information[22] = "";
            information[23] = $"{actionArrows[0]} Attack    {actionArrows[1]} Defend";
            information[24] = "";
            information[25] = $"{actionArrows[2]} Ability   {actionArrows[3]} Potion";
            information[26] = "";
            information[27] = $"{potionArrows[0]} {potionNames[0]}  {potionArrows[1]} {potionNames[1]}";
            information[28] = $"{potionArrows[2]} {potionNames[2]}   {potionArrows[3]} {potionNames[3]}";
            information[29] = "";
            information[30] = $"{notifications}";
            information[31] = "";

            Program.information = information;

            Program.GameStateDisplay(ConsoleColor.Red, ConsoleColor.Cyan);
        }

        public void HandleInput(ConsoleKey input)
        {
            bool inputTaken = false;

            if (potionAction && !inputTaken)
            {
                int key = 0;
                string stat = "";

                inputTaken = true;

                potionArrowPos = Program.ArrowGUI(potionArrowPos, input, 2, 2, null);

                switch (input)
                {
                    case ConsoleKey.R:
                        for (int potionName = 0; potionName < potionNames.Length; potionName++)
                        {
                            potionNames[potionName] = " ";
                        }

                        for (int potionArrow = 0; potionArrow < potionArrows.Length; potionArrow++)
                        {
                            potionArrows[potionArrow] = " ";
                        }

                        potionAction = false;

                        error = true;

                        SetUpNextState();
                        break;

                    case ConsoleKey.Enter:
                        switch (potionArrowPos)
                        {
                            case 1: stat = "Strength"; break;
                            case 2: stat = "Toughness"; break;
                            case 3: stat = "Defence"; break;
                            case 4: stat = "Luck"; break;
                        }

                        for (int potionCount = 0; potionCount < Program.potions.Count; potionCount++)
                        {
                            if (Program.potions[potionCount].stat == stat)
                            {
                                key = potionCount;
                                potion = Program.potions[potionCount];
                            }
                        }

                        if (potion != null)
                        {
                            if (character.hasFought)
                            {
                                error = true;
                                notifications = $"{character.nickname} has already fought!!";
                            }
                            else
                                character.hasFought = true;

                            if (!error)
                            {
                                bool miss = character.LuckRoll(15);

                                if (miss)
                                    notifications = $"{enemy.creatureName} missed!";

                                enemy.Combat(character, round, miss);

                                character.PotionWearOff();

                                Program.potions.Remove(key);

                                character.ConsumePotion(potion);

                                notifications = $"{character.nickname} used a {potion.name}";

                                for (int potionName = 0; potionName < potionNames.Length; potionName++)
                                {
                                    potionNames[potionName] = " ";
                                }

                                for (int potionArrow = 0; potionArrow < potionArrows.Length; potionArrow++)
                                {
                                    potionArrows[potionArrow] = " ";
                                }

                                potionAction = false;
                            }
                        }
                        else
                        {
                            error = true;
                            notifications = "You do not have that kind of potion!";
                        }

                        SetUpNextState();
                        break;
                }

                if (potionAction)
                {
                    for (int potionArrow = 0; potionArrow < potionArrows.Length; potionArrow++)
                    {
                        potionArrows[potionArrow] = potionArrowPos == potionArrow + 1 ? ">" : " ";
                    }
                }
            }

            if (!characterSelected && !inputTaken)
            {
                inputTaken = true;

                charArrowPos = Program.ArrowGUI(charArrowPos, input, 3, 1, null);

                switch (input)
                {
                    case ConsoleKey.R:
                        error = true;

                        SetUpNextState();
                        break;

                    case ConsoleKey.Enter:
                        if (charArrows[0] == ">")
                            character = ben;   
                        else if (charArrows[1] == ">")
                            character = tim;
                        else if (charArrows[2] == ">")
                            character = ty;

                        if (character.alive)
                            characterSelected = true;
                        else
                        {
                            error = true;
                            notifications = $"{character.nickname} is dead!";
                        }
                        break;
                }

                for(int charArrow = 0; charArrow < charArrows.Length; charArrow++)
                {
                    charArrows[charArrow] = charArrowPos == charArrow + 1 ? ">" : " ";
                }
            }

            if (characterSelected && !inputTaken)
            {
                actionArrowPos = Program.ArrowGUI(actionArrowPos, input, 2, 2, null);

                switch (input)
                {
                    case ConsoleKey.R:
                        error = true;

                        SetUpNextState();
                        break;

                    case ConsoleKey.Enter:
                        ActionExecution();
                        break;
                }

                for(int actionArrow = 0; actionArrow < actionArrows.Length; actionArrow++)
                {
                    actionArrows[actionArrow] = actionArrowPos == actionArrow + 1 ? ">" : " ";
                }
            }
        }

        private void ActionExecution()
        {
            character.Attacking = actionArrows[0] == ">";
            character.Defending = actionArrows[1] == ">";
            character.UsingAbility = actionArrows[2] == ">";
            character.UsingPotion = actionArrows[3] == ">";

            character.CheckAbility(character, "Combat Game State");

            if (character.Attacking)
            {
                    notifications = $"{character.nickname} used Attack!!";
            }
            else if (character.Defending)
            {
                    notifications = $"{character.nickname} used Defend!!";
            }
            else if (character.UsingAbility)
            {
                if (character.abilityActive)
                {
                    error = true;
                    notifications = $"{character.abilityName} cannot be used this round!!";
                }
                else
                {
                    character.ActivateAbility();
                    notifications = $"{character.nickname} used {character.abilityName}!!";
                }

            }
            else if (character.UsingPotion)
            {
                error = true;

                if (Program.potions.Count > 0)
                {
                    for (int potion = 0; potion < Program.potions.Count; potion++)
                    {
                        if(Program.potions[potion] != null)
                        {
                            switch (Program.potions[potion].stat)
                            {
                                case "Strength": strengthPotions += 1; break;
                                case "Toughness": toughnessPotions += 1; break;
                                case "Defence": defencePotions += 1; break;
                                case "Luck": luckPotions += 1; break;
                            }
                        }
                    }

                    potionNames[0] = $"Strength Potion x {strengthPotions}";
                    potionNames[1] = $"Toughness Potion x {toughnessPotions}";
                    potionNames[2] = $"Defence Potion x {defencePotions}";
                    potionNames[3] = $"Luck Potion x {luckPotions}";

                    character.UsingPotion = false;
                    potionAction = true;
                }
                else
                {
                    notifications = "You do not have any potions!";
                    potionAction = false;
                }
            }
            else
            {
                error = true;
                notifications = "You cannot choose that option!";
            }

            if (!error)
            {
                if (character.hasFought)
                {
                    error = true;
                    notifications = $"{character.nickname} has already fought!!";
                }
                else
                    character.hasFought = true;
            }

            if (!error)
            {
                bool miss = character.LuckRoll(15);

                if (miss)
                    notifications = $"{enemy.creatureName} missed!";

                enemy.Combat(character, round, miss);

                if (character.Crit && character.Attacking || character.CalcAttack == character.Strength * 1.5 && character.Attacking)
                    notifications = $"{character.nickname} got a Critical Hit!";

                character.PotionWearOff();
            }
                
            SetUpNextState();
        }

        private void SetUpNextState()
        {
            if(enemy.Health <= 0)
                enemyDead = true;

            if (!error)
            {
                if (round >= roundCap || !character.alive)
                {
                    round = 1;

                    foreach(Character chara in characters)
                    {
                        chara.hasFought = false;
                    }

                    enemy.Roll();
                }
                else
                    ++round;
            }
            else
                error = false;

            foreach(Character chara in characters)
            {
                chara.Ready();
            }

            characterSelected = false;
        }

        public IGameState SwitchToScene()
        {
            IGameState nextState;

            if (enemyDead)
            {
                foreach(Character chara in characters)
                {
                    chara.PotionWearOff();
                    chara.hasFought = false;
                    chara.Ready();
                    chara.abilityActive = false;
                }

                nextState = new VictoryGameState(enemy);

                Program.enemysDefeated += 1;
                Program.points += enemy.Points;
                Program.Gold += enemy.gold;
            }
            else
                nextState = new CombatGameState(round, notifications, charArrows, charArrowPos, actionArrows, actionArrowPos, 
                    characterSelected, character, enemy, potionArrows, potionArrowPos, potionNames, potionAction);

            if (!ben.alive && !tim.alive && !ty.alive)
                nextState = new GameOverGameState();

            return nextState;
        }
    }
}
