//Patrik Sullivan psullivan8@cnm.edu
//TextBasedAdventureGame
//7-16-20
//PortableHidingPlace.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class PortableHidingPlace : GameObject, IHidingPlace, IPortable
    {
        private GameObject hiddenObject;
        public PortableHidingPlace()
        {
        }

        public PortableHidingPlace(string description, int size, GameObject hiddenObject)
        {
            Description = description;
            Size = size;
            HiddenObject = hiddenObject;
        }
        public GameObject HiddenObject
        {
            get { return hiddenObject; }
            set { hiddenObject = value; }
        }
        public int Size { get; set; }

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