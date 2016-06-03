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
    /// Interaction logic for CreateQuestion.xaml
    /// </summary>
    public partial class CreateQuestion : Window
    {
        private int idTest, QuestionId;
        bool isEdit = false;
        public CreateQuestion(int idTest)
        {
            InitializeComponent();
            this.idTest = idTest;
        }
        public CreateQuestion(
                int id,
                string body,
                int testId,
                bool isFowAnswers,
                bool isDraft
            )
        {
            InitializeComponent();
            isEdit = true;
            this.txtbBody.Text = body;
            this.idTest = testId;
            this.QuestionId = id;
            this.checkbIsFowAnswer.IsChecked = isFowAnswers;
            this.checkbIsDraft.IsChecked = isDraft;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtbBody.Text))
            {
                if (this.isEdit)
                {
                   QuestionDatabaseManagerSingleton.Instance.update(new data.Question
                        (
                            this.QuestionId,
                            this.txtbBody.Text,
                            this.idTest,
                            this.checkbIsFowAnswer.IsChecked.Value,
                            this.checkbIsDraft.IsChecked.Value
                        )
                    );
                    MessageBox.Show("Save complite.");
                    this.Close();
                    return;
                }
                else
                {
                    QuestionDatabaseManagerSingleton.Instance.add(new data.Question
                        (
                            -1,
                            this.txtbBody.Text,
                            this.idTest,
                            this.checkbIsFowAnswer.IsChecked.Value,
                            this.checkbIsDraft.IsChecked.Value
                        )
                        );
                }
                MessageBox.Show("Save complite.");
                this.txtbBody.Clear();
                this.checkbIsFowAnswer.IsChecked = false;
                this.checkbIsDraft.IsChecked = false;
            }
            else
                MessageBox.Show("Field \"Body question\" bіt be filled!");
        }
    }
}