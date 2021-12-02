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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // при нажатии на кнопку реги открывается окно реги и закрывается окно авторизации
        private void LogRegistered_Click(object sender, RoutedEventArgs e)
        {
            new Register().Show();
            this.Close();
        }

        // при нажатии на кнопку авторизации проверяется существование пользователя и соответствие его пароля открывается личный кабинет и закрывается окно авторизации
        private void LogAutor_Click(object sender, RoutedEventArgs e)
        {
            // создание переменных с введенными данными
            string logLogin = LogLogin.Text;
            string logPassword = LogPassword.Password;


            using (var dataBase = new BaseDateEntities2())
            {

                // запрос на проверку введенных данных
                var query = dataBase.Users.Where(logins =>
                logins.Logins == logLogin &&
                logins.Passwords == logPassword).FirstOrDefault();

                // запрос роли по логину
                var query2 = from bace in dataBase.Users
                             where bace.Logins == logLogin
                             select new
                             {
                                 role = bace.TypeUserss
                             };


                // проверяется если в базе данных, учетная запись с данным логином и паролем, если да то открывается личный кабинет
                if (query != null)
                {
                    Person.login = logLogin;
                    Person.password = logPassword;
                    foreach (var i in query2)
                    {
                        Person.userType = Convert.ToInt16(i.role);
                    }
                    new Clietik().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        // чистит поля
        private void LogCancel_Click(object sender, RoutedEventArgs e)
        {
            LogLogin.Clear();
            LogPassword.Clear();
        }

        // закрывает окно
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //скрывает окно
        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // позволяет перемещать окно за тулбар
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }

    // повзоляет передать данные в клиент
    static class Person
    {
        public static string login;
        public static string password;
        public static int userType;
    }
}