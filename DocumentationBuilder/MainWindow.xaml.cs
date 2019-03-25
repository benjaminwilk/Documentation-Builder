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

        DataIcons userDI;

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
            this.userDI = new DataIcons(ow.OptionsVerticalIcon.Text, ow.OptionsHorizontalIcon.Text, ow.OptionsCrossIcon.Text, Int32.Parse(ow.OptionsTypeWidthBox.Text), Int32.Parse(ow.OptionsMethodWidth.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, this.userDI);
            this.InputBox.Text = String.Empty;
            InputBox.AppendText(ds.GetHeaderTitle() + "\n\n");
            InputBox.AppendText(ds.GetConstructorSummaryHeader());
            InputBox.AppendText("\n");
           // for (int p = 0; p < ) {

//            }
            InputBox.AppendText(ds.GetMethodSummaryHeader());
            for (int r = 0; r < ds.GetTypeMethodLength(); r++) {
                if (!String.IsNullOrEmpty(ds.ReturnType(r)) || !String.IsNullOrEmpty(ds.ReturnMethodName(r))) { 
                    InputBox.AppendText("\n" + this.userDI.GetVertIcon() + ds.ReturnType(r) + "\t" + this.userDI.GetVertIcon() + ds.ReturnMethodName(r) + this.userDI.GetVertIcon());
                    InputBox.AppendText("\n" + this.userDI.GetVertIcon() + "                    " + this.userDI.GetVertIcon());
          //      if (!String.IsNullOrEmpty(ds.ReturnMethodDescription(r))) {
           //         InputBox.AppendText("MethodDescription here" + "\n");
           //     } else {
                    InputBox.AppendText("                           " + "\n");
                    InputBox.AppendText(this.userDI.GetHorizontalDivider());
                }
                
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }

}
