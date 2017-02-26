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

namespace Kasse
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_num_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String text = button.Content.ToString();

            TextBox textbox = textBox;

            textBox.Text = textBox.Text + text;
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TextBox text = textBox;
            textBox.Text = "";
        }
    }
    
}
