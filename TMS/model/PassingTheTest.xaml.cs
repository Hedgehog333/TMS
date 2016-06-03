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
    /// Interaction logic for PassingTheTest.xaml
    /// </summary>
    public partial class PassingTheTest : Window
    {
        private data.Test test;
        private int currentQuestionIndex = 0;
        Dictionary<int, HashSet<int>> StoryChechbox = new Dictionary<int, HashSet<int>>();
        Dictionary<int, int> StoryRadiobutton = new Dictionary<int, int>();
        public PassingTheTest(int TestId)
        {
            InitializeComponent();
            this.test = TestDatabaseManagerSingleton.Instance.get(TestId);
            List<data.Question> q = QuestionDatabaseManagerSingleton.Instance.getAll();
            this.test.questions = q.FindAll(x => x.testId == TestId && x.isDraft == false);
            foreach (data.Question item in this.test.questions)
            {
                item.answers = AnswerDatabaseManagerSingleton.Instance.getAll().FindAll(x => x.questionId == item.id && x.isDraft == false);
            }
            this.lblTotalQuestion.Content = this.test.questions.Count;
            ShowQuestion(this.currentQuestionIndex);
        }
        private void ShowQuestion(int QuestionId)
        {
            this.txtbQuestionBody.Text = this.test.questions[QuestionId].body;
            ShowAnswers(this.test.questions[QuestionId]);
            Pagination(QuestionId);
        }

        private void ShowAnswers(data.Question question)
        {
            this.spQuestionList.Children.Clear();
            foreach (data.Answer item in question.answers)
            {
                Grid grid = new Grid()
                {
                    Uid = question.id.ToString()
                };
                ColumnDefinition gridCol1 = new ColumnDefinition()
                {
                    Width = new GridLength(70)
                };
                ColumnDefinition gridCol2 = new ColumnDefinition()
                {
                    Width = new GridLength(515, GridUnitType.Star)
                };
                grid.ColumnDefinitions.Add(gridCol1);
                grid.ColumnDefinitions.Add(gridCol2);

                TextBlock bodyAnswer = new TextBlock()
                {
                    Text = item.body,
                    Margin = new Thickness(10, 10, 10, 10),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top
                };
                Grid.SetColumn(bodyAnswer, 1);
                grid.Children.Add(bodyAnswer);

                if(question.isFowAnswers)
                {
                    CheckBox check = new CheckBox()
                    {
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        RenderTransformOrigin = new Point(0.462, 0.385),
                        Uid = item.id.ToString()
                    };
                    check.Checked += CBCheckedChanged;
                    check.Unchecked += check_Unchecked;
                    Grid.SetColumn(check, 0);
                    grid.Children.Add(check);
                }
                else
                {
                    RadioButton radioB = new RadioButton()
                    {
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        RenderTransformOrigin = new Point(0.462, 0.385),
                        GroupName = "group",
                        Uid = item.id.ToString()
                    };
                    radioB.Checked += RBCheckedChanged;
                    Grid.SetColumn(radioB, 0);
                    grid.Children.Add(radioB);
                }
                this.spQuestionList.Children.Add(grid);
            }
        }

        private void Pagination(int QuestionId)
        {
            this.lblCurrentQuestion.Content = QuestionId + 1;
            if (QuestionId == 0)
            {
                this.btnPreviousQuestion.IsEnabled = false;
                this.btnFirstQuestion.IsEnabled = false;
            }
            else
            {
                this.btnPreviousQuestion.IsEnabled = true;
                this.btnFirstQuestion.IsEnabled = true;
            }
            if (QuestionId == this.test.questions.Count - 1)
            {
                this.btnLasrQuestion.IsEnabled = false;
                this.btnNaxtQuestion.IsEnabled = false;
            }
            else
            {
                this.btnLasrQuestion.IsEnabled = true;
                this.btnNaxtQuestion.IsEnabled = true;
            }
        }

        private void btnFirstQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.currentQuestionIndex = 0;
            this.ShowQuestion(currentQuestionIndex);
        }

        private void btnPreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.ShowQuestion(--currentQuestionIndex);
        }

        private void btnNaxtQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.ShowQuestion(++currentQuestionIndex);
        }

        private void btnLasrQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.currentQuestionIndex = this.test.questions.Count-1;
            this.ShowQuestion(currentQuestionIndex);
        }

        private void RBCheckedChanged(object sender, EventArgs e)
        {
            int AnswerId = Int32.Parse((sender as RadioButton).Uid.ToString());
            int QuestionId = Int32.Parse(((sender as RadioButton).Parent as Grid).Uid.ToString());
            this.StoryRadiobutton[QuestionId] = AnswerId;
        }
        private void CBCheckedChanged(object sender, EventArgs e)
        {
            int AnswerId = Int32.Parse((sender as CheckBox).Uid.ToString());
            int QuestionId = Int32.Parse(((sender as CheckBox).Parent as Grid).Uid.ToString());

            try
            {
                this.StoryChechbox.Add(QuestionId, new HashSet<int>());
                this.StoryChechbox[QuestionId].Add(AnswerId);
            }
            catch (ArgumentException ex)
            {
                this.StoryChechbox[QuestionId].Add(AnswerId);
            }
        }

        private void check_Unchecked(object sender, RoutedEventArgs e)
        {
            int AnswerId = Int32.Parse((sender as CheckBox).Uid.ToString());
            int QuestionId = Int32.Parse(((sender as CheckBox).Parent as Grid).Uid.ToString());

            this.StoryChechbox[QuestionId].Remove(AnswerId);
        }

        private void FinishTest_Click(object sender, RoutedEventArgs e)
        {
            int correct = 0;
            int total = 0;
            foreach (var item in this.StoryChechbox.Keys)
            {
                foreach (var value in this.StoryChechbox[item])
                {
                    if (this.test.questions.Find(x => x.id == item).answers.Find(a => a.id == value && a.isCorrect == true) != null)
                        correct++;
                }
            }
            foreach (var item in this.StoryRadiobutton)
            {
                if(this.test.questions.Find(x => x.id == item.Key).answers.Find(a => a.id == item.Value && a.isCorrect == true) != null)
                    correct++;
            }
            foreach (var item in this.test.questions)
            {
                total += item.answers.FindAll(x => x.isCorrect == true).Count;
            }
            ResultDatabaseManagerSingleton.Instance.add(new data.Result(-1, this.test.id, CurrentUserSingleton.Instance.User.id, correct, total, DateTime.Now));
            this.Close();
            MessageBox.Show("You score " + correct + " out of " + total);
        }
    }
}
