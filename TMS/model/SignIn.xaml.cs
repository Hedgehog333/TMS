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

using TMS.logic;
using TMS.data;
namespace TMS.model
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void btnSignUpClick(object sender, RoutedEventArgs e)
        {
            SignUp subWindow = new SignUp();
            subWindow.Owner = this;
            subWindow.Show();
            this.Hide();
        }

        private void btnSignInClick(object sender, RoutedEventArgs e)
        {
            if (
                !String.IsNullOrWhiteSpace(this.txtbEmail.Text) &&
                !String.IsNullOrWhiteSpace(this.pasPassword.Password)
                )
            {
                this.lblErrorMessage.Content = "";

                //necessary for the authorization, writing data in CurrentUserSingleton
                LoginMethod log = new LoginMethod(this.txtbEmail.Text);

                if (CurrentUserSingleton.Instance.User != null)
                {
                    if (CurrentUserSingleton.Instance.User.password.Equals(this.pasPassword.Password))
                    {
                        PersonalArea subWindow = new PersonalArea();
                        this.txtbEmail.BorderBrush = Brushes.Gray;
                        this.pasPassword.BorderBrush = this.txtbEmail.BorderBrush;
                        subWindow.Owner = this;
                        subWindow.Show();
                        this.Hide();
                    }
                    else
                    {
                        this.lblErrorMessage.Content = "Incorrectly password!";
                        this.txtbEmail.BorderBrush = Brushes.Green;
                        this.pasPassword.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    this.lblErrorMessage.Content = "User not found!";
                    this.txtbEmail.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                this.lblErrorMessage.Content = "Incorrectly filled fields!";
                this.txtbEmail.BorderBrush = Brushes.Red;
                this.pasPassword.BorderBrush = Brushes.Red;
            }
        }
    }
}
