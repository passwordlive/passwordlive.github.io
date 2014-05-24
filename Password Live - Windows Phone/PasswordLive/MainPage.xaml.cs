using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace PasswordLive
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool IncorrectLengthValue = false;
        const string secretKeywordText_Saved = "Secret keyword (saved)";
        const string secretKeywordText_NotSaved = "Secret keyword";

        private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;

        private void Compute()
        {
            int length;
            bool isInt = int.TryParse(this.length_textBox.Text, out length);
            if (isInt)
            {
                IncorrectLengthValue = false;

                if ((this.secretKeyword_passwordBox.Password.Length > 0) && (this.whatFor_textBox.Text.Length > 0))
                {
                    this.result_textBox.Text = Generator.Compute(this.secretKeyword_passwordBox.Password, this.whatFor_textBox.Text, Convert.ToInt32(this.length_textBox.Text), (bool)this.upperCase_checkBox.IsChecked, (bool)this.lowerCase_checkBox.IsChecked, (bool)this.numbers_checkBox.IsChecked, (bool)this.specialCharacters_checkBox.IsChecked);
                }
                else
                {
                    this.result_textBox.Text = string.Empty;
                }
            }
            else
            {
                this.result_textBox.Text = string.Empty;
                IncorrectLengthValue = true;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            Visibility v = (Visibility)Resources["PhoneLightThemeVisibility"];
            if (v == System.Windows.Visibility.Visible)
            {
                background_ImageBrush.ImageSource = null;
                generator_Grid.Background = new SolidColorBrush(Colors.Transparent);
                options_rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                BitmapImage bi = new BitmapImage(new Uri("ThemeDark.png", UriKind.RelativeOrAbsolute));
                background_ImageBrush.ImageSource = bi;
                generator_Grid.Background = new SolidColorBrush(Color.FromArgb(42, 0, 0, 0));
                options_rectangle.Fill = new SolidColorBrush(Color.FromArgb(30, 0, 0, 0));
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            try
            {
                this.landscape_CheckBox.IsChecked = (bool)userSettings["useLandscape"];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                userSettings.Add("useLandscape", true);
                userSettings.Save();
            }

            if (landscape_CheckBox.IsChecked == true)
            {
                PhoneAppPage.SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            }
            else
            {
                PhoneAppPage.SupportedOrientations = SupportedPageOrientation.Portrait;
            }

            try
            {
                this.rememberKeyword_CheckBox.IsChecked = (bool)userSettings["saveKeyword"];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                userSettings.Add("saveKeyword", false);
                userSettings.Save();
            }

            if (rememberKeyword_CheckBox.IsChecked == true)
            {
                this.secretKeyword_TextBlock.Text = secretKeywordText_Saved;
            }
            else
            {
                this.secretKeyword_TextBlock.Text = secretKeywordText_NotSaved;
            }

            try
            {
                if ((string)userSettings["keyword"] != string.Empty)
                {
                    this.secretKeyword_passwordBox.Password = (string)userSettings["keyword"];
                }

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                userSettings.Add("keyword", string.Empty);
                userSettings.Save();
            }
        }

        private void result_textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.result_textBox.SelectAll();
        }

        private void rememberKeyword_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (rememberKeyword_CheckBox.IsChecked == true)
            {
                userSettings["saveKeyword"] = true;
                userSettings["keyword"] = this.secretKeyword_passwordBox.Password;
                userSettings.Save();
                this.secretKeyword_TextBlock.Text = secretKeywordText_Saved;
            }
            else
            {
                userSettings["saveKeyword"] = false;
                userSettings["keyword"] = string.Empty;
                userSettings.Save();
                this.secretKeyword_TextBlock.Text = secretKeywordText_NotSaved;
            }
        }

        private void secretKeyword_passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (rememberKeyword_CheckBox.IsChecked == true)
            {
                userSettings["keyword"] = this.secretKeyword_passwordBox.Password;
                userSettings.Save();
                this.secretKeyword_TextBlock.Text = secretKeywordText_Saved;
            }
            else
            {
                userSettings["keyword"] = string.Empty;
                userSettings.Save();
                this.secretKeyword_TextBlock.Text = secretKeywordText_NotSaved;
            }
        }

        private void landscape_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (landscape_CheckBox.IsChecked == true)
            {
                PhoneAppPage.SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
                userSettings["useLandscape"] = true;
                userSettings.Save();
            }
            else
            {
                PhoneAppPage.SupportedOrientations = SupportedPageOrientation.Portrait;
                userSettings["useLandscape"] = false;
                userSettings.Save();
            }
        }

        private void length_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IncorrectLengthValue)
                this.length_textBox.Text = "15";
            else
            {
                int length;
                bool isInt = int.TryParse(this.length_textBox.Text, out length);
                if (isInt)
                {
                    if (length < Generator.MinimumLength)
                    {
                        this.length_textBox.Text = Generator.MinimumLength.ToString();
                        this.Compute();
                    }
                    else if (length > Generator.MaximumLength)
                    {
                        this.length_textBox.Text = Generator.MaximumLength.ToString();
                        this.Compute();
                    }
                }
            }
        }

        private void reset_Button_Click(object sender, RoutedEventArgs e)
        {
            if (rememberKeyword_CheckBox.IsChecked == false)
                this.secretKeyword_passwordBox.Password = string.Empty;

            this.whatFor_textBox.Text = string.Empty;
            this.length_textBox.Text = "15";
            this.upperCase_checkBox.IsChecked = true;
            this.lowerCase_checkBox.IsChecked = true;
            this.numbers_checkBox.IsChecked = true;
            this.specialCharacters_checkBox.IsChecked = true;
        }

        private void secretKeyword_passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Compute();
        }

        private void whatFor_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Compute();
        }

        private void length_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Compute();
        }

        private void upperCase_checkBox_Click(object sender, RoutedEventArgs e)
        {
            this.Compute();
        }

        private void lowerCase_checkBox_Click(object sender, RoutedEventArgs e)
        {
            this.Compute();
        }

        private void numbers_checkBox_Click(object sender, RoutedEventArgs e)
        {
            this.Compute();
        }

        private void specialCharacters_checkBox_Click(object sender, RoutedEventArgs e)
        {
            this.Compute();
        }

        private void result_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Compute();

            if (result_textBox.Text.Length > 27)
            {
                this.result_textBox.FontStretch = FontStretches.Condensed;
                this.result_textBox.FontSize = 17;
            }
            else
            {
                this.result_textBox.FontStretch = FontStretches.Normal;
                this.result_textBox.FontSize = 25.333;
            }
            this.result_textBox.SelectAll();
        }
    }
}