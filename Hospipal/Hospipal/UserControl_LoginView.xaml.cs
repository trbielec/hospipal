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
using Telerik.Windows.Controls;

namespace Hospipal
{
   
    /// <summary>
    /// Interaction logic for UserControl_LoginView.xaml
    /// </summary>
    public partial class UserControl_LoginView : UserControl
    {
        public UserControl_LoginView()
        {
            InitializeComponent();

            ButtonLogin.Click += new RoutedEventHandler(ButtonLogin_Click);
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            RadTransitionControl transControl = this.Parent as RadTransitionControl;
            transControl.Content = new UserControl_MainTabView();
        }


    }
}
