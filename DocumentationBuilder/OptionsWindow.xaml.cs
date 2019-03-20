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
using System.Windows.Shapes;

namespace DocumentationBuilder {
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window {
        public OptionsWindow() {
            InitializeComponent();
        }

        private void OptionsCloseButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void OptionsPrintToFileRadio_Checked(object sender, RoutedEventArgs e) {
          //  OptionsFileLocation.IsEnabled = true;
        }

        private void OptionsPrintToScreenRadio_Checked(object sender, RoutedEventArgs e) {
            //OptionsFileLocation.IsEnabled = false;
        }

        private void OptionsTypeWidthBox_TextChanged(object sender, TextChangedEventArgs e) {
            if(OptionsTypeWidthBox.Text == "") {
                OptionsTypeWidthBox.Text = "20";
            }
        }

        private void OptionsMethodWidth_TextChanged(object sender, TextChangedEventArgs e) {
            if(OptionsMethodWidth.Text == "") {
                OptionsMethodWidth.Text = "60";
            }
        }

        private void OptionsVerticalIcon_TextChanged(object sender, TextChangedEventArgs e) {
            if(OptionsVerticalIcon.Text == "") {
                OptionsVerticalIcon.Text = "|";
            }
        }

        private void OptionsVerticalIcon_GotFocus(object sender, RoutedEventArgs e) {
           
        }

        private void OptionFilePathBrowseButton_Click(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "All Types(*.*)|*.*|Normal Text File (*.txt)|*.txt";
        //    if (result == true) {
                // Open document 
                //string filename = dlg.FileName;
                //textBox1.Text = filename;
        //    }
        }
    }
}
