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

namespace FrameFinder
{
    public class MethodsAndFunctions: MainWindow
    {
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

        public void Clear()
        {
            txtSalesOrder.Text = "";
            txtWorkOrder.Text = "";
            txtFrame.Text = "";
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
        }
    }
}
