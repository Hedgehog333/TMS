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
    /// Interaction logic for CreateGroup.xaml
    /// </summary>
    public partial class CreateGroup : Window
    {
        public CreateGroup()
        {
            InitializeComponent();
        }
        private void btnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(this.txtbGroupName.Text))
                if(GroupDatabaseManagerSingleton.Instance.get(this.txtbGroupName.Text) == null )
                {
                    GroupDatabaseManagerSingleton.Instance.add(new data.Group(-1, this.txtbGroupName.Text));
                    MessageBox.Show("Save complite.");
                    this.txtbGroupName.Clear();
                }
                else
                    MessageBox.Show("Group:\"" + this.txtbGroupName.Text + "\" is already exist");
            else
                MessageBox.Show("Empty field!");
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}