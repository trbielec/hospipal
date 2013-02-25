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
    /// Interaction logic for UserControl_AddWard.xaml
    /// </summary>
    public partial class UserControl_AddWard : UserControl
    {
        public UserControl_AddWard()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Save(object sender, MouseButtonEventArgs e)
        {
            //TODO

            //if (_isNewEmployee)
            // {
            //Add a new employee based on entered data
            //}
            // else
            // {
            //Update current employee
            // }
        }

        private void Cancel(object sender, MouseButtonEventArgs e)
        {
            //When user clicks on X the Wards view is displayed
            Content = new UserControl_Wards();
        }
    }
}
