using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RogueMates
{
    class Equipment
    {
        public double dexterityCost;
        public string quality;
        public string type;
        public string stat;
        public int  statValue;
        public string name;
        public string statString;
        private int maxQuality;

        public Equipment()
        {
            dexterityCost = 0;
            quality = "";
            type = "";
            stat = "";
            statValue = 0; 
            statString = "               ";
            maxQuality = 0;
            name = $"                ";
        }

        public Equipment(Random random)
        {
            int[] charaterDexterity = { Program.ben.Dexterity, Program.tim.Dexterity, Program.ty.Dexterity };

            do
            {
                switch (random.Next(1, 5))
                {
                    case 1:
                        maxQuality = 31;
                        type = "Gauntlet";
                        stat = "Strength:";
                        break;
                    case 2:
                        maxQuality = 31;
                        type = "Shield";
                        stat = "Defence:";
                        break;
                    case 3:
                        maxQuality = 31;
                        type = "Charm";
                        stat = "Luck:";
                        break;
                    case 4:
                        maxQuality = 31;
                        type = "Armour";
                        stat = "Toughness:";
                        break;
                }

                statValue = random.Next(1, maxQuality);

                double statOverMax = (double)statValue / (double)maxQuality;
                dexterityCost = statOverMax * 99;

            } while (dexterityCost > charaterDexterity.Max());


            int goldQuality = (int)(maxQuality * 0.67);
            int silverQuality = (int)(maxQuality * 0.335);

            if (Enumerable.Range(goldQuality, maxQuality - goldQuality).Contains(statValue))
                quality = "Gold";
            else if (Enumerable.Range(silverQuality, goldQuality - silverQuality).Contains(statValue))
                quality = "Silver";
            else if (Enumerable.Range(1, silverQuality - 1).Contains(statValue))
                quality = "Bronze";

            name = $"{quality} {type}";

            while (name.Length < 17)
            {
                name += " ";
            }

            statString = $"{stat} {statValue}";

            while (statString.Length < 16)
            {
                statString += " ";
            }
        }
    }
}
