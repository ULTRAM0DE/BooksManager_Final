﻿using BooksManager.DB;
using BooksManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BooksManager.Controll
{
    internal class ControllerBook
    {
      public static List<View.ModelView.ViewBooks> GetViewBooks()
        {
            try
            {
                DB.GGWPEntities2 entities1 = new DB.GGWPEntities2();
                var books = entities1.Books.ToList();

                List<View.ModelView.ViewBooks> views = new List<View.ModelView.ViewBooks>();

                foreach(var item in books)
                {
                    views.Add(new View.ModelView.ViewBooks(item));
                }
                return views;
            }

            catch
            {
                throw new Exception("ошибка БД");
            }
        }

        internal static bool AddBook(string name,string description,string price,object gowardlavcraft, object stivenking, object jorjoryel)
        {
            DB.Books books = new DB.Books();

            try
            {
                books = new DB.Books();
               
                books.Discription = description;
                books.Id_GowardLavcraft = GetIdGowardLavcraft(gowardlavcraft as string);
                books.Id_StivenKing = GetIdStivenKing(stivenking as string);
                books.Id_JorjOryel = GetIdJorjOryel(jorjoryel as string);
                books.Price = Convert.ToInt32(price);
                books.Name = name;



            }
            catch
            {
                throw new Exception("ошибка входных данных");
            }

            if(books == null)
            {
                return false;
            }
            try
            {
                DB.GGWPEntities2 entities1 = new DB.GGWPEntities2();
                entities1.Books.Add(books);
                entities1.SaveChanges();
                return true;
            }
            catch    
            {
                throw new Exception("adsad");
            }
            
            
        }

        internal static void Remove(DB.Books books)
        {
            try
            {
                DB.GGWPEntities2 entities2 = new DB.GGWPEntities2();
                entities2.Books.Remove(entities2.Books.Find(books.Id));
                entities2.SaveChanges();

            }
            catch
            {
                throw new Exception("Удаление не получилось");
            }
        }



        private static int GetIdJorjOryel(string name)
        {
            try
            {
                DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                return entities.JorjOryel.Where(x => x.YesOrNo == name).First().Id;
            }
            catch
            {
                throw new Exception("Магазин не найден");
            }
        }



        private static int GetIdStivenKing(string name)
        {
            try
            {
                DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                return entities.StivenKing.Where(x => x.YesOrNo == name).First().Id;
            }
            catch
            {
                throw new Exception("Магазин не найден");
            }
        }

        internal static bool ChangeGame(string name, string description, string price, object gowardlavcraft, object jorjoryel, object stivenking, DB.Books books)
        {
            DB.GGWPEntities2 entities2 = new GGWPEntities2();
            DB.Books book = entities2.Books.Find(books.Id);

            try
            {
                books = new DB.Books();

                books.Discription = description;
                books.Id_GowardLavcraft = GetIdGowardLavcraft(gowardlavcraft as string);
                books.Id_StivenKing = GetIdStivenKing(stivenking as string);
                books.Id_JorjOryel = GetIdJorjOryel(jorjoryel as string);
                books.Price = Convert.ToInt32(price);
                books.Name = name;



            }
            catch
            {
                throw new Exception("ошибка входных данных");
            }
            if (books == null)
            {
                return false;
            }
            try
            {
                DB.GGWPEntities2 entities1 = new DB.GGWPEntities2();
                entities1.Books.Add(books);
                entities1.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("adsad");
            }

        }

        private static int GetIdGowardLavcraft(string name)
        {
            try
            {
                DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                return entities.GowardLavcraft.Where(x => x.YesOrNo == name).First().Id;
            }
            catch
            {
                throw new Exception("Магазин не найден");
            }
        }
    }
}
