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
using System.Windows.Shapes;

using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Text.RegularExpressions;

namespace Autor
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        public Register()
        {
            InitializeComponent();
        }

        // при нажатии на кнопку регистрации если все условия выполнены то создается новая учетная запись
        private void RegRegist_Click(object sender, RoutedEventArgs e)
        {
            // создание переменных с введенными данными
            string regLogin = login.Text;
            string regPassword = password.Password;
            string regPasswordRepeat = passwordRepeat.Password;

            // проверка на символы в логине
            if (CheckTextLogin(regLogin))
            {
                // проверка на пробелы в пароле
                if (CheckTextPassword(regPassword))
                {
                    // проверка на пустоту логина
                    if (regLogin.Length > 0)
                    {
                        // проверка на пустоту 1-го пароля
                        if (regPassword.Length > 0)
                        {
                            // проверка на пустоту 2-го пароля
                            if (regPasswordRepeat.Length > 0)
                            {
                                // проверка на совпадение паролей
                                if (regPassword == regPasswordRepeat)
                                {
                                    using (var dataBase = new BaseDateEntities2())
                                    {
                                        // создание переменной с запросом в базу данных
                                        var QuertyCheck = dataBase.Users.Where(logins =>
                                        logins.Logins.Equals(regLogin)).FirstOrDefault();

                                        // проверяется если в базе данных, введенный логин и если нет то создается новый пользователь и открывается окно авторизации
                                        if (QuertyCheck == null)
                                        {
                                            // создание записи о пользователе в базу данных
                                            dataBase.Users.Add(new Users()
                                            {
                                                Logins = regLogin,
                                                Passwords = regPassword,
                                                TypeUserss = 1
                                            });

                                            // сохранение изменений в базе данных
                                            dataBase.SaveChanges();

                                            new MainWindow().Show();
                                            this.Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Логин занят");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пароли не совпадают");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Повторите пароль");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Напишите пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Напишите логин");
                    }
                }
                else
                {
                    QuestioPassword.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Source/QuestAfter.png"));
                }
            }
            else
            {
                QuestioLogin.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Source/QuestAfter.png"));
            }
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

        // при нажатии на кнопку открывается окно авторизации
        private void ReturnLogin_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        // проверка введенного на разрешенные символы в логине
        private bool CheckTextLogin(string ChText)
        {
            bool res;
            string patternLog = @"^[A-Za-z0-9_]{3,50}$";
            if (Regex.IsMatch(ChText, patternLog))
            {
                res = true;
            }
            else
            {
                res = false;
            }

            return res;
        }

        // проверка введенного пароля на пробелы в пароле
        private bool CheckTextPassword(string ChText)
        {
            bool res;
            string patternLog = @"^\S{3,50}$";
            if (Regex.IsMatch(ChText, patternLog))
            {
                res = true;
            }
            else
            {
                res = false;
            }

            return res;
        }
    }
}
