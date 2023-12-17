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

            //First check if amount of participants doesn't exceed the limit
            CheckAmountOfParticipants();
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
    }
}
