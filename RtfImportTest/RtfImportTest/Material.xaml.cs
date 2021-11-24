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
using System.Windows.Documents;
using System.IO;


namespace RtfImportTest
{
    /// <summary>
    /// Логика взаимодействия для Material.xaml
    /// </summary>
    public partial class Material : Page
    {
        object content;

        public Material(object maincontent)
        {
            InitializeComponent();
            content = maincontent;
        }

        private void materialRichTextBox_Initialized(object sender, EventArgs e)
        {
        }

        public void backButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = content;
        }
    }
}
