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
using Hospipal.Database_Class;


namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_Wards.xaml
    /// </summary>
    public partial class UserControl_Wards : UserControl
    {
        public UserControl_Wards()
        {
            InitializeComponent();
            List<Ward> wards = Ward.GetWards();
            WardDG.DataContext = wards;
        }
        private void WardAdd(object sender, RoutedEventArgs e)
        {
            UserControl_AddWard myWindow = new UserControl_AddWard();
           
            myWindow.ShowDialog();
        }

        private void WardEdit(object sender, RoutedEventArgs e)
        {
            if (WardDG.SelectedItems.Count > 0)
            {
                UserControl_AddWard myWindow = new UserControl_AddWard((Ward)WardDG.SelectedItem);

                myWindow.ShowDialog();
            }
        }

        private void WardDelete(object sender, RoutedEventArgs e)
        {
           
        }


        private void RoomAdd(object sender, RoutedEventArgs e)
        {
            UserControl_AddRoom myWindow = new UserControl_AddRoom();

            myWindow.ShowDialog();
        }

        private void RoomEdit(object sender, RoutedEventArgs e)
        {
            UserControl_AddRoom myWindow = new UserControl_AddRoom();

            myWindow.ShowDialog();
        }

        private void RoomDelete(object sender, RoutedEventArgs e)
        {
            
        }

        private void BedAdd(object sender, RoutedEventArgs e)
        {
           // List<object[]> bedNumbers = Database.Select("SELECT bed_no from Bed");

           // int i = 1;
           // string nameString;
           // foreach (object[] number in bedNumbers)
           // {
                //_b = number[1].ToString();
              //  if ((Regex.Match(nameString, @"\d+").Value) == i)
              //  {
              //  }

              //  i++;
          //  }
        }

        private void BedEdit(object sender, RoutedEventArgs e)
        {

        }

        private void BedDelete(object sender, RoutedEventArgs e)
        {
            
        }

        private void WardDGCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            List<Room> rooms = Room.GetRooms(((Ward)WardDG.SelectedItem).WardName);
            RoomDG.DataContext = rooms;
        }
    }
}
