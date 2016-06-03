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
    /// Interaction logic for ShowResult.xaml
    /// </summary>
    public partial class ShowResult : Window
    {
        public ShowResult()
        {
            InitializeComponent();
            if (CurrentUserSingleton.Instance.User.role == data.ERoles.student)
            {
                this.dgResults.Columns.RemoveAt(0);
                this.dgResults.Columns.RemoveAt(0);
                this.dgResults.Columns.RemoveAt(0);
            }
            List<data.ResultViewBindingDataGrid> binding = new List<data.ResultViewBindingDataGrid>();
            List<data.Result> results = CurrentUserSingleton.Instance.User.role == data.ERoles.student?
                ResultDatabaseManagerSingleton.Instance.getAll().FindAll(x => x.userId == CurrentUserSingleton.Instance.User.id)
                :ResultDatabaseManagerSingleton.Instance.getAll();
            
            foreach (data.Result item in results)
            {
                /*if
                    (
                        CurrentUserSingleton.Instance.User.role != data.ERoles.student?
                        TestDatabaseManagerSingleton.Instance.get("authorId", CurrentUserSingleton.Instance.User.id.ToString()).id == item.testId
                        : true
                    )
                {*/
                    data.User user = UserDatabaseManagerSingleton.Instance.get(item.userId);
                    binding.Add(new data.ResultViewBindingDataGrid()
                    {
                        UserName = user.fName,
                        UserLastname = user.lName,
                        UserGroup = GroupDatabaseManagerSingleton.Instance.get(user.groupId).Name,
                        TestName = TestDatabaseManagerSingleton.Instance.get(item.testId).title,
                        correctQuestion = item.correctQuestion,
                        totalQuestion = item.totalQuestion,
                        compliteTest = item.compliteTest
                    });
               // }
            }
            this.dgResults.ItemsSource = binding;
        }
    }
}
