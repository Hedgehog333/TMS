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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private data.User user;
        public EditUser(int id)
        {
            InitializeComponent();
            this.user = UserDatabaseManagerSingleton.Instance.get(id);

            this.txtbFName.Text = this.user.fName;
            this.txtbLName.Text = this.user.lName;
            this.txtbSName.Text = this.user.sName;
            this.txtbEmail.Text = this.user.email;

            if (CurrentUserSingleton.Instance.User.id == id)
            {
                this.cboxRole.SelectedIndex = 0;
                this.cboxRole.IsEnabled = false;
            }
            else
                this.cboxRole.SelectedIndex = (int)user.role;
            this.cboxGroup.ItemsSource = GroupDatabaseManagerSingleton.Instance.getAll();
            this.cboxGroup.DisplayMemberPath = "Name";
            this.cboxGroup.SelectedValuePath = "id";
            try
            {
                this.cboxGroup.SelectedValue = GroupDatabaseManagerSingleton.Instance.get(user.groupId).id;
            }
            catch (Exception ex) { }
        }

        private void btnGoBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSignUpClick(object sender, RoutedEventArgs e)
        {
            bool fN = false, lN = fN, em = fN, pas = fN, gbR = fN, gbN = fN;

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

            if (this.cboxRole.SelectedValue == null)
                this.cboxRole.BorderBrush = Brushes.Red;
            else
            {
                this.cboxRole.BorderBrush = Brushes.Green;
                gbR = true;
            }

            if (fN && lN && em && pas && gbR && gbN)
            {  
                UserDatabaseManagerSingleton.Instance.update(
                    new data.User(
                        user.id,
                        this.txtbFName.Text,
                        this.txtbLName.Text,
                        this.txtbSName.Text,
                        this.txtbEmail.Text,
                        this.pasPassword.Password,
                        (int)this.cboxGroup.SelectedValue,
                        (data.ERoles)this.cboxRole.SelectedIndex,
                        user.registrationDate
                    )
                );
                Console.WriteLine(this.cboxRole.SelectedIndex);
                MessageBox.Show("Save complite.");
                this.Close();
            }
        }
    }
}
