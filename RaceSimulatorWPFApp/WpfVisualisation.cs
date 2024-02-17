using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Model;

namespace RaceSimulatorWPFApp
{
    public enum Direction
    {
        North,
        South,
        East,
        West
    }

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

        //Normal participants
        private const string _blueCar = @".\Images\Participants\BlueCar.png";
        private const string _greenCar = @".\Images\Participants\GreenCar.png";
        private const string _greyCar = @".\Images\Participants\GreyCar.png";
        private const string _redCar = @".\Images\Participants\RedCar.png";
        private const string _yellowCar = @".\Images\Participants\YellowCar.png";

        //Broken participants
        private const string _blueCarBroken = @".\Images\Participants\BrokenCars\BlueCarBroken.png";
        private const string _greenCarBroken = @".\Images\Participants\BrokenCars\GreenCarBroken.png";
        private const string _greyCarBroken = @".\Images\Participants\BrokenCars\GreyCarBroken.png";
        private const string _redCarBroken = @".\Images\Participants\BrokenCars\RedCarBroken.png";
        private const string _yellowCarBroken = @".\Images\Participants\BrokenCars\YellowCarBroken.png";


        #endregion

        private static int _cursorX;
        private static int _cursorY;
        private static Direction _currentDirection;

        private static Bitmap _trackBitmap;
        private static Graphics _trackGraphics;

        public static void Initalise()
        {
            _currentDirection = Direction.East;
        }

        //Draws a track based on the parameter track
        public static BitmapSource DrawTrack(Model.Track track)
        {
            Initalise();

            _cursorX = 384;
            _cursorY = 192;

            _trackBitmap = ImageHandler.GetNewEmptyBitmap(3000, 2100);   //Track dimensions are static for now
            _trackGraphics = Graphics.FromImage(_trackBitmap);
            
            //Loop through sections
            foreach (Section section in track.Sections)
            {
                DrawSection(DecideSectionToDraw(section));
                if (section.SectionType == SectionTypes.LeftCorner || section.SectionType == SectionTypes.RightCorner)
                {
                    ChangeDirection(section.SectionType);
                }
                ChangeCursorPosition();
            }
            BitmapSource bitMapSourceImage = ImageHandler.CreateBitmapSourceFromGdiBitmap(_trackBitmap);
            return bitMapSourceImage;
        }

        public static void DrawSection(Bitmap bitmapToDraw)
        {
            bool reverse = ToReverseOrNotToReverse();
            if (reverse)
            {
                //reverse the bitmap
                bitmapToDraw = ReverseSectionBitmap(bitmapToDraw);
            }

            _trackGraphics.DrawImage(bitmapToDraw, _cursorX, _cursorY);

            //Reverse again if the new direction is north or west



        }

        private static bool ToReverseOrNotToReverse()
        {
            return _currentDirection == Direction.North || _currentDirection == Direction.West;
        }

