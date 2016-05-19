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

        private void SignUP(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.FName.Text + " " 
                + this.LName.Text + " " 
                + this.SName.Text + " " 
                + this.Email.Text + " " 
                + this.Password.Password + " " 
                + this.RepeatPassword.Password);
        }
    }
}
