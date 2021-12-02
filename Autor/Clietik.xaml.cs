using Autor.Properties;
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

namespace Autor
{
    /// <summary>
    /// Логика взаимодействия для Clietik.xaml
    /// </summary>
    public partial class Clietik : Window
    {
        public Clietik()
        {
            InitializeComponent();
            string loginss = Person.login;
            int userss = Person.userType;

            // проверка на уровень доступа, здесь на пользователя или уровни выше, если пользователь, то не будет информации
            if (userss > 1)
            {
                // если модератор, показывается логин и роль, если уровень выше то + пароль
                if (userss == 2)
                {
                    using (var dataBase = new BaseDateEntities2())
                    {
                        var quertyView = (from bace in dataBase.Users
                                          join TypeUsers in dataBase.TypeUsers on bace.TypeUserss equals TypeUsers.IdType
                                          select new
                                          {
                                              loginDG = bace.Logins,
                                              roleDG = TypeUsers.NameType
                                          }).ToList();
                        DGUser.ItemsSource = quertyView;
                        PassInDG.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    using (var dataBase = new BaseDateEntities2())
                    {
                        var quertyView = (from bace in dataBase.Users
                                          join TypeUsers in dataBase.TypeUsers on bace.TypeUserss equals TypeUsers.IdType
                                          select new
                                          {
                                              loginDG = bace.Logins,
                                              passwordDG = bace.Passwords,
                                              roleDG = TypeUsers.NameType
                                          }).ToList();
                        DGUser.ItemsSource = quertyView;
                    }
                }
            }
            else
            {
                UserSecret.Text = "    У вас нет прав для просмотра информации. \n \n                                                             Совет О5"; // я знаю что так нельзя, но мне лень тут делать разметку + это бесполезная инфа
                UserSecretImage.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Source/SCP_Found.png"));
                DGUser.Visibility = Visibility.Hidden;
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Visibility = Visibility.Hidden;
            }
        }
    }
}
