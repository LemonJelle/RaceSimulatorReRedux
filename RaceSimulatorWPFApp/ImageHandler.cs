using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace RaceSimulatorWPFApp
{
    //Creates, loads and edits images
    public static class ImageHandler
    {
        //Dictionary to match paths with bitmaps, act as cache
        private static Dictionary<string, Bitmap> _imageCache = new();

        //Gets image from the path in a bitmap
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

        //Empty the cache
        private static void EmptyCache()
        {
            _imageCache.Clear();
        }

        //Gets a new, empty bitmap with specified width and height
        private static Bitmap GetNewEmptyBitmap(int width, int height) 
        {
            string key = "empty";
            try
            {
                return (Bitmap)_imageCache[key].Clone();
            }
            catch (KeyNotFoundException)
            {
                _imageCache.Add(key, new Bitmap(width, height));
                Graphics graphics = Graphics.FromImage(_imageCache[key]);
                graphics.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(97, 245, 32)), 0, 0, width, height);
                return (Bitmap)_imageCache[key].Clone();
            }
           
        }

        //Template code from Tasker that magically converts a Bitmap to a BitmapSource
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
