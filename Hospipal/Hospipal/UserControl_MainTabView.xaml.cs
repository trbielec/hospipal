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
    /// Interaction logic for UserControl_MainTabView.xaml
    /// </summary>
    public partial class UserControl_MainTabView : UserControl
    {
        public UserControl_MainTabView()
        {
            InitializeComponent();

            WardsTab.Content = new UserControl_Wards();

        }
    }
}
