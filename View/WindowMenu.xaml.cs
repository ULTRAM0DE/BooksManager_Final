using BooksManager.DB;
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

namespace BooksManager.View
{
    /// <summary>
    /// Логика взаимодействия для WindowMenu.xaml
    /// </summary>
    public partial class WindowMenu : Window
    {
        public WindowMenu()
        {
            InitializeComponent();
        }
        public WindowMenu(User user) : this()
        {
            Title += $"Вход ({user.Name}) {user.TypeUser.Name}";
        }
        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btAllBooks_Click(object sender, RoutedEventArgs e)
        {
            WindowAllBooks windowAllBooks = new WindowAllBooks();
            windowAllBooks.Show();
            this.Close();
        }
    }
}
