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
using BooksManager.DB;
using static BooksManager.MainWindow;

namespace BooksManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            tbName.Text = "GGWP";
            tbPassword.Text = "432";
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = Authentication.AuthenticationUser(tbName.Text, tbPassword.Text);
                View.WindowMenu menu = new View.WindowMenu(user);
                MessageBox.Show("Добро пожаловать пользователь " + user.Name);
                menu.Show();
                this.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class Authentication
        {
            public static DB.User AuthenticationUser(string login, string password)
            {
                try
                {
                    DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                    var user = entities.User.Single(x => x.Login == login && x.Password == password);
                    if (user != null)
                    {
                        DB.Logs logs = new DB.Logs() { UserLogin = user.Login, Data = DateTime.Now };
                        entities.Logs.Add(logs);
                        entities.SaveChanges();
                        return user;
                    }
                    else throw new Exception($"Пользователь не найден");
                }
                catch(Exception ex)
                {
                    throw new Exception($"ошибка авторизации \r {ex.Message}");
                }
            }
        }
    }
}
