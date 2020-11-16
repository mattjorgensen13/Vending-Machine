using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class InventoryItem
    {
        VendingMachine vendingMachine = new VendingMachine();
        public string SlotPosition { get; private set; }
        public string Name { get; private set; }
        public  decimal Price { get; private set; }
        public string Type { get; private set; }
        public int Quantity { get; set; } = 5;
        public string SoundPlayed { get; private set; }
        public InventoryItem (string slot, string name, decimal price, string type)
        {
            SlotPosition = slot;
            Name = name;
            Price = price;
            Type = type;

            switch (Type)
            {
                case "Chip":
                    SoundPlayed = "Crunch Crunch, Yum!";
                    break;
                case "Candy":
                    SoundPlayed = "Munch Munch, Yum!";
                    break;
                case "Drink":
                    SoundPlayed = "Glug Glug, Yum!";
                    break;
                case "Gum":
                    SoundPlayed = "Chew Chew, Yum!";
                    break;
            }
        }
        public void InventoryRemaining()
        {
            if (Quantity == 0)
            {
                Name = "SOLD OUT";
            }
        }

    }
}
