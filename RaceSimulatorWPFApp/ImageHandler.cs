using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RaceSimulatorWPFApp
{
    //Creates, loads and edits images
    public static class ImageHandler
    {
        private static Dictionary<string, Bitmap> _imageCache = new();

        private static Bitmap GetBitmapImage(string path)
        {
            try
            {
                return _imageCache[path];
            }
            catch (KeyNotFoundException)
            {
                Bitmap image = new Bitmap(path);
                _imageCache.Add(path, image);
                return _imageCache[path];
            }
        }

        private static void EmptyCache()
        {
            _imageCache.Clear();
        }
    }
}
