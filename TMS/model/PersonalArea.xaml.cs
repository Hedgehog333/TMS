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

            DependencyObject parent = VisualTreeHelper.GetParent(this.btnCreateGroup);
            if(CurrentUserSingleton.Instance.User.role.Equals(data.ERoles.student))
            {
                this.btnCreateGroup.Visibility = System.Windows.Visibility.Hidden;
                this.btnCrateCategoriesTest.Visibility = System.Windows.Visibility.Hidden;
                this.btnCrateTest.Visibility = System.Windows.Visibility.Hidden;

                (parent as StackPanel).Children.Remove(this.btnCreateGroup);
                (parent as StackPanel).Children.Remove(this.btnCrateCategoriesTest);
                (parent as StackPanel).Children.Remove(this.btnCrateTest);
                (parent as StackPanel).Children.Remove(this.btnDeleteCategory);
                (parent as StackPanel).Children.Remove(this.btnDeleteGroup);
            }
            if (CurrentUserSingleton.Instance.User.role != data.ERoles.admin)
                (parent as StackPanel).Children.Remove(this.btnShowUsers);

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

            List<data.Test> tests = TestDatabaseManagerSingleton.Instance.getAll();
            this.spListTests.Children.Clear();
            foreach (data.Test item in CurrentUserSingleton.Instance.User.role == data.ERoles.student? tests.FindAll(x => x.isDraft == false): tests)
            {
                Grid grid = new Grid()
                {
                    Uid = item.id.ToString(),
                    Height = 78,
                    Background = item.isDraft ? Brushes.LightGray : Brushes.White
                };

                Label title = new Label()
                {
                    Content = item.title,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Width = 426,
                    Margin = new Thickness(10, 10, 0, 0)
                };
                
                grid.Children.Add(title);

                if(!CurrentUserSingleton.Instance.User.role.Equals(data.ERoles.student))
                {
                    Button edit = new Button()
                    {
                        Content = "Edit",
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Height = 27,
                        Width = 48,
                        Margin = new Thickness(468, 22, 0, 0)
                    };
                    edit.Click += this.btnEditTest_Click;

                    Button show = new Button()
                    {
                        Content = "Questions",
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Height = 27,
                        Width = 86,
                        Margin = new Thickness(521, 22, 0, 0)
                    };
                    show.Click += btnShowQuestions_Click;

                    Button delete = new Button()
                    {
                        Content = "Delete",
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Height = 27,
                        Width = 48,
                        Margin = new Thickness(612, 22, 0, 0)
                    };
                    delete.Click += this.btnDeleteTest_Click;

                    grid.Children.Add(edit);
                    grid.Children.Add(show);
                    grid.Children.Add(delete);
                }
                else
                {
                    Button start = new Button()
                    {
                        Content = "Start test",
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        Height = 27,
                        Width = 192,
                        Margin = new Thickness(468, 22, 0, 0)
                    };
                    start.Click += this.btnStartTest_Click;

                    grid.Children.Add(start);
                }

                List<data.Question> qustions = QuestionDatabaseManagerSingleton.Instance.getAll();
                Label countQuestion = new Label() 
                {
                    Content = "Total questions: " + qustions.Count(x => x.testId == item.id).ToString(),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Width = 426,
                    Margin = new Thickness(10, 41, 0, 0)
                };
                
                data.Categories category = CategoryDatabaseManagerSingleton.Instance.get(item.categoriesId);
                Label categoryName = new Label()
                {
                    Content = "Category: " + category.title,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Width = 180,
                    Margin = new Thickness(185, 41, 0, 0)
                };

                grid.Children.Add(countQuestion);
                grid.Children.Add(categoryName);

                this.spListTests.Children.Add(grid);
            }
        }

        private void btnEditTest_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            data.Test test = TestDatabaseManagerSingleton.Instance.get(id);
            CreateTest CT = new CreateTest(test.id,test.title, test.desctiption, test.categoriesId, test.authorId,test.creationDate, test.isDraft);
            CT.ShowDialog();
            this.RefreshTests();

        }
        private void btnShowQuestions_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            ShowQuestions SQ = new ShowQuestions(id);
            SQ.ShowDialog();
            this.RefreshTests();
        }
        private void btnDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            TestDatabaseManagerSingleton.Instance.delete(TestDatabaseManagerSingleton.Instance.get(id));
            this.RefreshTests();
        }
        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((sender as Button).Parent as Grid).Uid);
            PassingTheTest PTT = new PassingTheTest(id);
            PTT.ShowDialog();
        }

        private void btnRefreshTestsClick(object sender, RoutedEventArgs e)
        {
            this.RefreshTests();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //this.Owner.Close();
        }

        private void btnDeleteGroupClick(object sender, RoutedEventArgs e)
        {
            DeleteCategoryOrGroup delGroup = new DeleteCategoryOrGroup(GroupDatabaseManagerSingleton.Instance.getAll());
            delGroup.ShowDialog();
        }

        private void btnDeleteCategoryClick(object sender, RoutedEventArgs e)
        {
            DeleteCategoryOrGroup delCategory = new DeleteCategoryOrGroup(CategoryDatabaseManagerSingleton.Instance.getAll());
            delCategory.ShowDialog();
        }

        private void btnShowResultsClick(object sender, RoutedEventArgs e)
        {
            ShowResult SR = new ShowResult();
            SR.ShowDialog();
        }

        private void btnShowUsersClick(object sender, RoutedEventArgs e)
        {
            ShowUsers SU = new ShowUsers();
            SU.ShowDialog();
        }
    }
}