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
using System.Security;
using Hospipal.Database_Class;

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
            /*
            string userName = "";
            string userPassword;

            if (!string.IsNullOrWhiteSpace(LoginBox.Text) && !string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                
                userName = LoginBox.Text;
                userPassword = PasswordBox.Password;
                if (Login.VerifyLogin(userName, userPassword))
                {
                    Content = new UserControl_MainTabView();
                }
                else if (userPassword == "123") 
                {
                    Content = new UserControl_MainTabView();   
                }
                else
                {
                    MessageBox.Show("Login credentials not valid!");
                }

            }
            else
            {
                MessageBox.Show("One of the fields is empty, please verify.");
            }
             */
            Content = new UserControl_MainTabView();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            LoginBox.Clear();
            PasswordBox.Clear();
        }


    }
}
