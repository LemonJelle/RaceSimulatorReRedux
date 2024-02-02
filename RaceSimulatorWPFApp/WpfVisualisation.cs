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
        //Section and car images
        #region images 
        //Track pieces

        //Straight
        private const string _straightHoizontal = @".\Images\TrackPieces\StraightHorizontal.png";
        private const string _straightVertical = @".\Images\TrackPieces\StraightVertical.png";

        //Finish
        private const string _finishHorizontal = @".\Images\TrackPieces\FinishHorizontal.png";
        private const string _finishVertical = @".\Images\TrackPieces\FinishVertical.png";
        
        //Start grid
        private const string _startHorizontal = @".\Images\TrackPieces\StartHorizontal.png";
        private const string _startVertical = @".\Images\TrackPieces\StartVertical.png";

        //Corners
        private const string _leftCornerHorizontal = @".\Images\TrackPieces\LeftCornerHorizontal.png";
        private const string _leftCornerVertical = @".\Images\TrackPieces\LeftCornerVertical.png";
        private const string _rightCornerHorizontal = @".\Images\TrackPieces\RightCornerHorizontal.png";
        private const string _rightCornerVertical = @".\Images\TrackPieces\RightCornerVertical.png";
        

        #endregion

        //Draws a track based on the parameter track
        public static BitmapSource DrawTrack(Model.Track track)
        {
            BitmapSource emptyImage = ImageHandler.CreateBitmapSourceFromGdiBitmap(ImageHandler.GetNewEmptyBitmap(1000, 700));
            return emptyImage;
        }




       
    }
}
