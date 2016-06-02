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
    /// Interaction logic for CreateTest.xaml
    /// </summary>
    public partial class CreateTest : Window
    {
        public CreateTest()
        {
            InitializeComponent();

            List<data.Categories> dataList = dao.Manager<db.XMLCategoriesDB>.Instance.getAll();
            this.cmbbCategories.ItemsSource = dataList;
            this.cmbbCategories.DisplayMemberPath = "title";
            this.cmbbCategories.SelectedValuePath = "id";
        }

        private void btnCreate(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(this.txtbTitle.Text) && this.cmbbCategories.SelectedValue != null)
            {
                this.txtbTitle.BorderBrush = Brushes.Green;
                this.cmbbCategories.BorderBrush = Brushes.Green;
                dao.Manager<db.XMLTestDB>.Instance.add(
                    new data.Test(
                        -1,
                        this.txtbTitle.Text,
                        this.txtbDescription.Text,
                        (int)this.cmbbCategories.SelectedValue,
                        DateTime.Now,
                        DateTime.Now,
                        CurrentUserSingleton.Instance.User.id,
                        true
                    )
                );
                MessageBox.Show("Save complite.");
                this.txtbTitle.Clear();
                this.txtbDescription.Clear();
                this.cmbbCategories.SelectedValue = null;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.txtbTitle.Text))
                    this.txtbTitle.BorderBrush = Brushes.Red;
                if(this.cmbbCategories.SelectedValue == null)
                    this.cmbbCategories.BorderBrush = Brushes.Red;
            }
        }

        private void btnGoBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
