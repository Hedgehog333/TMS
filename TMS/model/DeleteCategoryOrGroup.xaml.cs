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
    /// Interaction logic for DeleteCategoryOrGroup.xaml
    /// </summary>
    public partial class DeleteCategoryOrGroup : Window
    {
        bool isGroup = true;
        public DeleteCategoryOrGroup(List<data.Categories> categories)
        {
            InitializeComponent();
            this.Title = "Delete Category";
            this.cmbList.ItemsSource = categories;
            this.cmbList.DisplayMemberPath = "title";
            this.cmbList.SelectedValuePath = "id";
        }
        public DeleteCategoryOrGroup(List<data.Group> groups)
        {
            InitializeComponent();
            this.Title = "Delete Group";
            this.cmbList.ItemsSource = groups;
            this.cmbList.DisplayMemberPath = "Name";
            this.cmbList.SelectedValuePath = "id";
            isGroup = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isGroup)
                {
                    GroupDatabaseManagerSingleton.Instance.delete(GroupDatabaseManagerSingleton.Instance.get(Int32.Parse(this.cmbList.SelectedValue.ToString())));
                    this.cmbList.ItemsSource = GroupDatabaseManagerSingleton.Instance.getAll();
                }
                else
                {
                    CategoryDatabaseManagerSingleton.Instance.delete(CategoryDatabaseManagerSingleton.Instance.get(Int32.Parse(this.cmbList.SelectedValue.ToString())));
                    this.cmbList.ItemsSource = CategoryDatabaseManagerSingleton.Instance.getAll();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("An error occurred while removing!");
            }
        }
    }
}
