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
    /// Interaction logic for UserControl_Ward.xaml
    /// </summary>
    public partial class UserControl_AddRoom : Window
    {
        private bool _isNewRoom = true;  //To use the same window for edits and adds.

        private UserControl_Wards ParentWardsWindow = null;

        //Required for databinding -- BEGIN
        /* Also if you look at the Text property of the Textboxes, you can see that the binding is done there.
         * Also it references the name of the control in the binding
         */

        private static readonly DependencyProperty RoomProperty =
                         DependencyProperty.Register("room", typeof(Room),
                                                     typeof(UserControl_AddRoom));

        private Room room
        {
            get { return (Room)GetValue(RoomProperty); }
            set { SetValue(RoomProperty, value); }
        }

        private Ward _Ward;

        public UserControl_AddRoom(Ward ward, UserControl_Wards parentUC)
        {
            InitializeComponent();
            room = new Room();
            _Ward = ward;
            ParentWardsWindow = parentUC as UserControl_Wards;
        }

        public UserControl_AddRoom(Room Room, Ward ward, UserControl_Wards parentUC)
        {
            InitializeComponent();
            _isNewRoom = false;  //A new patient will not have a health care no to reference
            room = Room;
            _Ward = ward;
            ParentWardsWindow = parentUC as UserControl_Wards;

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            room.RoomNo = Convert.ToInt32(RoomNo.Text);
            room.WardName = _Ward.SlugName;
            SaveButton.Focus();  //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
            if (_isNewRoom)
            {
                room.Insert();
            }
            else
            {
                room.Update();
            }

            ParentWardsWindow.RoomDG.DataContext = Room.GetRooms(_Ward.SlugName);
                
            this.Close();
        }

    }
}
