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
using BooksManager.Controll;
using BooksManager.DB;
using BooksManager.View.ModelView;
using SendGrid.Helpers.Mail;

namespace BooksManager.View
{
    /// <summary>
    /// Логика взаимодействия для WindowAllBooks.xaml
    /// </summary>
    public partial class WindowAllBooks : Window
    {

        private int actualList = 1;
        public int actualMax;

        private List<string> constencbFilter = new List<string>
        {
            "Стивен Кинг","Говард Лафкрафт","Джордж Оруэл","Без сортировки"
        };

        private List<View.ModelView.ViewBooks> content = new List<ModelView.ViewBooks>();
        public WindowAllBooks()
        {
            
            InitializeComponent();
            lbContent.MouseDoubleClick += LbContent_MouseDoubleClick;

            try
            {
                content = GetContent();
                Run(content);
                CbFilter.ItemsSource = ContrillerBook.GetBook();
                CbFilter.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        private void LbContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var sours = e.OriginalSource as Border;
            if (sours == null)
            {
                return;
            }
            var books = sours.DataContext as ModelView.ViewBooks;
            WindowChangeBook windowChangeBook = new WindowChangeBook(books);
            windowChangeBook.Show();
            this.Close();
        }

        private void Run(List<View.ModelView.ViewBooks> books)
        {
            lbContent.ItemsSource = null;
            lbContent.ItemsSource = books;
            DinamicStackButton(books.Count);
            actualMax = spButtons.Children.Count - 2;
            var s = IntMin(actualList);
            RefreshContent(s, CountContent(s, books.Count), books);
            labelList.Content = $"лист{actualList}";
            labelItog.Content = $"В базе{GetContent().Count}записей";
            labelRezult.Content = $"В базе{lbContent.Items.Count}записей";

        }
        private void RefreshContent(int start, int end, List<View.ModelView.ViewBooks> books)
        {
            List<View.ModelView.ViewBooks> s = new List<ViewBooks>();

            if (books.Count > end && end > 0)
            {
                s.AddRange(books.GetRange(start, end));
            }
            else
            {
                return;
            }

            lbContent.ItemsSource = null;
            lbContent.ItemsSource = s;
            labelList.Content = $"лист{actualList}";
            labelItog.Content = $"В базе {GetContent().Count}записей";
            labelRezult.Content = $"Выведено {lbContent.Items.Count}записей";
        }

        public static int IntMin(int list)
        {
            return (list * 15) - 15;
        }
        public static int CountContent(int start, int Maxcount)
        {
            int rez = Maxcount - start;
            if(rez>=15)
            {
                return 15;
            }
            else
            {
                return rez;
            }
        }

        private List<ViewBooks> GetContent()
        {
            try
            {
                return ControllerBook.GetViewBooks();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void DinamicStackButton(int count)
        {
            spButtons.Children.Clear();
            int countButton = GetCountButton(count);
            spButtons.Children.Add(CreateButton("btDown", "<<", btDown_Click));

            for (int i = 0; i < GetCountButton(count); i++)
            {
                spButtons.Children.Add(CreateButton($"btNext{i}", $"{ i + 1}", btNext_Click));
            }
            spButtons.Children.Add(CreateButton("btUp", ">>", btUp_Click));
        }
        private Button CreateButton(string name, string content, RoutedEventHandler action)
        {
            var b = new Button() { Name = name, Content = content, Margin = new Thickness(5) };
            b.Padding = new Thickness(4);
            b.Background = new SolidColorBrush(Color.FromArgb(255, 255, 193, 193));
            b.HorizontalAlignment = HorizontalAlignment.Center;

            b.Click += action;

            return b;
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            var but = e.OriginalSource as Button;
            actualList = Convert.ToInt32(but.Content.ToString());
            var s = IntMin(actualList);
            RefreshContent(s, CountContent(s, content.Count), content);
        }
        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            if (actualList > 1)
            {
                actualList--;
                var s = IntMin(actualList);
                RefreshContent(s, CountContent(s, content.Count), content);
            }
        }
        private void btUp_Click(object Sender, RoutedEventArgs e)
        {
            if (actualList < actualMax)
            {
                actualList++;
                var s = IntMin(actualList);
                RefreshContent(s, CountContent(s, content.Count), content);
            }
        }

       

        public int GetCountButton(int count)
        {
            if (count % 15 == 0)
            {
                return count / 15;
            }
            else
            {
                return count / 15 + 1;
            }

        }

        private void txSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var s = content.Where(x => x.Books.Name.ToUpper().StartsWith(txSearch.Text.ToUpper())).ToList();

            
            s.AddRange(content.Where(x => x.Books.Name.ToUpper().Contains(txSearch.Text.ToUpper())));

            s = s.Distinct().ToList();

            if (s.Count < 1)
            {
                MessageBox.Show("Обьект не найден");
                txSearch.Text = string.Empty;
                Run(GetContent());
                return;
            }

            Run(s);
        }

        private void btAddBook_Click(object sender, RoutedEventArgs e)
        {
            View.WindowAddBook windowAddBook = new WindowAddBook();
            windowAddBook.Show();
            this.Close();
        }

        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            View.WindowMenu windowMenu = new WindowMenu();
            windowMenu.Show();
            this.Close();
        }

       

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CbFilter.SelectedItem.ToString())
            {
                case "Без сортировки": Run(GetContent()); break;
                case "Стивен Кинг": StivenFilter(); break;
                case "Говард Лафкрафт": GowardFilter(); break;
                case "Джордж Оруэл": JorgFilter(); break;
                default:break;

            }
        }
        private void StivenFilter()
        {
            content = content.OrderBy(x => x.StivenKingID).ToList();
            Run(content);
        }
        private void GowardFilter()
        {
            content = content.OrderBy(x => x.GowardLavсraftID).ToList();
            Run(content);
        }
        private void JorgFilter()
        {
            content = content.OrderBy(x => x.JorjOryelID).ToList();
            Run(content);
        }
    }
}
