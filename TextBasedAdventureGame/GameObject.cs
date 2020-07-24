//Patrik Sullivan psullivan8@cnm.edu
//TextBasedAdventureGame
//7-16-20
//GameObject.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class GameObject
    {
        public virtual string Description {get; set;}

        //default constructor
        public GameObject()
        {
        }

        //one parameter constructor
        public GameObject(string description)
        {
            Description = description;
        }

        //ToString method returns the Description
        public override string ToString()
        {
            return Description.ToString();
        }
    }
}