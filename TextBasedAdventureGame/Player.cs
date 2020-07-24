//Zoe Seamon-Baumgartner zseamonbaumgar@cnm.edu
//TextBasedAdventureGame
//7-24-20
//Player.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace TextBasedAdventureGame
{
    public class Player
    {
        private int inventorySize = 0;

        public List<IPortable> Inventory
        {
            get; set;
        }

        public bool AddInventoryItem(IPortable item)
        {

            //check to make sure inventory size is not full
            if (inventorySize != MaxInventory)
            {
                //check that item will fit in inventory
                if ((inventorySize += item.Size) <= MaxInventory)
                {
                    Inventory.Add(item);
                    Calc();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void RemoveInventoryItem(IPortable item)
        {
            try
            {
                //remove it from the list
                Inventory.Remove(item);
                Calc();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Calc()
        {
            //calculate inventory size
            inventorySize = Inventory.Count();
        }
        public List<IPortable> inventory;
        public int MaxInventory { get; set; }
        public MapLocation Location { get; set; }

        //Constructor with one parameter
        public Player(MapLocation location)
        {
            Location = location;
            MaxInventory = 8;
            Inventory = new List<IPortable>(10);
        }

        public Player()
        {
            MaxInventory = 8;
            Inventory = new List<IPortable>(10);
        }
    }
}