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
using System.Windows.Shapes;
using TMS.logic;

namespace TMS.model
{
    /// <summary>
    /// Interaction logic for ShowQuestions.xaml
    /// </summary>
    public partial class ShowQuestions : Window
    {
        int TestId;
        private Grid selectedQuestion;
        public ShowQuestions(int TestId)
        {
            InitializeComponent();
            this.TestId = TestId;
            this.RefreshQuestions();
            this.RefreshAnswers();
        }

        private void RefreshQuestions()
        {
            List<data.Question> questions = QuestionDatabaseManagerSingleton.Instance.getAll();
            this.spListQuestions.Children.Clear();
            foreach (data.Question item in questions)
            {
                Grid grid = new Grid()
                {
                    Uid = item.id.ToString(),
                    MinHeight = 80,
                    Background = Brushes.White
                };
                ColumnDefinition gridCol1 = new ColumnDefinition()
                {
                    MinWidth = 150,
                    Width = new GridLength(200, GridUnitType.Star)
                };
                ColumnDefinition gridCol2 = new ColumnDefinition()
                {
                    Width = new GridLength(74, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(gridCol1);
                grid.ColumnDefinitions.Add(gridCol2);

                TextBlock bodyQuestion = new TextBlock()
                {
                    Text = item.body,
                    Margin = new Thickness(10, 10, 10, 0),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top
                };
                grid.Children.Add(bodyQuestion);
                grid.MouseDown += this.SelectedQusetion_MouseDown;


                Button edit = new Button()
                {
                    Content = "Edit",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 33,
                    Width = 75,
                    Margin = new Thickness(10, 5, 0, 0),
                    IsEnabled = false
                };
                Grid.SetColumn(edit, 1);
                edit.Click += this.btnEditQuestion_Click;

                grid.Children.Add(edit);


                Button delete = new Button()
                {
                    Content = "Delete",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 33,
                    Width = 75,
                    Margin = new Thickness(10, 42, 0, 0),
                    IsEnabled = false
                };
                Grid.SetColumn(delete, 1);
                delete.Click += this.btnDeleteQuestion_Click;

                grid.Children.Add(delete);

                Button addAnswer = new Button()
                {
                    Content = "Add answer",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 33,
                    Width = 75,
                    Margin = new Thickness(10, 80, 0, 5),
                    IsEnabled = false
                };
                Grid.SetColumn(addAnswer, 1);
                addAnswer.Click += this.btnAddAnswer_Click;

                grid.Children.Add(addAnswer);
                this.spListQuestions.Children.Add(grid);
            }
        }
        private void RefreshAnswers()
        {
            int qid;
            List<data.Answer> questions = new List<data.Answer>();
            try 
            {
                qid = Convert.ToInt32(this.selectedQuestion.Uid.ToString());
                questions = AnswerDatabaseManagerSingleton.Instance.getAll().FindAll(x => x.questionId.Equals(qid));
            }
            catch(Exception ex)
            {
            };

            this.spListAnswers.Children.Clear();
            foreach (data.Answer item in questions)
            {
                Grid grid = new Grid()
                {
                    Uid = item.id.ToString(),
                    MinHeight = 80,
                    Background = Brushes.White
                };
                ColumnDefinition gridCol1 = new ColumnDefinition()
                {
                    MinWidth = 150,
                    Width = new GridLength(200, GridUnitType.Star)
                };
                ColumnDefinition gridCol2 = new ColumnDefinition()
                {
                    Width = new GridLength(74, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(gridCol1);
                grid.ColumnDefinitions.Add(gridCol2);

                TextBlock bodyAnswer = new TextBlock()
                {
                    Text = item.body,
                    Margin = new Thickness(10, 10, 10, 0),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top
                };
                grid.Children.Add(bodyAnswer);

                Button edit = new Button()
                {
                    Content = "Edit",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 33,
                    Width = 75,
                    Margin = new Thickness(10, 5, 0, 0)
                };
                Grid.SetColumn(edit, 1);
                edit.Click += this.btnEditAnswer_Click;

                grid.Children.Add(edit);


                Button delete = new Button()
                {
                    Content = "Delete",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 33,
                    Width = 75,
                    Margin = new Thickness(10, 42, 0, 0)
                };
                Grid.SetColumn(delete, 1);
                delete.Click += this.btnDeleteAnswer_Click;

                grid.Children.Add(delete);

                
                this.spListAnswers.Children.Add(grid);
            }
        }

        private void btnDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            QuestionDatabaseManagerSingleton.Instance.delete(QuestionDatabaseManagerSingleton.Instance.get(id));
            if (((sender as Button).Parent as Grid).Equals(this.selectedQuestion))
            {
                this.selectedQuestion = null;
            }


            foreach (data.Answer item in AnswerDatabaseManagerSingleton.Instance.getAll().FindAll(x => x.questionId == id))
            {
                AnswerDatabaseManagerSingleton.Instance.delete(item);
            }


            this.RefreshQuestions();
            this.RefreshAnswers();
        }

        private void btnEditAnswer_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            data.Answer answer = AnswerDatabaseManagerSingleton.Instance.get(id);
            CreateAnswer CA = new CreateAnswer(answer.id, answer.body, answer.isCorrect, answer.questionId, answer.isDraft);
            CA.ShowDialog();
            this.RefreshAnswers();
        }

        private void btnDeleteAnswer_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            AnswerDatabaseManagerSingleton.Instance.delete(AnswerDatabaseManagerSingleton.Instance.get(id));

            this.RefreshAnswers();
        }

        private void btnAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            CreateAnswer CA = new CreateAnswer(id);
            CA.ShowDialog();
            this.RefreshAnswers();
        }
        private void btnEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            data.Question question = QuestionDatabaseManagerSingleton.Instance.get(id);
            CreateQuestion CQ = new CreateQuestion(question.id,question.body,question.testId,question.isFowAnswers,question.isDraft);
            CQ.ShowDialog();
            this.RefreshQuestions();
            this.selectedQuestion = null;
            this.RefreshAnswers();
        }
        private void btnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestion CQ = new CreateQuestion(TestId);
            CQ.ShowDialog();
            this.RefreshQuestions();
            this.selectedQuestion = null;
            this.RefreshAnswers();
        }

        private void SelectedQusetion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.selectedQuestion != null)
            {
                this.selectedQuestion.Background = Brushes.White;
                (this.selectedQuestion.Children[1] as Button).IsEnabled = false;
                (this.selectedQuestion.Children[2] as Button).IsEnabled = false;
                (this.selectedQuestion.Children[3] as Button).IsEnabled = false;
            }
            this.selectedQuestion = sender as Grid;
            this.selectedQuestion.Background = Brushes.LightSeaGreen;
            (this.selectedQuestion.Children[1] as Button).IsEnabled = true;
            (this.selectedQuestion.Children[2] as Button).IsEnabled = true;
            (this.selectedQuestion.Children[3] as Button).IsEnabled = true;

            this.RefreshAnswers();
        }
    }
}
