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
        }

        private void boxWard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //query rooms in the selected ward and display them
        }

        private void boxRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //query beds in the selected room and display them
        }

        private void boxBed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WardAdd(object sender, MouseButtonEventArgs e)
        {

        }

        private void WardEdit(object sender, MouseButtonEventArgs e)
        {

        }

        private void WardDelete(object sender, MouseButtonEventArgs e)
        {
           
        }


        private void RoomAdd(object sender, MouseButtonEventArgs e)
        {

        }

        private void RoomEdit(object sender, MouseButtonEventArgs e)
        {

        }

        private void RoomDelete(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void BedAdd(object sender, MouseButtonEventArgs e)
        {

        }

        private void BedEdit(object sender, MouseButtonEventArgs e)
        {

        }

        private void BedDelete(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
