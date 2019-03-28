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
            //InputBox.AppendText(tf.GetConstructorSummaryHeader() + Environment.NewLine);
            displayText.Append(fd.ReturnClassName());
            displayText.Append(tf.GetConstructorSummaryHeader() + Environment.NewLine);
            for (int p = 0; p < fd.ConstructorCount(); p++) {
                displayText.Append(tf.AssembleConstructorRow(fd.GetConstructor(p)));
            }
            displayText.Append(Environment.NewLine + Environment.NewLine + tf.GetMethodSummaryHeader());
            for (int r = 0; r < fd.GetMethodCount(); r++) {
                if (fd.GetMethodComment(r).Equals("")) {
                    displayText.Append(tf.AssembleFunctionRow(fd.GetType(r), fd.GetMethod(r)));
                } else {
                    displayText.Append(tf.AssembleFunctionRow(fd.GetType(r), fd.GetMethod(r), fd.GetMethodComment(r)));
                }
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

        private void VerticalIconInput_Initialized(object sender, EventArgs e) {
            VerticalIconInput.Text = "" + TextFramework.GetOriginalVertIcon();
        }

        private void HorizontalIconInput_Initialized(object sender, EventArgs e) {
            HorizontalIconInput.Text = "" + TextFramework.GetOriginalHoriIcon();
        }

        private void CrossIconInput_Initialized(object sender, EventArgs e) {
            CrossIconInput.Text = "" + TextFramework.GetOriginalCrosIcon();
        }

        private void TypeWidthInput_Initialized(object sender, EventArgs e) {
            TypeWidthInput.Text = "" + TextFramework.GetOriginalTypeWidth();
        }

        private void MethodWidthInput_Initialized(object sender, EventArgs e) {
            MethodWidthInput.Text = "" + TextFramework.GetOriginalMethodWidth();
        }

        private void VerticalIconInput_LostFocus(object sender, RoutedEventArgs e) {
            if (VerticalIconInput.Text == "") {
                VerticalIconInput.Text = "" + TextFramework.GetOriginalVertIcon();
            }
        }

        private void VerticalIconInput_GotFocus(object sender, RoutedEventArgs e) {
            VerticalIconInput.Text = String.Empty;
        }

        private void HorizontalIconInput_GotFocus(object sender, RoutedEventArgs e) {
            HorizontalIconInput.Text = String.Empty;
        }

        private void CrossIconInput_GotFocus(object sender, RoutedEventArgs e) {
            CrossIconInput.Text = String.Empty;
        }

        private void HorizontalIconInput_LostFocus(object sender, RoutedEventArgs e) {
            if (HorizontalIconInput.Text == "") {
                HorizontalIconInput.Text = "" + TextFramework.GetOriginalHoriIcon();
            }
        }

        private void CrossIconInput_LostFocus(object sender, RoutedEventArgs e) {
            if (CrossIconInput.Text == "") {
                CrossIconInput.Text = "" + TextFramework.GetOriginalCrosIcon();
            }
        }

        private void TypeWidthInput_GotFocus(object sender, RoutedEventArgs e) {
            TypeWidthInput.Text = String.Empty;
        }

        private void MethodWidthInput_GotFocus(object sender, RoutedEventArgs e) {
            MethodWidthInput.Text = String.Empty;
        }

        private void TypeWidthInput_LostFocus(object sender, RoutedEventArgs e) {
            if (TypeWidthInput.Text == "") {
                TypeWidthInput.Text = "" + TextFramework.GetOriginalTypeWidth();
            }
        }

        private void MethodWidthInput_LostFocus(object sender, RoutedEventArgs e) {
            if (MethodWidthInput.Text == "") {
                MethodWidthInput.Text = "" + TextFramework.GetOriginalMethodWidth();
            }
        }

        private void InputBox_GotFocus(object sender, RoutedEventArgs e) {
            InputBox.Text = String.Empty;
        }

        private void InputBox_LostFocus(object sender, RoutedEventArgs e) {
            if (InputBox.Text == "") {
                InputBox.Text = "Paste in Functions you would like formatted";
            }
        }
    }
}
