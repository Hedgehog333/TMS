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
    /// Interaction logic for PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Window
    {
        public PersonalArea()
        {
            InitializeComponent();
            this.Wellcome.Content += CurrentUserSingleton.Instance.User.lName + " " + CurrentUserSingleton.Instance.User.fName;

            if(CurrentUserSingleton.Instance.User.role.Equals(data.ERoles.student))
            {
                this.btnCreateGroup.Visibility = System.Windows.Visibility.Hidden;
                this.btnCrateCategoriesTest.Visibility = System.Windows.Visibility.Hidden;
                this.btnCrateTest.Visibility = System.Windows.Visibility.Hidden;
            }
            this.RefreshTests();
        }

        private void btnSignOutClick(object sender, RoutedEventArgs e)
        {
            CurrentUserSingleton.Clear();
            this.Owner.Show();
            this.Close();
        }

        private void btnCreateGroupClick(object sender, RoutedEventArgs e)
        {
            CreateGroup CG = new CreateGroup();
            CG.ShowDialog();
        }

        private void btnCrateTestClick(object sender, RoutedEventArgs e)
        {
            CreateTest CT = new CreateTest();
            CT.ShowDialog();
            this.RefreshTests();
        }
        private void btnCrateCategoriesTestClick(object sender, RoutedEventArgs e)
        {
            CreateCategoryTest CCT = new CreateCategoryTest();
            CCT.ShowDialog();
        }

        public void RefreshTests()
        {

            List<data.Test> tests = dao.Manager<db.XMLTestDB>.Instance.getAll();
            this.spListTests.Children.Clear();
            foreach (data.Test item in tests)
            {
                /*                
                <Grid Uid="1" Height="77" >
                    <Label Content="Label" HorizontalAlignment="Left" Margin="10,10,0,0" VerticalAlignment="Top" Width="426"/>
                    <Button Content="Edit" HorizontalAlignment="Left" Margin="468,22,0,0" VerticalAlignment="Top" Width="48" Height="27"/>
                    <Button Content="Delete" HorizontalAlignment="Left" Margin="612,22,0,0" VerticalAlignment="Top" Width="48" Height="27"/>
                    <Button Content="Add Question" HorizontalAlignment="Left" Margin="521,22,0,0" VerticalAlignment="Top" Width="86" Height="27"/>
                    <Label Content="Total questions: " HorizontalAlignment="Left" Margin="10,41,0,0" VerticalAlignment="Top" Width="170"/>
                    <Label Content="Category: " HorizontalAlignment="Left" Margin="185,41,0,0" VerticalAlignment="Top" Width="170"/>
                </Grid>
                 */
                Grid grid = new Grid();
                grid.Uid = item.id.ToString();
                grid.Height = 78;

                Label title = new Label();
                title.Content = item.title;
                title.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                title.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                title.Width = 426;
                title.Margin = new Thickness(10, 10, 0, 0);
                
                grid.Children.Add(title);

                if(!CurrentUserSingleton.Instance.User.role.Equals(data.ERoles.student))
                {
                    Button edit = new Button();
                    edit.Content = "Edit";
                    edit.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    edit.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    edit.Height = 27;
                    edit.Width = 48;
                    edit.Margin = new Thickness(468, 22, 0, 0);
                    edit.Click += this.btnEditQuestion_Click;

                    Button add = new Button();
                    add.Content = "Add Question";
                    add.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    add.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    add.Height = 27;
                    add.Width = 86;
                    add.Margin = new Thickness(521, 22, 0, 0);
                    add.Click += btnAddQuestion_Click;

                    Button delete = new Button();
                    delete.Content = "Delete";
                    delete.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    delete.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    delete.Height = 27;
                    delete.Width = 48;
                    delete.Margin = new Thickness(612, 22, 0, 0);
                    delete.Click += this.btnDeleteQuestion_Click;

                    grid.Children.Add(edit);
                    grid.Children.Add(add);
                    grid.Children.Add(delete);
                }
                else
                {
                    Button start = new Button();
                    start.Content = "Start test";
                    start.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    start.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    start.Height = 27;
                    start.Width = 192;
                    start.Margin = new Thickness(468, 22, 0, 0);
                    start.Click += this.btnStartTest_Click;

                    grid.Children.Add(start);
                }

                Label countQuestion = new Label();
                countQuestion.Content = "Total questions: " + dao.Manager<db.XMLQuestionDB>.Instance.getForTestId(item.id).Count.ToString();
                countQuestion.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                countQuestion.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                countQuestion.Width = 426;
                countQuestion.Margin = new Thickness(10, 41, 0, 0);

                Label categoryName = new Label();
                data.Categories category = dao.Manager<db.XMLCategoriesDB>.Instance.get(item.categoriesId);
                categoryName.Content = "Category: " + category.title;
                categoryName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                categoryName.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                categoryName.Width = 426;
                categoryName.Margin = new Thickness(185, 41, 0, 0);

                grid.Children.Add(countQuestion);
                grid.Children.Add(categoryName);

                this.spListTests.Children.Add(grid);
            }
        }

        private void btnEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);

        }
        private void btnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);

        }
        private void btnDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            dao.Manager<db.XMLTestDB>.Instance.delete(dao.Manager<db.XMLTestDB>.Instance.get(id));
            this.RefreshTests();
        }
        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);

        }
    }
}