//Patrik Sullivan psullivan8@cnm.edu
//TextBasedAdventureGame
//7-16-20
//HidingPlace.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    class HidingPlace : GameObject, IHidingPlace
    {
        public GameObject HiddenObject { get; set; }

        public HidingPlace()
        {

        }

        public HidingPlace(string description) : base(description)
        {
            Description = description;
        }

        public GameObject Search()
        {
            try
            {
                return HiddenObject;
            }
            finally
            {
                HiddenObject = null;
            }
        }
    }
}
