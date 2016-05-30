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
using TMS.data;
using TMS.dao;

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
        }

        private void btnSignUpClick(object sender, RoutedEventArgs e)
        {
            bool fN = false, lN = fN, em = fN, pas = fN, rpas = fN;

            if (String.IsNullOrWhiteSpace(this.FName.Text))
                this.FName.BorderBrush = Brushes.Red;
            else
            {
                this.FName.BorderBrush = Brushes.Green;
                fN = true;
            }

            if (String.IsNullOrWhiteSpace(this.LName.Text))
                this.LName.BorderBrush = Brushes.Red;
            else
            {
                this.LName.BorderBrush = Brushes.Green;
                lN = true;
            }

            if (String.IsNullOrWhiteSpace(this.Email.Text))
                this.Email.BorderBrush = Brushes.Red;
            else
            {
                this.Email.BorderBrush = Brushes.Green;
                em = true;
            }

            if (String.IsNullOrWhiteSpace(this.Password.Password))
                this.Password.BorderBrush = Brushes.Red;
            else
            {
                this.Password.BorderBrush = Brushes.Green;
                pas = true;
            }


            if (String.IsNullOrWhiteSpace(this.RepeatPassword.Password) || !this.Password.Password.Equals(this.RepeatPassword.Password))
                this.RepeatPassword.BorderBrush = Brushes.Red;
            else
            {
                this.RepeatPassword.BorderBrush = Brushes.Green;
                rpas = true;
            }

            if (fN && lN && em && pas && rpas)
            {
                UserDatabaseManagerSingleton.Instance.add(
                    new User(
                        -1, 
                        this.FName.Text, 
                        this.LName.Text, 
                        this.SName.Text, 
                        this.Email.Text, 
                        this.Password.Password, 
                        ERoles.student, 
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
