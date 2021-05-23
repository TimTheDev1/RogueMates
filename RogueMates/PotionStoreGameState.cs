using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace RogueMates
{
    class PotionStoreGameState : IGameState
    {
        private string[] arrows = new string[8];
        private int arrowPos = 1;
        private IGameState nextState;
        private int healingPrice = 100;
        private Potion strengthPotion = new Potion("Strength", 15, 50);
        private Potion toughnessPotion = new Potion("Toughness", 15, 50);
        private Potion defencePotion = new Potion("Defence", 15, 50);
        private Potion luckPotion = new Potion("Luck", 15, 50);
        private bool buyPotion = false;
        private bool heal = false;
        private Potion potionSold;
        private int healAmount = 100;
        private Character character;

        public PotionStoreGameState()
        {
            for(int arrow = 0; arrow < arrows.Length; arrow++)
            {
                arrows[arrow] = " ";
            }

            arrows[0] = ">";
        }

        public PotionStoreGameState(string[] arrows, int arrowPos)
        {
            this.arrows = arrows;
            this.arrowPos = arrowPos;
        }

        public void Display()
        {
            Console.Clear();

            string[] information = new string[Program.information.Length];

            information[0] = @"                     )  )                     ";
            information[1] = @"                    (  (                  /\  ";
            information[2] = @"                     (_)                 /  \ ";
            information[3] = @"             ________[_]________      /\/    \";
            information[4] = @"    /\      /\        ______    \    /   /\/\ ";
            information[5] = @"   /  \    //_\       \    /\    \  /\/\/    \";
            information[6] = @"  / /\/\  //___\       \__/  \    \/          ";
            information[7] = @" /\/    \//_____\       \ |[]|     \          ";
            information[8] = @"/       //_______\       \|__|      \         ";
            information[9] = @"       /XXXXXXXXXX\                  \        ";
            information[10] = @"      /_I_II  I__I_\__________________\       ";
            information[11] = @"        I_I|  I__I_____[]_|_[]_____I          ";
            information[12] = @"        I_II  I__I_____[]_|_[]_____I          ";
            information[13] = @"        I II__I  I     XXXXXXX     I          ";
            information[14] = "      ~~~~~'   '~~~~~~~~~~~~~~~~~~~~~~~~       ";
            information[15] = "";
            information[16] = $"You found a Potion Store!!";
            information[17] = "";
            information[18] = $"They have an assortment of Potions to buy";
            information[19] = $"and a healing well that heals you by {healAmount}";
            information[20] = "";
            information[21] = $"{arrows[0]} Strength Potion - {strengthPotion.price} gold";
            information[22] = $"{arrows[1]} Toughness Potion - {toughnessPotion.price} gold";
            information[23] = $"{arrows[2]} Defence Potion - {defencePotion.price} gold";
            information[24] = $"{arrows[3]} Luck Potion - {luckPotion.price} gold";
            information[25] = "";
            information[26] = $"{arrows[4]} Heal Ben - {healingPrice} gold";
            information[27] = $"{arrows[5]} Heal Tim - {healingPrice} gold";
            information[28] = $"{arrows[6]} Heal Ty - {healingPrice} gold";
            information[29] = "";
            information[30] = $"{arrows[7]} Go back";

            Program.information = information;

            Program.GameStateDisplay(ConsoleColor.Magenta, ConsoleColor.Green);
        }

        public void HandleInput(ConsoleKey input)
        {
            buyPotion = arrowPos < 5;
            heal = Enumerable.Range(5, 3).Contains(arrowPos);

            arrowPos = Program.ArrowGUI(arrowPos, input, 1, 8, null);

            nextState = new PotionStoreGameState(arrows, arrowPos);

            switch (input)
            {
                case ConsoleKey.Enter:

                    if (buyPotion)
                    {
                        int cap = Program.potions.Count + 1;

                        nextState = new PotionStoreGameState(arrows, arrowPos);

                        for (int potion = 0; potion < cap; potion++)
                        {
                            if (Program.potions.Count == potion)
                            {
                                cap = potion;

                                switch (arrowPos)
                                {
                                    case 1: potionSold = strengthPotion; break;
                                    case 2: potionSold = toughnessPotion; break;
                                    case 3: potionSold = defencePotion; break;
                                    case 4: potionSold = luckPotion; break;
                                }

                                if (potionSold != null)
                                    Program.potions[potion] = potionSold;

                                if (Program.potions[potion].price <= Program.Gold)
                                {
                                    nextState = new ExplorationGameState();
                                    Program.Gold -= Program.potions[potion].price;
                                }
                                else
                                    Program.potions.Remove(potion);
                            }
                        }
                    }
                    else if (heal)
                    {
                        if (Program.Gold >= healingPrice)
                        {
                            nextState = new ExplorationGameState();

                            switch (arrowPos)
                            {
                                case 5: character = Program.ben; break;
                                case 6: character = Program.tim; break;
                                case 7: character = Program.ty; break;
                            }

                            if (character.alive)
                            {
                                character.Health += healAmount;
                                Program.Gold -= 100;
                            }
                        }
                        else
                            nextState = new PotionStoreGameState(arrows, arrowPos);
                    }
                    else
                        nextState = new ExplorationGameState();

                    break;
            }

            for(int arrow = 0; arrow < 8; arrow++)
            {
                arrows[arrow] = arrowPos == (arrow + 1) ? ">" : " ";
            }
        }

        public IGameState SwitchToScene()
        {
            if (nextState == null)
                nextState = new PotionStoreGameState(arrows, arrowPos);

            return nextState;
        }
    }
}
