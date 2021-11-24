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
using System.Xml.Serialization;

namespace RtfImportTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string materialChoice = "";
        string docPath = "";
        object maincontent;


        public MainWindow()
        {
            InitializeComponent();
            maincontent = this.Content;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void SerializeQuestions()
        {
            string[] quiz1options = { "Halo Infinite", "Call of Duty: Vanguard", "Battlefield 2042" };
            string[] quiz1answers = { "Halo Infinite", "Call of Duty: Vanguard" };
            Quiz quiz1 = new Quiz(2, "check", "Какие беты не разочаровали в 2021?", quiz1options, quiz1answers);

            string[] quiz2options = { "1", "2", "0" };
            string[] quiz2answers = { "1" };
            Quiz quiz2 = new Quiz(1, "radio", "Сколько тут правильных ответов?", quiz2options, quiz2answers);

            List<Quiz> questions = new List<Quiz>();
            questions.Add(quiz1);
            questions.Add(quiz2);

            //Сериализация списка
            XmlSerializer serializer = new XmlSerializer(questions.GetType(), new XmlRootAttribute("Quiz"));
            StreamWriter writer = new StreamWriter("quiz.xml");
            serializer.Serialize(writer.BaseStream, questions);
            writer.Close();
        }

        public void DeserializeQuestions()
        {
            //Десериализация списка 
            List<Quiz> returnedQuestions = new List<Quiz>();
            using (var reader = new StreamReader("quiz.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(returnedQuestions.GetType(),
                    new XmlRootAttribute("Quiz"));
                returnedQuestions = (List<Quiz>)deserializer.Deserialize(reader);
            }
        }

        private void MaterialPick(object sender, RoutedEventArgs e)
        {
            maincontent = this.Content;
            Material materialPage = new Material(maincontent);
            this.Content = materialPage.Content;
            materialChoice = (sender as Button).Content.ToString();
            switch (materialChoice)
            {
                case "1":
                    docPath = "document1.rtf";
                    break;
                case "2":
                    docPath = "document2.rtf";
                    break;
                default:
                    docPath = "errordocument.rtf";
                    break;
            }

            FlowDocument doc = new FlowDocument();
            FileStream fs = File.Open(docPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            var content = new TextRange(doc.ContentStart, doc.ContentEnd);
            if (content.CanLoad(DataFormats.Rtf))
            {
                content.Load(fs, DataFormats.Rtf);
            }

            materialPage.documentViewer.Document = doc;
        }
    }
}
