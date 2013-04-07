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
    /// Interaction logic for PasswordReset.xaml
    /// </summary>
    public partial class PasswordReset : Window
    {
        private bool status;
        private string password;
        private string repeatPassword;
        private string oldPassword;
        private const int PASSWORD_LENGTH = 3;

        public bool Status
        {
            get 
            {
                return status;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
        }

        public string RepeatPassword
        {
            get
            {
                return repeatPassword;
            }
        }

        public PasswordReset()
        {
            InitializeComponent();
            status = false;
            password = null;
            repeatPassword = null;
        }

        public PasswordReset(string oldPass)
        {
            InitializeComponent();
            status = false;
            password = null;
            repeatPassword = null;
            oldPassword = oldPass;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            password = this.PasswordBox.Password;
            repeatPassword = this.RepeatPasswordBox.Password;

            if (password.Length <= PASSWORD_LENGTH || repeatPassword.Length <= PASSWORD_LENGTH)
            {
                MessageBox.Show("Passwords must me larger than " + PASSWORD_LENGTH + " characters");
            }
            else
            {
                if ((password != null && repeatPassword != null) && password == repeatPassword)
                {
                    if (oldPassword != password)
                    {
                        status = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please use a different password than your old password");
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match, Please try again!");
                }
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            status = false;
            this.Close();
        }
    }
}
