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

     /*   private void MenuOptionButton_Click(object sender, RoutedEventArgs e) {
            OptionsWindow ow = new OptionsWindow();
            ow.Show();
        }*/

        private void MenuCloseButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e) {
            SizeToContent = SizeToContent.Height + 50;
        }

        private void ComputationButton_Click(object sender, RoutedEventArgs e) {
            FormatData fd = new FormatData();
       //     OptionsWindow ow = new OptionsWindow();
            TextFramework tf = new TextFramework(VerticalIconInput.Text, HorizontalIconInput.Text, CrossIconInput.Text, Int32.Parse(TypeWidthInput.Text), Int32.Parse(MethodWidthInput.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, fd);
            this.InputBox.Text = String.Empty;
            InputBox.AppendText(fd.ReturnClassName());
            InputBox.AppendText(tf.GetConstructorSummaryHeader() + "\n");
            for (int p = 0; p < fd.ConstructorCount(); p++) {
                InputBox.AppendText(tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetConstructor(p), 59) + tf.GetVertIcon() + "\n");
                InputBox.AppendText(tf.GetConstructorDivider());
            }
            InputBox.AppendText("\n\n" + tf.GetMethodSummaryHeader());
            for (int r = 0; r < fd.GetMethodCount(); r++) {
                InputBox.AppendText("\n" + tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetType(r), 19) + tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetMethod(r), 59) + tf.GetVertIcon() + "\n");
                InputBox.AppendText(tf.GetHorizontalDivider());
            //    }
                
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

    }

}
