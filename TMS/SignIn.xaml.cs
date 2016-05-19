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

namespace TMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();

            //db.XMLUserDB x = new db.XMLUserDB();
            //data.User user = new data.User(2, "fname Z", "lname", "sname", data.ERoles.student, DateTime.Now, DateTime.Now.AddDays(3));
            //x.add(user);
            //x.update(user);
            //x.delete(user);
            //data.User y = x.get(2);
            //Console.WriteLine(y.lName);
            /*foreach (var item in x.getAll())
            {
                Console.WriteLine(item.ToString() + "\n");
            }*/
            //Console.WriteLine(dao.Manager<db.XMLUserDB>.Instance.Dao.get(1).ToString());
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            SignUp subWindow = new SignUp();
            subWindow.Show();
            this.Close();
        }

        private void SignIN(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.Email.Text + " " + this.Password.Password);
        }
    }
}
