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
    /// Interaction logic for CreateAnswer.xaml
    /// </summary>
    public partial class CreateAnswer : Window
    {
        int QuestionId, AnswerId;
        bool isEdit = false;
        public CreateAnswer(int QuestionId)
        {
            InitializeComponent();
            this.QuestionId = QuestionId;
        }
        /*
         public CreateAnswer(data.Answer answer)
         Error	Inconsistent accessibility: parameter type 'TMS.data.Answer' is less accessible than method 'TMS.model.CreateAnswer.CreateAnswer(TMS.data.Answer)'
         */
        public CreateAnswer(
            int id,
                string body,
                bool isTrue,
                int questionId,
                bool isDraft
            )
        {
            InitializeComponent();
            isEdit = true;
            this.AnswerId = id;
            this.QuestionId = questionId;
            this.txtbBody.Text = body;
            this.checkbIsCorrect.IsChecked = isTrue;
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
                    AnswerDatabaseManagerSingleton.Instance.update(new data.Answer(
                                this.AnswerId,
                                this.txtbBody.Text,
                                this.checkbIsCorrect.IsChecked.Value,
                                this.QuestionId,
                                this.checkbIsDraft.IsChecked.Value
                            )
                    );

                    MessageBox.Show("Save complite.");
                    this.Close();
                    return;
                }
                else
                {
                    AnswerDatabaseManagerSingleton.Instance.add(new data.Answer(
                            -1,
                            this.txtbBody.Text,
                            this.checkbIsCorrect.IsChecked.Value,
                            this.QuestionId,
                            this.checkbIsDraft.IsChecked.Value
                        )

                    );
                }
                MessageBox.Show("Save complite.");
                this.txtbBody.Clear();
                this.checkbIsCorrect.IsChecked = false;
                this.checkbIsDraft.IsChecked = false;
            }
            else
                MessageBox.Show("Field \"Body answer\" bіt be filled!");
        }
    }
}
