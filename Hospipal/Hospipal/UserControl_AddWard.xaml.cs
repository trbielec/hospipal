﻿using System;
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
    public partial class UserControl_AddWard : Window
    {
        public UserControl_AddWard()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
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

        private void Cancel(object sender, RoutedEventArgs e)
        {
            //When user clicks on X the Wards view is displayed
            this.Close();
        }
    }
}
