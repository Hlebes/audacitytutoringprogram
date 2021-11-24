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
        int materialId;

        public Material(object maincontent, int materialId)
        {
            InitializeComponent();
            content = maincontent;
            this.materialId = materialId;
        }

        private void materialRichTextBox_Initialized(object sender, EventArgs e)
        {
        }

        public void backButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = content;
        }

        private void quizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizPage quizpage = new QuizPage(materialId);
            content = quizpage.Content;
            Application.Current.MainWindow.Content = content;
        }
    }
}
