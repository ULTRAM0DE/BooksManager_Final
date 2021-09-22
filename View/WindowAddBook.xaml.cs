using BooksManager.Controll;
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
    /// Логика взаимодействия для WindowAddBook.xaml
    /// </summary>
    public partial class WindowAddBook : Window
    {
        public WindowAddBook()
        {
            InitializeComponent();
            this.Loaded += WindowAddBook_Loaded;
        }
        private void WindowAddBook_Loaded(object sender,RoutedEventArgs e)
        {
            try
            {
                Run();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Run()
        {
            cbJorjOryel.ItemsSource = ConterollerLibralyBooks.GetJorjOryelComboBox();
            cbStivenKing.ItemsSource = ConterollerLibralyBooks.GetStivenKingComboBox();
            cbGowardlavcraft.ItemsSource = ConterollerLibralyBooks.GetGowardLavcraftComboBox();
            cbImage.ItemsSource = ControllerImage.GetImages();
        }

        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            View.WindowMenu windowMenu = new WindowMenu();
            windowMenu.Show();
            this.Close();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("имя не задано");
                return;
            }
            if(string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                MessageBox.Show("Укажите цену");
                return;
            }

            try
            {
                double p = Convert.ToDouble(tbPrice.Text);
                if(p<0)
                {
                    MessageBox.Show("Цена не может быть отрицательной ");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Укажите верный формат цены");
            }
            try
            {
                if (ControllerBook.AddBook(tbName.Text, tbDescription.Text, 
                    tbPrice.Text,cbGowardlavcraft.SelectedItem,cbStivenKing.SelectedItem,cbJorjOryel.SelectedItem))
                {
                    MessageBox.Show("Обьект добавлен в БД");
                }
                else
                {
                    MessageBox.Show("Объект не   добавлен в  БД");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления в  бд");
            }

        }
    }
}
