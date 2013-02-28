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
    /// Interaction logic for UserControl_AddWard.xaml
    /// </summary>
    public partial class UserControl_AddWard : Window
    {

        private bool _isNewWard = true;  //To use the same window for edits and adds.

        private UserControl_Wards ParentWardsWindow = null;

        //Required for databinding -- BEGIN
        /* Also if you look at the Text property of the Textboxes, you can see that the binding is done there.
         * Also it references the name of the control in the binding
         */
        private static readonly DependencyProperty WardProperty =
                          DependencyProperty.Register("ward", typeof(Ward),
                                                      typeof(UserControl_AddWard));
        private Ward ward
        {
            get { return (Ward)GetValue(WardProperty); }
            set { SetValue(WardProperty, value); }
        }
        //Required for databinding -- END

        public UserControl_AddWard(UserControl_Wards parentUC)
        {
            InitializeComponent(); //New patient can use default constructor
            ward = new Ward();
            ParentWardsWindow = parentUC as UserControl_Wards; 
        }

        public UserControl_AddWard(Ward Ward, UserControl_Wards parentUC)
        {
            InitializeComponent();
            _isNewWard = false;  //A new patient will not have a health care no to reference
            ward = Ward;
            ParentWardsWindow = parentUC as UserControl_Wards; 
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Save(object sender, RoutedEventArgs e)
        {
                SaveButton.Focus();  //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
                if (_isNewWard)
                {
                    ward.Insert();
                }
                else
                {
                    ward.Update();
                }

                ParentWardsWindow.WardDG.DataContext = Ward.GetWards();
                this.Close();
         }
    }
}
