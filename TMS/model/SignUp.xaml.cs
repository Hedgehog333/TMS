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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();

            List<data.Group> dataList = GroupDatabaseManagerSingleton.Instance.getAll();
            this.cboxGroup.ItemsSource = dataList;
            this.cboxGroup.DisplayMemberPath = "Name";
            this.cboxGroup.SelectedValuePath = "id";
        }

        private void btnSignUpClick(object sender, RoutedEventArgs e)
        {
            bool fN = false, lN = fN, em = fN, pas = fN, rpas = fN, gbN = fN;

            if (String.IsNullOrWhiteSpace(this.txtbFName.Text))
                this.txtbFName.BorderBrush = Brushes.Red;
            else
            {
                this.txtbFName.BorderBrush = Brushes.Green;
                fN = true;
            }

            if (String.IsNullOrWhiteSpace(this.txtbLName.Text))
                this.txtbLName.BorderBrush = Brushes.Red;
            else
            {
                this.txtbLName.BorderBrush = Brushes.Green;
                lN = true;
            }

            if (String.IsNullOrWhiteSpace(this.txtbEmail.Text))
                this.txtbEmail.BorderBrush = Brushes.Red;
            else
            {
                this.txtbEmail.BorderBrush = Brushes.Green;
                em = true;
            }

            if (String.IsNullOrWhiteSpace(this.pasPassword.Password))
                this.pasPassword.BorderBrush = Brushes.Red;
            else
            {
                this.pasPassword.BorderBrush = Brushes.Green;
                pas = true;
            }

            if (this.cboxGroup.SelectedValue == null)
                this.cboxGroup.BorderBrush = Brushes.Red;
            else
            {
                this.cboxGroup.BorderBrush = Brushes.Green;
                gbN = true;
            }

            if (String.IsNullOrWhiteSpace(this.pasRepeatPassword.Password) || !this.pasPassword.Password.Equals(this.pasRepeatPassword.Password))
                this.pasRepeatPassword.BorderBrush = Brushes.Red;
            else
            {
                this.pasRepeatPassword.BorderBrush = Brushes.Green;
                rpas = true;
            }

            if (fN && lN && em && pas && rpas && gbN)
            {
                UserDatabaseManagerSingleton.Instance.add(
                    new data.User(
                        -1,
                        this.txtbFName.Text,
                        this.txtbLName.Text,
                        this.txtbSName.Text, 
                        this.txtbEmail.Text,
                        this.pasPassword.Password, 
                        (int)this.cboxGroup.SelectedValue,
                        data.ERoles.student, 
                        DateTime.Now
                    )
                );
                MessageBox.Show("Save complite.");
                this.Owner.Show();
                this.Close();
            }
        }

        private void btnGoBackClick(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
