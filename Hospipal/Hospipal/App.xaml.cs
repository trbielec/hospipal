﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void highlightBox(object sender, RoutedEventArgs a)
        {
            TextBox t = sender as TextBox;
            t.Background = System.Windows.Media.Brushes.LightGoldenrodYellow;

        }


        private void checkInput(object sender, TextCompositionEventArgs a)
        {
            Regex regex = new Regex("[^0-9]+");
            a.Handled = regex.IsMatch(a.Text);
        }

        private void checkPostalCode(object sender, TextCompositionEventArgs a)
        {
            TextBox text = (TextBox)a.OriginalSource;
            int modulus = text.Text.Count() % 2;
            switch (modulus)
            {
                case 0:
                    a.Handled = (char.IsDigit(a.Text[0]));
                    break;
                case 1:
                    a.Handled = (char.IsLetter(a.Text[0]));
                    break;
                default:
                    break;
            }
        }

        private void checkInputLetters(object sender, TextCompositionEventArgs a)
        {
            Regex regex = new Regex(@"[^\w\s.]");
            a.Handled = regex.IsMatch(a.Text);
        }

        private void noPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (textAllowed(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private bool textAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        private bool textAllowedLetters(string text)
        {
            Regex regex = new Regex(@"[^\w\s.]");
            return regex.IsMatch(text);
        }

        private void noSpaces(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