        public static Bitmap DecideSectionToDraw(Section section)
        {
            SectionTypes sectionType = section.SectionType;
            switch (sectionType)
            {
                case SectionTypes.StartGrid:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return GetSectionBitmap(_startHorizontal);
                    }
                    else
                    {
                        return GetSectionBitmap(_startVertical);
                    }
                case SectionTypes.Finish:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return GetSectionBitmap(_finishHorizontal);
                    }
                    else
                    {
                        return GetSectionBitmap(_finishVertical);
                    }
                case SectionTypes.Straight:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return GetSectionBitmap(_straightHoizontal);
                    }
                    else
                    {
                        return GetSectionBitmap(_straightVertical);
                    }
                case SectionTypes.LeftCorner:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return GetSectionBitmap(_leftCornerHorizontal);
                    }
                    else
                    {
                        return GetSectionBitmap(_leftCornerVertical);
                    }
                case SectionTypes.RightCorner:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return GetSectionBitmap(_rightCornerHorizontal);
                    }
                    else
                    {
                        return GetSectionBitmap(_rightCornerVertical);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, "Something went wrong with the sections! The section may be unsupported.");

            }
        }

        public static void ChangeDirection(SectionTypes sectionType)
        {
            switch (sectionType)
            {
                case SectionTypes.LeftCorner:
                    switch (_currentDirection)
                    {
                        case Direction.North:
                            _currentDirection = Direction.West;
                            break;
                        case Direction.South:
                            _currentDirection = Direction.East;
                            break;
                        case Direction.East:
                            _currentDirection = Direction.North;
                            break;
                        case Direction.West:
                            _currentDirection = Direction.South;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(_currentDirection), _currentDirection, "Something went wrong with the directions! The direction may be unsupported.");

                    };
                    break;
                case SectionTypes.RightCorner:
                    switch (_currentDirection)
                    {
                        case Direction.North:
                            _currentDirection = Direction.East;
                            break;
                        case Direction.South:
                            _currentDirection = Direction.West;
                            break;
                        case Direction.East:
                            _currentDirection = Direction.South;
                            break;
                        case Direction.West:
                            _currentDirection = Direction.North;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(_currentDirection), _currentDirection, "Something went wrong with the directions! The direction may be unsupported.");
                    };
                    break;
            }
        }

        //Standard section width and height is 192
        public static void ChangeCursorPosition()
        {
            switch (_currentDirection)
            {
                case Direction.North:
                    _cursorY -= 192;
                    break;
                case Direction.East:
                    _cursorX += 192;
                    break;
                case Direction.South:
                    _cursorY += 192;
                    break;
                case Direction.West:
                    _cursorX -= 192;
                    break;

            }
        }

        public static Bitmap GetSectionBitmap(string path)
        {
            return new Bitmap(ImageHandler.GetBitmapImage(path));
        }

        public static Bitmap ReverseSectionBitmap(Bitmap bitmap)
        {
            Bitmap reversedBitmap = new Bitmap(bitmap);
            reversedBitmap.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            return reversedBitmap;
        }

        public static Bitmap ShowParticipantsOnBitmap(Bitmap input, SectionTypes type, IParticipant left, IParticipant right)
        {
            string pathLeft;
            string pathRight;
            Bitmap newBitmap = (Bitmap)input.Clone();
            Graphics participantGraphics = Graphics.FromImage(newBitmap);

            //left
            if (left != null)
            {

                Bitmap leftBitmap = new Bitmap(ImageHandler.GetBitmapImage(GetPath(left)));
                switch (type)
                {
                    case SectionTypes.Straight:
                        if (_currentDirection == Direction.North)
                        {
                            //rotate bitmap so it faces the right direction
                        }
                }
                
            }

            return null;
        }

        //checks if participant is broken, if so, return the broken path, if not, return the normal path
        public static bool IsBroken(IParticipant participant)
        {
            return participant.Equipment.IsBroken;
        }

        public static string GetPath(IParticipant participant)
        {
            if (participant.TeamColor == TeamColors.Blue)
            {
                if (IsBroken(participant))
                {
                    return _blueCarBroken;
                }
                else
                {
                    return _blueCar;
                }
            }
            else if (participant.TeamColor == TeamColors.Green)
            {
                if (IsBroken(participant))
                {
                    return _greenCarBroken;
                }
                else
                {
                    return _greenCar;
                }
            }
            else if (participant.TeamColor == TeamColors.Grey)
            {
                if (IsBroken(participant))
                {
                    return _greyCarBroken;
                }
                else
                {
                    return _greyCar;
                }
            }
            else if (participant.TeamColor == TeamColors.Red)
            {
                if (IsBroken(participant))
                {
                    return _redCarBroken;
                }
                else
                {
                    return _redCar;
                }
            }
            else if (participant.TeamColor == TeamColors.Yellow)
            {
                if (IsBroken(participant))
                {
                    return _yellowCarBroken;
                }
                else
                {
                    return _yellowCar;
                }
            }
            else
            {
                return null;
            }

            
        }
    }
}
