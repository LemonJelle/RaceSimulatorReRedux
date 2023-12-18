﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;

namespace RaceSimulatorReRedux
{
    enum Direction
    {
        North,
        South,
        East,
        West
    }
    public static class Visualisation
    {
        #region graphics
        //Horizontal sections
        private static string[] _startHorizontal = 
        { 
            "----", 
            "  1)", 
            " 2) ", 
            "----" 
        };

        private static string[] _finishHorizontal = 
        { 
            "----", 
            "1 | ", 
            "2 | ", 
            "----" 
        };

        private static string[] _straightHorizontal =
        {
            "----",
            "1   ",
            "2   ",
            "----"
        };

        //Vertical sections
        private static string[] _startVertical =
        {
            "|2 |",
            "|-1|",
            "| -|",
            "|  |"
        };

        private static string[] _finishVertical =
       {
            "|21|",
            "|  |",
            "|==|",
            "|  |"
        };

        private static string[] _straightVertical = 
        { 
            "|21|", 
            "|  |", 
            "|  |", 
            "|  |" 
        };

       

        //Corners
        private static string[] _rightCornerHorizontal = 
        { 
            "---|", 
            "1  |", 
            "2  |", 
            "|  |" 
        };

        private static string[] _leftCornerHorizontal =
        {
            "|  |",
            "1  |",
            "2  |",
            "---|"
        };

        private static string[] _rightCornerVertical = 
        { 
            "|21|", 
            "   |", 
            "   |", 
            "---|" 
        };
        private static string[] _leftCornerVertical = 
        { 
            "|21|", 
            "|   ", 
            "|   ", 
            "|---" 
        };

        #endregion

        //Additional properties
        private static int _cursorX;                    //Cursor X position
        private static int _cursorY;                    //Cursor Y position
        private static Direction _currentDirection;     //Direction the track is being drawn in, this changes in the corners
        private static Section _currentSection;         //Current section that is being drawn

        //Initalise the visualisation by setting relevant properties
        public static void Initialise(Race race)
        {
            Console.Clear();
            _cursorX = 10;
            _cursorY = 5;
            _currentDirection = Direction.East;     //A track always starts eastwards
        }

        //Draws track with the sections provided in the track variable
        public static void DrawTrack(Track track)
        {
            //Write track name
            Console.SetCursorPosition(0, 0);
            Console.Write($"Track name: {track.Name}");

            Console.SetCursorPosition(_cursorX, _cursorY); //Set cursor position to track draw start

            //Loop through sections
            foreach (Section section in track.Sections)
            {
                _currentSection = section;  //update section
                DrawSection(DecideSectionToDraw(section), section);
                if(section.SectionType == SectionTypes.LeftCorner || section.SectionType == SectionTypes.RightCorner)
                {
                    ChangeDirection(section.SectionType);
                }
                ChangeCursorPosition();                             //change cursor position
                Console.SetCursorPosition(_cursorX, _cursorY);      //Put cursor in place for new section
            }
        }

        //Decide what string to draw, horizontally or vertically, based on the current direction.
        public static string[] DecideSectionToDraw(Section section)
        {
            SectionTypes sectionType = section.SectionType;
            switch (sectionType)
            {
                case SectionTypes.StartGrid:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return _startHorizontal;
                    }
                    else
                    {
                        return _startVertical;
                    }
                case SectionTypes.Finish:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return _finishHorizontal;
                    }
                    else
                    {
                        return _finishVertical;
                    }
                case SectionTypes.Straight:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        return _straightHorizontal;
                    }
                    else
                    {
                        return _straightVertical;
                    }
                case SectionTypes.LeftCorner:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {
                        
                        return _leftCornerHorizontal;
                    }
                    else
                    {
             
                        return _leftCornerVertical;
                    }
                case SectionTypes.RightCorner:
                    if (_currentDirection == Direction.East || _currentDirection == Direction.West)
                    {  
                        return _rightCornerHorizontal;
                    }
                    else
                    {
                        return _rightCornerVertical;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, "Something went wrong with the sections! The section may be unsupported.");

            }
        }

        public static void DrawSection(string[] sectionLinesToDraw, Section currentSection)
        {
            int tempX = _cursorX;
            int tempY = _cursorY;
            bool reverse = ToReverseOrNotToReverse();

            if (reverse)
            {
                Array.Reverse(sectionLinesToDraw);
            }

            foreach (string line in sectionLinesToDraw)
            {
                Console.SetCursorPosition(tempX, tempY);
                if(reverse)
                {
                    Console.Write(ReverseString(line));
                } else
                {
                    Console.WriteLine(line);
                }
                tempY++;
            }

            if (reverse)
            {
                Array.Reverse(sectionLinesToDraw);
            }
        }

        //Reverses the input string by converting it to a char array, reversing it and returning the new string.
        public static string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            string newString = new string(charArray);
            return newString;
        }

        //Changes cursor position based on the current direction
        public static void ChangeCursorPosition()
        {
            switch (_currentDirection)
            {
                case Direction.North:
                    _cursorY -= 4;
                    break;
                case Direction.East:
                    _cursorX += 4;
                    break;
                case Direction.South:
                    _cursorY += 4;
                    break;
                case Direction.West:
                    _cursorX -= 4;
                    break;
            }
        }

        //Changes direction when there's a corner
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

        //If the direction is north or west, the sections should be reversed. 
        public static bool ToReverseOrNotToReverse()
        {
            if (_currentDirection == Direction.North || _currentDirection == Direction.West)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
