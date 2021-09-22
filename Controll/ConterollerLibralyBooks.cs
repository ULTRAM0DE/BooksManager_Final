using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Controll
{
    public class ConterollerLibralyBooks
    {
        internal static List<string> GetGowardLavcraftComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                types = entities.GowardLavcraft.Select(x => x.YesOrNo).ToList();
                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }


        /// <summary>
        /// Получение Id Epic Games для ComboBox
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetJorjOryelComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                types = entities.JorjOryel.Select(x => x.YesOrNo).ToList();
                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }

        /// <summary>
        /// Получение Id Ubisoft Store
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetStivenKingComboBox()
        {
            List<string> types = new List<string>();
            try
            {
                DB.GGWPEntities2 entities = new DB.GGWPEntities2();
                types = entities.StivenKing.Select(x => x.YesOrNo).ToList();
                types.OrderBy(x => x);
                return types;
            }
            catch
            {
                throw new Exception("Error db");
            }
        }
    }
}
