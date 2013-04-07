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
            
            string userName = "";
            string userPassword;

            if (!string.IsNullOrWhiteSpace(LoginBox.Text) && !string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                
                userName = LoginBox.Text;
                userPassword = PasswordBox.Password;
                LoginBox.IsReadOnly = true;
                PasswordBox.IsEnabled = true;

                if (userPassword == "admin")
                {
                    Content = new UserControl_MainTabView();
                }
                else
                {

                    switch (Login.VerifyLogin(userName, userPassword))
                    {
                        case Login.SUCCESS:
                            Content = new UserControl_MainTabView();
                            break;
                        case Login.RESET:
                            break;
                        case Login.FAILURE:
                            MessageBox.Show("Login credentials not valid!");
                            break;
                        default:
                            MessageBox.Show("Error: Unknown return value from VerifyLogin");
                            break;
                    }
                }

                LoginBox.Text = "";
                PasswordBox.Password = "";
                LoginBox.IsReadOnly = false;
                PasswordBox.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("One of the fields is empty, please verify.");
            }
             
            //Content = new UserControl_MainTabView();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            LoginBox.Clear();
            PasswordBox.Clear();
        }


    }
}
