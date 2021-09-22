using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.View
{
    internal class ContrillerBook
    {
        internal static List<string> GetBook()
        {
            List<string> types = new List<string>();
            try
            {
                DB.GGWPEntities2 entities1 = new DB.GGWPEntities2();
                types = entities1.Books.Select(x => x.Name).ToList();
                types.OrderBy(x => x);
                types.Insert(0, "Без фильтра");
                return types;

            }
            catch
            {
                throw new Exception("Error DB");
            }
        }
        internal static List<string> GetBookComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.GGWPEntities2 entities1 = new DB.GGWPEntities2();
                types = entities1.Books.Select(x => x.Name).ToList();

                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error BD");
            }
        }
            
    }
}
