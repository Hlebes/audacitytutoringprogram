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
        RadioButton[] radiobuttons;
        CheckBox[] checkbuttons;
        int currentQuizId = 0;
        Quiz currentQuiz;

        public MainWindow()
        {
            InitializeComponent();
            maincontent = this.Content;

            SerializeQuestions();

            //Десериализация списка 
            List<Quiz> returnedQuestions = new List<Quiz>();
            using (var reader = new StreamReader("quiz.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(returnedQuestions.GetType(),
                    new XmlRootAttribute("Quiz"));
                returnedQuestions = (List<Quiz>)deserializer.Deserialize(reader);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButton[] quizRB = new RadioButton[] { (RadioButton)this.FindName("quizButton1"),
                                                       (RadioButton)this.FindName("quizButton2"),
                                                       (RadioButton)this.FindName("quizButton3"),
                                                       (RadioButton)this.FindName("quizButton4")};
            radiobuttons = quizRB;

            CheckBox[] quizCB = new CheckBox[] { (CheckBox)this.FindName("quizButton1"),
                                                 (CheckBox)this.FindName("quizButton2"),
                                                 (CheckBox)this.FindName("quizButton3"),
                                                 (CheckBox)this.FindName("quizButton4")};
            checkbuttons = quizCB;
        }

        public void SerializeQuestions()
        {
            string[] quiz1options = { "Halo Infinite", "Call of Duty: Vanguard", "Battlefield 2042", "Back4Blood" };
            string[] quiz1answers = { "Halo Infinite", "Call of Duty: Vanguard" };
            Quiz quiz1 = new Quiz(2, "check", "Какие беты не разочаровали в 2021?", quiz1options, quiz1answers);

            string[] quiz2options = { "1", "2", "0", "Бесконечность" };
            string[] quiz2answers = { "1" };
            Quiz quiz2 = new Quiz(1, "radio", "Сколько тут правильных ответов?", quiz2options, quiz2answers);

            List<Quiz> questions = new List<Quiz>();
            questions.Add(quiz2);
            questions.Add(quiz1);

            //Сериализация списка
            XmlSerializer serializer = new XmlSerializer(questions.GetType(), new XmlRootAttribute("Quiz"));
            StreamWriter writer = new StreamWriter("quiz.xml");
            serializer.Serialize(writer.BaseStream, questions);
            writer.Close();
        }

        public void UpdateQuiz()
        {
        }

        public void SwitchQuiz()
        {
            currentQuizId++;
            UpdateQuiz();

            if (currentQuiz.QuizType == "radio")
            {

                radiobuttons[0].Content = currentQuiz.Answers[0];
                radiobuttons[1].Content = currentQuiz.Answers[1];
                radiobuttons[2].Content = currentQuiz.Answers[2];
                radiobuttons[3].Content = currentQuiz.Answers[3];
            }
        }

        private void MaterialPick(object sender, RoutedEventArgs e)
        {
            int materialId;
            materialChoice = (sender as Button).Content.ToString();
            Int32.TryParse(materialChoice, out materialId);

            maincontent = this.Content;
            Material materialPage = new Material(maincontent, materialId);
            this.Content = materialPage.Content;
            
            switch (materialId)
            {
                case 1:
                    docPath = "document1.rtf";
                    break;
                case 2:
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
