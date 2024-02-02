using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace RaceSimulatorWPFApp
{
    public static class WpfVisualisation
    {
        //Draws a track based on the parameter track
        public static BitmapSource DrawTrack(Model.Track track)
        {
            BitmapSource emptyImage = ImageHandler.CreateBitmapSourceFromGdiBitmap(ImageHandler.GetNewEmptyBitmap(1000, 700));
            return emptyImage;
        }

       
    }
}
