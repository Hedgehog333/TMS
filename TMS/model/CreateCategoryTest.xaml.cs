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
    /// Interaction logic for CreateCategoryTest.xaml
    /// </summary>
    public partial class CreateCategoryTest : Window
    {
        public CreateCategoryTest()
        {
            InitializeComponent();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtbCategoryName.Text))
                if (CategoryDatabaseManagerSingleton.Instance.get(this.txtbCategoryName.Text) == null)
                {
                    CategoryDatabaseManagerSingleton.Instance.add(new data.Categories(-1, this.txtbCategoryName.Text));
                    MessageBox.Show("Save complite.");
                    this.txtbCategoryName.Clear();
                }
                else
                    MessageBox.Show("Category: \"" + this.txtbCategoryName.Text + "\" is already exist");
            else
                MessageBox.Show("Empty field!");
        }
    }
}
