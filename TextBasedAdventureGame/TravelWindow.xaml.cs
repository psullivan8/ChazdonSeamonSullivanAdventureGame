//Patrik Sullivan psullivan8@cnm.edu
//TextBasedAdventureGame
//7-16-20
//TravelWindow.xaml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextBasedAdventureGame
{
    /// <summary>
    /// Window that shows player where they are and provides capability to move from location to location in the map.
    /// </summary>
    public partial class TravelWindow : Window
    {
        GameObject[] objects = new GameObject[3];
        /// <summary>
        /// Game object that has map
        /// </summary>
        Map game;
        Player player;

        /// <summary>
        /// Initialize the form, the game and call display location to start the form.
        /// </summary>
        public TravelWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            objects[0] = new HidingPlace();
            objects[1] = new InventoryItem();
            objects[2] = new PortableHidingPlace();
            game = new Map();
            player = new Player(game.Locations[0]);
            DisplayLocation();
            GameStatus();
        }

        private void UpdateDisplay()
        {
            DisplayLocation();
            lbInventory.ItemsSource = player.Inventory;
            lbTravelOptions.ItemsSource = player.Location.TravelOptions;
            lbItems.ItemsSource = player.Location.Items;
            lbInventory.Items.Refresh();
            lbTravelOptions.Items.Refresh();
            lbItems.Items.Refresh();
            GameStatus();
        }

        /// <summary>
        /// Tells the player where they are.
        /// </summary>
        private void DisplayLocation()
        {
            txbLocationDescription.Text = player.Location.Description;
            lbTravelOptions.ItemsSource = player.Location.TravelOptions;
        }

        /// <summary>
        /// Double click a travel option to move to a new location on the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbTravelOptions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbTravelOptions.SelectedItem != null)
            {
                TravelOption to = (TravelOption)lbTravelOptions.SelectedItem;
                switch (lbTravelOptions.SelectedItem.ToString())
                {
                    case "Follow a dark tunnel under the jail.":
                        bool isDark = true;
                        foreach (var item in lbInventory.Items)
                        {
                            if (item.ToString() == "Flashlight")
                            {
                                isDark = false;
                            }
                        }
                        if (isDark)
                        {
                            MessageBox.Show("You need the flashlight to enter the tunnel.");
                        }
                        else
                        {
                            player.Location = to.Location;
                            DisplayLocation();
                            UpdateDisplay();
                        }
                        break;
                    default:
                        player.Location = to.Location;
                        DisplayLocation();
                        UpdateDisplay();
                        break;
                }
            }
            else
            {
                MessageBox.Show("No item is selected!");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Media.SoundPlayer search = new System.Media.SoundPlayer();
                search.SoundLocation = "Music/Search.wav";
                if (lbItems.SelectedItem != null)
                {
                    string type = lbItems.SelectedItem.GetType().ToString();
                    switch (type)
                    {
                        case "TextBasedAdventureGame.HidingPlace":
                            HidingPlace hidingPlace = (HidingPlace)lbItems.SelectedItem;
                            if (hidingPlace.HiddenObject != null)
                            {
                                InventoryItem item = new InventoryItem();
                                item = (InventoryItem)hidingPlace.Search();
                                player.Location.Items.Add(item);
                                search.Play();
                            }
                            else
                            {
                                MessageBox.Show("This item is empty.");
                            }
                            break;
                        case "TextBasedAdventureGame.PortableHidingPlace":
                            PortableHidingPlace portableHidingPlace = (PortableHidingPlace)lbItems.SelectedItem;
                            if (portableHidingPlace.HiddenObject != null)
                            {
                                InventoryItem item = new InventoryItem();
                                item = (InventoryItem)portableHidingPlace.Search();
                                player.Location.Items.Add(item);
                                search.Play();
                            }
                            else
                            {
                                MessageBox.Show("This item is empty.");
                            }
                            break;
                        default:
                            MessageBox.Show("This item cannot be searched.");
                            break;
                    }
                    UpdateDisplay();
                }
                else
                {
                    MessageBox.Show("You must select an item.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnTake_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Media.SoundPlayer take = new System.Media.SoundPlayer();
                take.SoundLocation = "Music/Take.wav";
                if (lbItems.SelectedItem != null)
                {
                    string type = lbItems.SelectedItem.GetType().ToString();
                    switch (type)
                    {
                        case "TextBasedAdventureGame.HidingPlace":
                            MessageBox.Show("You cannot add that item to your inventory!");
                            break;
                        case "TextBasedAdventureGame.PortableHidingPlace":
                            IPortable portableHidingPlace = (PortableHidingPlace)lbItems.SelectedItem;
                            if (player.AddInventoryItem(portableHidingPlace))
                            {
                                player.Location.Items.RemoveAt(lbItems.SelectedIndex);
                                take.Play();
                            }
                            else
                            {
                                MessageBox.Show("Your inventory is full! You must drop an item.");
                            }
                            break;
                        case "TextBasedAdventureGame.InventoryItem":
                            IPortable inventoryItem = (InventoryItem)lbItems.SelectedItem;
                            if (player.AddInventoryItem(inventoryItem))
                            {
                                player.Location.Items.RemoveAt(lbItems.SelectedIndex);
                                take.Play();
                            }
                            else
                            {
                                MessageBox.Show("Your inventory is full! You must drop an item.");
                            }
                            if (lbItems.SelectedItem.ToString() == "Worms")
                            {
                                MessageBox.Show("Why would you want to pick that up?!", "Eww!");
                            }
                            break;
                    }
                    UpdateDisplay();
                }
                else
                {
                    MessageBox.Show("You must select an item.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbInventory.SelectedItem != null)
                {
                    string type = lbInventory.SelectedItem.GetType().ToString();
                    switch (type)
                    {
                        case "TextBasedAdventureGame.PortableHidingPlace":
                            IPortable portableHidingPlace = (PortableHidingPlace)lbInventory.SelectedItem;
                            player.RemoveInventoryItem(portableHidingPlace);
                            player.Location.Items.Add((GameObject)portableHidingPlace);
                            break;
                        case "TextBasedAdventureGame.InventoryItem":
                            IPortable inventoryItem = (InventoryItem)lbInventory.SelectedItem;
                            player.RemoveInventoryItem(inventoryItem);
                            player.Location.Items.Add((GameObject)inventoryItem);
                            break;
                    }
                    UpdateDisplay();
                    System.Media.SoundPlayer drop = new System.Media.SoundPlayer();
                    drop.SoundLocation = "Music/Drop.wav";
                    drop.Play();
                }
                else
                {
                    MessageBox.Show("You must select an item.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void GameStatus()
        {
            string summary = "Find the Gold Nugget, $100 Bill, and Key to win the game.";
            summary += "\nYou have " + lbInventory.Items.Count + " items in your inventory.";
            summary += " There are " + (player.MaxInventory - lbInventory.Items.Count) + " slots remaining.";
            //When we decide on a storyline and objects that need to be found, I will update this to inform
            //the player of there progress and the items remaining to be found.
            int count = 0;
            for (int i = 0; i < lbInventory.Items.Count; i++)
            {
                count = 0;
                foreach (var item in lbInventory.Items)
                {
                    switch (item.ToString())
                    {
                        case "Gold Nugget":
                            count++;
                            break;
                        case "$100 Bill":
                            count++;
                            break;
                        case "Key":
                            count++;
                            break;
                    }
                }
            }
            summary += "\nYou have " + count + " out of 3 game winning items.";
            txtGameStatus.Text = summary;
            if (count == 3)
            {
                if (MessageBox.Show("You win! Would you like to play again?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TravelWindow travel = new TravelWindow();
                    Application.Current.Windows[0].Close();
                    travel.ShowDialog();
                }
                else
                {
                    this.Close();
                }
            }
        }
        void MediaElement_Ended(object sender, RoutedEventArgs e)
        {
            myMediaElement.Position = TimeSpan.FromMilliseconds(1);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
                myMediaElement.Play();
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (chkMute.IsChecked == true)
            {
                myMediaElement.Volume = 0;
            }
            if(chkMute.IsChecked == false)
            {
                myMediaElement.Volume = 10;
            }
        }
    }
}