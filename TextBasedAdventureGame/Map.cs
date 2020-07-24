//Patrik Sullivan psullivan8@cnm.edu
//TextBasedAdventureGame
//7-16-20
//Map.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace TextBasedAdventureGame
{
    /// <summary>
    /// Class that represents the game. 
    /// Has a map and location of player.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// List of map locations.
        /// </summary>
        public List<MapLocation> Locations { get; set; }

        /// <summary>
        /// Constructor that creates the game map.
        /// </summary>
        public Map()
        {
            //Create map locations first
            Locations = new List<MapLocation>();           
            Locations.Add(new MapLocation("You are on a road leading to a town."));
            Locations.Add(new MapLocation("You are on a road in front of a saloon."));
            Locations.Add(new MapLocation("You are in a saloon."));
            Locations.Add(new MapLocation("You are on a road in front of a jail."));
            Locations.Add(new MapLocation("You are in a jail."));
            Locations.Add(new MapLocation("You are on a road in front of a general store."));
            Locations.Add(new MapLocation("You are in a general store."));
            Locations.Add(new MapLocation("You are in a tunnel leading to a mine shaft."));
            Locations.Add(new MapLocation("You are in a mine shaft."));
            Locations.Add(new MapLocation("You are in the middle of the desert."));
            Locations.Add(new MapLocation("You are at a well."));

            //Now add travel options to each map location

            //Road outside of town
            Locations[0].TravelOptions.Add(new TravelOption("Follow the road into town.",Locations[1]));

            //Road in front of salloon
            Locations[1].TravelOptions.Add(new TravelOption("Follow the road out of town.", Locations[0]));            
            Locations[1].TravelOptions.Add(new TravelOption("Enter the Salloon.", Locations[2]));            
            Locations[1].TravelOptions.Add(new TravelOption("Follow the road leading further into town.", Locations[3]));

            //Salloon
            Locations[2].TravelOptions.Add(new TravelOption("Exit the Salloon.", Locations[1]));

            //Road in front of jail
            Locations[3].TravelOptions.Add(new TravelOption("Follow the road out of town.", Locations[1]));
            Locations[3].TravelOptions.Add(new TravelOption("Enter the Jail.", Locations[4]));
            Locations[3].TravelOptions.Add(new TravelOption("Follow the road leading further into town.", Locations[5]));

            //Jail
            Locations[4].TravelOptions.Add(new TravelOption("Exit the Jail.", Locations[3]));
            Locations[4].TravelOptions.Add(new TravelOption("Enter a hidden tunnel under the jail.", Locations[7]));

            //Road in front of general store
            Locations[5].TravelOptions.Add(new TravelOption("Follow the road out of town.", Locations[3]));
            Locations[5].TravelOptions.Add(new TravelOption("Enter the General Store.", Locations[6]));
            Locations[5].TravelOptions.Add(new TravelOption("Follow the road toward the desert.", Locations[9]));

            //Store
            Locations[6].TravelOptions.Add(new TravelOption("Exit the General Store.", Locations[5]));

            //Tunnel to mine shaft
            Locations[7].TravelOptions.Add(new TravelOption("Head back to the jail.", Locations[4]));
            Locations[7].TravelOptions.Add(new TravelOption("Enter the Mine Shaft.", Locations[8]));

            //Mine Shaft
            Locations[8].TravelOptions.Add(new TravelOption("Exit the Mine Shaft.", Locations[7]));

            //Desert
            Locations[9].TravelOptions.Add(new TravelOption("Follow the path toward the well.", Locations[10]));
            Locations[9].TravelOptions.Add(new TravelOption("Head back into town.", Locations[5]));

            //Well
            Locations[10].TravelOptions.Add(new TravelOption("Head back toward the town.", Locations[9]));

            //Add some items
            //Map
            InventoryItem map = new InventoryItem("Map");
            Locations[1].Items.Add(map);

            //Safe and $100 Bill
            HidingPlace safe = new HidingPlace("Wall Safe");
            safe.HiddenObject = new InventoryItem("$100 Bill");
            //Add the safe to the Salloon
            Locations[2].Items.Add(safe);

            //Utility Box and Flashlight
            HidingPlace box = new HidingPlace("Utility Box");
            box.HiddenObject = new InventoryItem("Flashlight");
            //Add the utility box to the Salloon
            Locations[2].Items.Add(box);

            //Wanted Poster
            InventoryItem poster = new InventoryItem("Wanted Poster");
            Locations[4].Items.Add(poster);

            //Sheriff Badge
            InventoryItem badge = new InventoryItem("Sheriff Badge");
            Locations[4].Items.Add(badge);

            //Mine Cart and Golden Nugget
            HidingPlace cart = new HidingPlace("Mine Cart");
            cart.HiddenObject = new InventoryItem("Gold Nugget");
            //Add the Mine Cart to the Gold Mine
            Locations[8].Items.Add(cart);

            //Whiskey
            InventoryItem bottle = new InventoryItem("Bottle of Whiskey");
            Locations[6].Items.Add(bottle);

            //Barrel and Revolver
            HidingPlace barrel = new HidingPlace("Barrel");
            barrel.HiddenObject = new InventoryItem("Revolver");
            //Add the Mine Cart to the Gold Mine
            Locations[6].Items.Add(barrel);

            //Pickaxe
            InventoryItem axe = new InventoryItem("Pickaxe");
            Locations[7].Items.Add(axe);

            //Steer Skull
            HidingPlace skull = new HidingPlace("Steer Skull");
            skull.HiddenObject = new InventoryItem("Worms");
            Locations[9].Items.Add(skull);

            //Bucket and Key
            PortableHidingPlace bucket = new PortableHidingPlace("Bucket", 1, new InventoryItem("Key"));
            Locations[10].Items.Add(bucket);
        }
    }
}
