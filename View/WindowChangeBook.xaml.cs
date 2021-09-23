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
    /// Логика взаимодействия для WindowChangeBook.xaml
    /// </summary>
    public partial class WindowChangeBook : Window
    {
        public ModelView.ViewBooks Books { get; }
        public WindowChangeBook()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="books"></param>
        public WindowChangeBook(ModelView.ViewBooks books) : this()
        {
            Books = books;
            tbName.Text = Books.Books.Name;
            tbDescription.Text = Books.Books.Discription;
            tbPrice.Text = Convert.ToString(Books.Books.Price);
            try
            {
                cbJorjOryel.ItemsSource = ConterollerLibralyBooks.GetJorjOryelComboBox();
                cbStivenKing.ItemsSource = ConterollerLibralyBooks.GetStivenKingComboBox();
                cbGowardLavcraft.ItemsSource = ConterollerLibralyBooks.GetGowardLavcraftComboBox();

                var sk = (cbStivenKing.ItemsSource as List<string>)
                    .Single(x => x == books.Books.StivenKing.YesOrNo);
                cbStivenKing.SelectedItem = cbStivenKing.Items.IndexOf(sk);

                var gl = (cbGowardLavcraft.ItemsSource as List<string>)
                   .Single(x => x == books.Books.GowardLavcraft.YesOrNo);
                cbGowardLavcraft.SelectedItem = cbGowardLavcraft.Items.IndexOf(gl);

                var jo = (cbJorjOryel.ItemsSource as List<string>)
                  .Single(x => x == books.Books.JorjOryel.YesOrNo);
                cbJorjOryel.SelectedItem = cbJorjOryel.Items.IndexOf(jo);

                cbImage.ItemsSource = ControllerImage.GetImages();
                cbImage.SelectedIndex = 1;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        /// <summary>
        /// Кнопка удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить", "Удалить обьект?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Controll.ControllerBook.Remove(Books.Books);
                    MessageBox.Show("Обьект удалён");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }
        /// <summary>
        /// Кнопка назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            WindowAllBooks windowAllBooks = new WindowAllBooks();
            windowAllBooks.Show();
            this.Close();
        }
        /// <summary>
        /// Кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Controll.ControllerBook.ChangeGame(tbName.Text, tbDescription.Text, 
                    tbPrice.Text, cbImage.SelectedItem ,cbGowardLavcraft.SelectedItem, cbJorjOryel.SelectedItem, cbStivenKing.SelectedItem,Books.Books))
                {
                    MessageBox.Show("Обьект сохранен");
                }
            }
            catch
            {
                MessageBox.Show("ошибка");
            }
        }
    }
}

