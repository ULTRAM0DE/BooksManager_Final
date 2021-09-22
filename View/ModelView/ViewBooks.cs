using BooksManager.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.View.ModelView
{
    public class ViewBooks
    {
        public Books Books { get; set; }

        public string Image { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
       
       public int StivenKingID{get;set;}
       public int  GowardLavсraftID{get;set;}
       public int  JorjOryelID{get;set;}
         public string Libraly { get; set; }
       
       


        public ViewBooks(DB.Books books)
        {
            Image = string.IsNullOrWhiteSpace(books.ImagePath) ? @"/Image\NoImage.jpg" : books.ImagePath;

            Image = books.ImagePath;
            Books = books;
            Name = $"{books.Name} | {books.Name}";
            Price = $"{books.Price}";
            Libraly = $"Стивен кинг:{books.StivenKing.YesOrNo} | Джордж Оруэл:{books.JorjOryel.YesOrNo} | Говард Лавкрафт:{books.GowardLavcraft.YesOrNo}";
            StivenKingID = books.Id_StivenKing;
            GowardLavсraftID = books.Id_GowardLavcraft;
            JorjOryelID = books.Id_JorjOryel;
        }

        
 
    }
}
