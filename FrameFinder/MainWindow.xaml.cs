using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Deployment.Application;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace FrameFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Disbale MaxSizeBox
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        #region MaxSize disbale
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;

        public MainWindow()
        {
            InitializeComponent();
            try { lblVersion.Content = $"Version: {ApplicationDeployment.CurrentDeployment.CurrentVersion}"; }
            catch { lblVersion.Content = "N/A"; }
            this.SourceInitialized += Window_SourceInitialized;
            //listBox.BorderThickness = b
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper((Window)sender).Handle;
            var value = GetWindowLong(hwnd, GWL_STYLE);
            SetWindowLong(hwnd, GWL_STYLE, (int)(value & ~WS_MAXIMIZEBOX));
        }
        #endregion

        
        /// <summary>
        /// Move focus to the next control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region MoveFocus Next Control
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                uie.MoveFocus(
                new TraversalRequest(
                FocusNavigationDirection.Next));
            }
        }
        #endregion

        /// <summary>
        /// Read the TXT file and seek for the Frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Search Frame
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileContent = @"C:\Users\Emir\Documents\Development\Visual Studio\Test frame finder\" + txtSalesOrder.Text + "-" + txtWorkOrder.Text + "-Log.txt";
                FileInfo fc = new FileInfo(fileContent);
                if (fc.Exists)
                {
                    string[] lineas = File.ReadAllLines(fileContent);

                    bool exito = Array.Exists(lineas, p => p.Contains(txtFrame.Text));

                    if (exito == true)
                    {
                        lblResult.Content = txtFrame.Text + " was find in " + txtSalesOrder.Text + "-" + txtWorkOrder.Text + "-Log.txt!";
                        ChangeColor("good");

                        listBox.ItemsSource = lineas;

                        //foreach (string element in lineas)
                        //{
                        //    ListBoxItem item = new ListBoxItem();
                        //    item.Content = element;
                        //    listBox.Items.Add(item);
                        //}

                        Clear();
                    }
                    else
                    {
                        lblResult.Content = txtFrame.Text + " couldn't find in " + txtSalesOrder.Text + "-" + txtWorkOrder.Text + "-Log.txt!";
                        ChangeColor("bad");
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show(txtSalesOrder.Text + "-" + txtWorkOrder.Text + "-Log.txt does not exits!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Methods to change color based in the TXT result
        /// </summary>
        /// <param name="colorname"></param>
        /// <returns></returns>
        #region Change color
        public string ChangeColor(string colorname)
        {
            if (colorname == "good")
            {
                mainGrid.Background = Brushes.Green;
                ResultColor();
            }
            else if (colorname == "bad")
            {
                mainGrid.Background = Brushes.Red;
                ResultColor();
            }
            return colorname;
        }

        public void NormalColor()
        {
            mainGrid.Background = Brushes.WhiteSmoke;
            lblWorkOrder.Foreground = Brushes.Black;
            lblSalesOrder.Foreground = Brushes.Black;
            lblFrame.Foreground = Brushes.Black;
            lblVersion.Foreground = Brushes.Black;
            lblResult.Foreground = Brushes.Black;
        }

        public void ResultColor()
        {
            lblWorkOrder.Foreground = Brushes.White;
            lblSalesOrder.Foreground = Brushes.White;
            lblFrame.Foreground = Brushes.White;
            lblVersion.Foreground = Brushes.White;
            lblResult.Foreground = Brushes.White;
            listBox.Background = Brushes.White;
        }
        #endregion

        /// <summary>
        /// Methods to clean textbox
        /// </summary>
        #region Clear
        public void Clear()
        {
            txtSalesOrder.Text = "";
            txtWorkOrder.Text = "";
            txtFrame.Text = "";
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listBox.ItemsSource = "";
            lblResult.Content = "";
            Clear();
            NormalColor();
        }

        #endregion

    }
}