using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Engine.Models;
using Engine.ViewModels;
namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public TradeScreen()
        {
            InitializeComponent();
        }
        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem =
            ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (groupedInventoryItem != null)
            {
                Session.CurrentPlayer.ReceiveGold(groupedInventoryItem.Item.Price);
                Session.CurrentTrader.AddItemToInventory(groupedInventoryItem.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(groupedInventoryItem.Item);
            }
        }

        private void OnClick_SellAll(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem =
                ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (groupedInventoryItem != null)
            {
                ///needs fixing
                var loopingList = Enumerable.Range(1, groupedInventoryItem.Quantity);
                foreach (var listItem in loopingList)
                {
                    Session.CurrentPlayer.ReceiveGold(groupedInventoryItem.Item.Price);
                    Session.CurrentTrader.AddItemToInventory(groupedInventoryItem.Item);
                    Session.CurrentPlayer.RemoveItemFromInventory(groupedInventoryItem.Item);
                }
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem =
                ((FrameworkElement) sender).DataContext as GroupedInventoryItem;
            if (groupedInventoryItem != null)
            {
                if (Session.CurrentPlayer.Gold >= groupedInventoryItem.Item.Price)
                {
                    Session.CurrentPlayer.SpendGold(groupedInventoryItem.Item.Price);
                    Session.CurrentTrader.RemoveItemFromInventory(groupedInventoryItem.Item);
                    Session.CurrentPlayer.AddItemToInventory(groupedInventoryItem.Item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }

        private void OnClick_BuyAll(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem =
                ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (groupedInventoryItem != null)
            {
                if (Session.CurrentPlayer.Gold >= groupedInventoryItem.Item.Price)
                {

                    //needs fixing
                    int totalGroupedItems = groupedInventoryItem.Quantity;
                    for (int i = totalGroupedItems; i > 0; i--)
                    {
                        Session.CurrentPlayer.SpendGold(groupedInventoryItem.Item.Price);
                        Session.CurrentTrader.RemoveItemFromInventory(groupedInventoryItem.Item);
                        Session.CurrentPlayer.AddItemToInventory(groupedInventoryItem.Item);
                    }
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }
    
        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}