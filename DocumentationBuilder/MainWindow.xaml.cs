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

namespace DocumentationBuilder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void MenuOptionButton_Click(object sender, RoutedEventArgs e) {
            OptionsWindow ow = new OptionsWindow();
            ow.Show();
        }

        private void MenuCloseButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e) {
            SizeToContent = SizeToContent.Height + 50;
        }

        private void ComputationButton_Click(object sender, RoutedEventArgs e) {
            OptionsWindow ow = new OptionsWindow();
            DocumentStripper ds = new DocumentStripper(InputBox.Text, ow.OptionsVerticalIcon.Text, ow.OptionsHorizontalIcon.Text, ow.OptionsCrossIcon.Text, ow.OptionsTypeWidthBox.Text, ow.OptionsMethodWidth.Text);
            this.InputBox.Text = String.Empty;
            InputBox.AppendText(ds.DisplayText());
        }
    }

}
