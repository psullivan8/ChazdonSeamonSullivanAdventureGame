//Patrik Sullivan psullivan8@cnm.edu
//TextBasedAdventureGame
//7-16-20
//InventoryItem.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class InventoryItem : GameObject, IPortable
    {

        public int Size { get; set; }

        //one parameter constructor passes description
        public InventoryItem(string description) : base(description)
        {
            Description = description;
        }

        public InventoryItem()
        {
        }
    }
}