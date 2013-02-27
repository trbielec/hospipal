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
        List<Ward> Wards;

        public UserControl_Wards()
        {
            InitializeComponent();
            Wards = Ward.GetWards();
            WardDG.DataContext = Wards;
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
            if (WardDG.SelectedItems.Count > 0)
            {
                Wards[WardDG.SelectedIndex].Delete();
                Wards.RemoveAt(WardDG.SelectedIndex);

                WardDG.Items.Refresh();
            }
        }


        private void RoomAdd(object sender, RoutedEventArgs e)
        {
            UserControl_AddRoom myWindow = new UserControl_AddRoom(((Ward)WardDG.SelectedItem));

            myWindow.ShowDialog();
        }

        private void RoomEdit(object sender, RoutedEventArgs e)
        {
            UserControl_AddRoom myWindow = new UserControl_AddRoom(((Room)RoomDG.SelectedItem),((Ward)WardDG.SelectedItem));

            myWindow.ShowDialog();
        }

        private void RoomDelete(object sender, RoutedEventArgs e)
        {
            
        }

        private void BedAdd(object sender, RoutedEventArgs e)
        {
            Bed newBed;
            if (BedDG.Items.Count > 0 && BedDG.Items.Count < 99) //hard limit 99 beds in one room
            {
                Bed LastBed = ((Bed)BedDG.Items[BedDG.Items.Count - 1]);
                newBed = new Bed(LastBed.bedNo + 1, (Bed.States)1, 0, LastBed.roomNo, "");  //Non-patients have a patient id of 0.
            }
            else
            {
                string roomNo = ((Room)RoomDG.SelectedItem).RoomNo;
                newBed = new Bed(1, (Bed.States)1, 0, roomNo, "");
            }
            newBed.Insert();

        }

        private void BedDelete(object sender, RoutedEventArgs e)
        {
            Bed LastBed = ((Bed)BedDG.Items[BedDG.Items.Count - 1]);
            LastBed.Delete();
        }

        private void WardDGCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (WardDG.SelectedItem != null)
            {
                List<Room> rooms = Room.GetRooms(((Ward)WardDG.SelectedItem).WardName);
                RoomDG.DataContext = rooms;
                BedDG.DataContext = new List<Bed>();
            }
        }
        private void RoomDGCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (RoomDG.SelectedItem != null)
            {
                List<Bed> beds = Bed.GetBeds(((Room)RoomDG.SelectedItem).RoomNo);
                BedDG.DataContext = beds;
            }
        }
    }
}
