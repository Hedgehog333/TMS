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
    /// Interaction logic for ShowUsers.xaml
    /// </summary>
    public partial class ShowUsers : Window
    {
        public ShowUsers()
        {
            InitializeComponent();
            RefreshUseers();
        }
        private void RefreshUseers()
        {

            List<data.User> users = UserDatabaseManagerSingleton.Instance.getAll();


            this.spListUsers.Children.Clear();
            foreach (data.User item in users)
            {
                Grid grid = new Grid()
                {
                    Uid = item.id.ToString(),
                    MinHeight = 80,

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
                    Text = item.fName + " " + item.lName,
                    Margin = new Thickness(10, 10, 10, 0),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
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
                edit.Click += this.btnEditUser_Click;

                grid.Children.Add(edit);


                Button delete = new Button()
                {
                    Content = "Delete",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 33,
                    Width = 75,
                    Margin = new Thickness(10, 42, 0, 0),
                    IsEnabled = item.id == CurrentUserSingleton.Instance.User.id ? false : true
                };
                Grid.SetColumn(delete, 1);
                delete.Click += this.btnDeleteUser_Click;

                grid.Children.Add(delete);


                this.spListUsers.Children.Add(grid);
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            UserDatabaseManagerSingleton.Instance.delete(UserDatabaseManagerSingleton.Instance.get(id));
            this.RefreshUseers();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            EditUser ES = new EditUser(id);
            ES.ShowDialog();
            this.RefreshUseers();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
