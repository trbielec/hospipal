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
        private List<Ward> Wards;
        private List<Room> Rooms;

        public UserControl_Wards()
        {
            InitializeComponent();

            Wards = Ward.GetWards();
            WardDG.DataContext = Wards;
        }
        private void WardAdd(object sender, RoutedEventArgs e)
        {
            UserControl_AddWard myWindow = new UserControl_AddWard(this);

            myWindow.ShowDialog();
        }

        private void WardEdit(object sender, RoutedEventArgs e)
        {
            if (WardDG.SelectedItems.Count > 0)
            {
                UserControl_AddWard myWindow = new UserControl_AddWard((Ward)WardDG.SelectedItem, this);

                myWindow.ShowDialog();
            }
        }

        private void WardDelete(object sender, RoutedEventArgs e)
        {
            if (WardDG.SelectedItems.Count > 0)
            {
                //List<Ward> WardToDel = Ward.GetWards();

                List<Room> roomsToDel = Room.GetRooms(((Ward)WardDG.SelectedItem).SlugName);
                foreach (Room roomRow in roomsToDel)
                {
                    List<Bed> bedsToDel = Bed.GetBeds(roomRow.RoomNo, roomRow.WardName);
                    foreach (Bed bedRow in bedsToDel)
                    {
                        bedRow.Delete();
                    }

                    roomRow.Delete();
                }

                ((Ward)WardDG.SelectedItem).Delete();

                BedDG.DataContext = null;
                BedDG.Items.Refresh();

                RoomDG.DataContext = null;
                RoomDG.Items.Refresh();

                WardDG.DataContext = Ward.GetWards();
                WardDG.Items.Refresh();
            }
        }







        private void RoomAdd(object sender, RoutedEventArgs e)
        {
            UserControl_AddRoom myWindow = new UserControl_AddRoom(((Ward)WardDG.SelectedItem), this);

            myWindow.ShowDialog();
        }

        private void RoomEdit(object sender, RoutedEventArgs e)
        {
            UserControl_AddRoom myWindow = new UserControl_AddRoom(((Room)RoomDG.SelectedItem), ((Ward)WardDG.SelectedItem), this);

            myWindow.ShowDialog();
        }

        private void RoomDelete(object sender, RoutedEventArgs e)
        {
            if (RoomDG.SelectedItems.Count > 0)
            {
                List<Bed> bedsToDel = Bed.GetBeds(((Room)RoomDG.SelectedItem).RoomNo, ((Room)RoomDG.SelectedItem).WardName);
                foreach (Bed row in bedsToDel)
                {
                    row.Delete();
                }


                BedDG.DataContext = Bed.GetBeds(((Room)RoomDG.SelectedItem).RoomNo, ((Room)RoomDG.SelectedItem).WardName);
                BedDG.Items.Refresh();

                ((Room)RoomDG.SelectedItem).Delete();

                RoomDG.DataContext = Room.GetRooms(((Ward)WardDG.SelectedItem).SlugName);
                RoomDG.Items.Refresh();
            }
        }







        private void BedAdd(object sender, RoutedEventArgs e)
        {
            Bed newBed;
            if (BedDG.Items.Count < 99 && RoomDG.SelectedItems.Count > 0)
            {
                if (BedDG.Items.Count > 0) //hard limit 99 beds in one room
                {
                    Bed LastBed = ((Bed)BedDG.Items[BedDG.Items.Count - 1]);
                    newBed = new Bed(LastBed.bedNo + 1, Bed.States.Available, 0, LastBed.roomNo, "", LastBed.ward);  //Non-patients have a patient id of 0.
                }
                else
                {
                    int roomNo = ((Room)RoomDG.SelectedItem).RoomNo;
                    string wardName = ((Ward)WardDG.SelectedItem).SlugName;
                    newBed = new Bed(1, Bed.States.Available, 0, roomNo, "", wardName);
                }
                newBed.Insert();


                List<Bed> beds = Bed.GetBeds(((Room)RoomDG.SelectedItem).RoomNo, ((Room)RoomDG.SelectedItem).WardName);
                BedDG.DataContext = beds;
                BedDG.Items.Refresh();
            }


        }

        private void BedDelete(object sender, RoutedEventArgs e)
        {
            if (BedDG.Items.Count > 0)
            {
                Bed LastBed = ((Bed)BedDG.Items[BedDG.Items.Count - 1]);
                LastBed.Delete();

                List<Bed> beds = Bed.GetBeds(((Room)RoomDG.SelectedItem).RoomNo, ((Room)RoomDG.SelectedItem).WardName);
                BedDG.DataContext = beds;
                BedDG.Items.Refresh();
            }

        }

        private void WardDGCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (WardDG.SelectedItem != null)
            {
                List<Room> rooms = Room.GetRooms(((Ward)WardDG.SelectedItem).SlugName);
                RoomDG.DataContext = rooms;
                BedDG.DataContext = new List<Bed>();
            }
        }
        private void RoomDGCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (RoomDG.SelectedItem != null)
            {
                List<Bed> beds = Bed.GetBeds(((Room)RoomDG.SelectedItem).RoomNo, ((Room)RoomDG.SelectedItem).WardName);
                BedDG.DataContext = beds;
            }
        }
    }
}
