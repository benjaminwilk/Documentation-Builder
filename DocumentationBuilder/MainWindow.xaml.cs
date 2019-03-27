using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void MenuCloseButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e) {
            SizeToContent = SizeToContent.Height + 50;
        }

        private void ComputationButton_Click(object sender, RoutedEventArgs e) {
            string computedText = formatOutputText();
            if (PrintToScreenRadio.IsChecked == true) {
                this.InputBox.Text = String.Empty;
                InputBox.AppendText(computedText);
            } else {
                if (PrintToFileRadio.IsChecked == true && FilePath.Text == "Filepath" || FilePath.Text == "") {
                    System.Windows.MessageBox.Show("Print to file chosen, but no file path was given", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else {
                    File.WriteAllText(FilePath.Text, computedText);
                }
            }
        }

        private String formatOutputText() {
            FormatData fd = new FormatData();
            TextFramework tf = new TextFramework(VerticalIconInput.Text, HorizontalIconInput.Text, CrossIconInput.Text, Int32.Parse(TypeWidthInput.Text), Int32.Parse(MethodWidthInput.Text));
            DocumentStripper ds = new DocumentStripper(InputBox.Text, fd);
            StringBuilder displayText = new StringBuilder();
            //this.InputBox.Text = String.Empty;
            //InputBox.AppendText(fd.ReturnClassName());
            InputBox.AppendText(tf.GetConstructorSummaryHeader() + "\n");
            displayText.Append(fd.ReturnClassName());
            displayText.Append(tf.GetConstructorSummaryHeader() + "\n");
            for (int p = 0; p < fd.ConstructorCount(); p++) {
                displayText.Append(tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetConstructor(p), 59) + tf.GetVertIcon() + "\n");
                displayText.Append(tf.GetConstructorDivider());
                //InputBox.AppendText(tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetConstructor(p), 59) + tf.GetVertIcon() + "\n");
                //InputBox.AppendText(tf.GetConstructorDivider());
            }
            //InputBox.AppendText("\n\n" + tf.GetMethodSummaryHeader());
            displayText.Append("\n\n" + tf.GetMethodSummaryHeader());
            for (int r = 0; r < fd.GetMethodCount(); r++) {
                displayText.Append("\n" + tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetType(r), 19) + tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetMethod(r), 59) + tf.GetVertIcon() + "\n");
                displayText.Append(tf.GetHorizontalDivider());
                //InputBox.AppendText("\n" + tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetType(r), 19) + tf.GetVertIcon() + TextFramework.LeftAlignmentTextWithPadding(fd.GetMethod(r), 59) + tf.GetVertIcon() + "\n");
                //InputBox.AppendText(tf.GetHorizontalDivider());
            }
            return displayText.ToString();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }


        private void FilePathBrowseButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog choofdlog = new OpenFileDialog();
            FilePath.Text = String.Empty;
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (!choofdlog.ShowDialog() == DialogResult.HasValue) {
                FilePath.AppendText(choofdlog.FileName);
            }
        }

        private void FilePath_GotFocus(object sender, RoutedEventArgs e) {
            OpenFileDialog choofdlog = new OpenFileDialog();
            FilePath.Text = String.Empty;
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (!choofdlog.ShowDialog() == DialogResult.HasValue) {
                FilePath.AppendText(choofdlog.FileName);
            }
        }

        private void WidthResetButton_Click(object sender, RoutedEventArgs e) {
            VerticalIconInput.Text = String.Empty;
            VerticalIconInput.Text = "" + TextFramework.GetOriginalVertIcon();
            HorizontalIconInput.Text = String.Empty;
            HorizontalIconInput.Text = "" + TextFramework.GetOriginalHoriIcon();
            CrossIconInput.Text = String.Empty;
            CrossIconInput.Text = "" + TextFramework.GetOriginalCrosIcon();
        }

        private void IconsResetButton_Click(object sender, RoutedEventArgs e) {
            TypeWidthInput.Text = String.Empty;
            TypeWidthInput.Text = "" + TextFramework.GetOriginalTypeWidth();
            MethodWidthInput.Text = String.Empty;
            MethodWidthInput.Text = "" + TextFramework.GetOriginalMethodWidth();
        }
    }
}
