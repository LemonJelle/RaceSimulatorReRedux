using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public DateTime StartTime { get; set; }
        public List<IParticipant> Participants { get; set; }

        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {

            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();

            //First check if amount of participants doesn't exceed the limit
            CheckAmountOfParticipants();

            //Place participants on start and randomize equipment
            GiveParticipantsStartPosition();
            RandomizeEquipment();

        }

        public SectionData GetSectionData(Section section)
        {
            //Return the sectiondata for the given section
            try
            {
                return _positions[section];
            }
            catch (KeyNotFoundException) //If not found, create default new section data for the given section
            {
                SectionData newSectionData = new SectionData();
                _positions.Add(section, newSectionData);
                return _positions[section];
            }
        }

        //Participant number can't be over 6, the race would be too messy
        public void CheckAmountOfParticipants()
        {
            if (Participants.Count > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(Participants), "The maximum number of participants in a race is six.");
            }
        }

        //Randomnise the quality and performance of participants
        public void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
        }

        public void GiveParticipantsStartPosition()
        {
            //Make stack of start grid sections
            Stack<Section> startGrids = new Stack<Section>();

            //Search for startgrid sections in the track and add them to stack
            foreach (Section section in Track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            //Get number of participants
            int participants = Participants.Count;

            //Keep track of participants already placed, a counter of sorts
            int participantsAlreadyPlaced = 0;

            //Place participants as long as there is still a startgrid left
            //Check how many participants are left
            while (startGrids.Count > 0)
            {
                //Get section from stack
                Section section = startGrids.Pop();
                //Get data of each section
                SectionData sectionData = GetSectionData(section);
                //If there is still two or more participants left to place, take up two positions
                if (participants - participantsAlreadyPlaced > 1)
                {
                    //Left
                    sectionData.Left = Participants[participantsAlreadyPlaced];
                    participantsAlreadyPlaced += 1;
                    //Right
                    sectionData.Right = Participants[participantsAlreadyPlaced];
                    participantsAlreadyPlaced += 1;
                    _positions[section] = sectionData;
                }

                //if there is still one participant left to place, take up the left position 
                else if (participants - participants == 1)
                {
                    sectionData.Left = Participants[participants];
                    participantsAlreadyPlaced += 1;
                    _positions[section] = sectionData;
                }
            }
        }
    }
}
