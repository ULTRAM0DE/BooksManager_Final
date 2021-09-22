using System.Collections.Generic;
using System.IO;
using System.Linq;
using BooksManager.View;



namespace BooksManager.Controll
{
    class ControllerImage
    {

        public static List<Image> GetImages()
        {

            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));


            wanted_path += @"\\Image";

            string[] allfolders = Directory.GetFiles(wanted_path);

            List<string> allfoldersJPG = allfolders.Where(c => (c.EndsWith(".jpg")) || (c.EndsWith(".jpeg")) || (c.EndsWith(".png"))).ToList();

            List<View.Image> images = new List<Image>();

            foreach (var item in allfoldersJPG)
            {
                images.Add(new Image { ImagePath = item, NameImage = Path.GetFileName(item) });
            }

            return images;
        }
    }
}
