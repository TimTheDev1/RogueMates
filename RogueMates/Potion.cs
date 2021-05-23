using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class Potion
    {
        public string name;
        public string stat;
        public int statValue;
        public int price;

        public Potion(string stat, int statValue, int price)
        {
            this.stat = stat;
            this.statValue = statValue;
            this.price = price;

            name = $"{stat} Potion";
        }
    }
}
