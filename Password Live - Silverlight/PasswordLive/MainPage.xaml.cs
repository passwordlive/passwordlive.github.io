using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PasswordLive
{
    public partial class MainPage : UserControl
    {
        private static Generator gen = new Generator();
        private bool IncorrectLengthValue = false;

        public MainPage()
        {
            InitializeComponent();
            if (!Application.Current.IsRunningOutOfBrowser)
            {
                this.canvasWindowControl.Visibility = System.Windows.Visibility.Collapsed;
                this.borderWindowControl.Background.Opacity = 0.25;
                this.hyperlinkButtonOpenHomepage.IsEnabled = false;
            }
            else
            {
                Application.Current.Host.Settings.EnableAutoZoom = false;
            }
        }

        private void buttonCopyResult_Click(object sender, RoutedEventArgs e)
        {
            this.ComputeUpdate();

            try
            {
                Clipboard.SetText(this.textBoxResult.Text);
            }
            catch (System.Security.SecurityException)
            {

            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                Application.Current.MainWindow.DragMove();
            }
        }

        private void rectangleMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        private void ellipseClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                Application.Current.MainWindow.Close();
            }
        }

        private void ComputeUpdate()
        {
            int length;
            bool isInt = int.TryParse(this.textBoxLength.Text, out length);
            if (isInt)
            {
                this.textBoxLength.Background = this.Resources["TextBoxBackground"] as LinearGradientBrush;
                IncorrectLengthValue = false;

                if ((this.passwordBoxKeyword.Password.Length > 0) && (this.textBoxWhatFor.Text.Length > 0))
                {
                    this.textBoxResult.Text = gen.Compute(this.passwordBoxKeyword.Password, this.textBoxWhatFor.Text, Convert.ToInt32(this.textBoxLength.Text), (bool)this.checkBoxUpperCase.IsChecked, (bool)this.checkBoxLowerCase.IsChecked, (bool)this.checkBoxNumbers.IsChecked, (bool)this.checkBoxSpecialCharacters.IsChecked);
                }
                else
                {
                    this.textBoxResult.Text = string.Empty;
                }
            }
            else
            {
                this.textBoxResult.Text = string.Empty;

                this.textBoxLength.Background = new SolidColorBrush(Color.FromArgb(255, 255, 58, 0));
                IncorrectLengthValue = true;
            }
        }

        private void checkBoxUpperCase_Click(object sender, RoutedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void checkBoxLowerCase_Click(object sender, RoutedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void checkBoxSpecialCharacters_Click(object sender, RoutedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void checkBoxNumbers_Click(object sender, RoutedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void passwordBoxKeyword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void textBoxWhatFor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void textBoxLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ComputeUpdate();
        }

        private void textBoxLength_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IncorrectLengthValue)
                this.textBoxLength.Text = "15";

            int length;
            bool isInt = int.TryParse(this.textBoxLength.Text, out length);
            if (isInt)
            {
                if (length < gen.MinimumLength)
                {
                    this.textBoxLength.Text = gen.MinimumLength.ToString();
                    this.ComputeUpdate();
                }
                else if (length > gen.MaximumLength)
                {
                    this.textBoxLength.Text = gen.MaximumLength.ToString();
                    this.ComputeUpdate();
                }
            }
        }
    }
}
