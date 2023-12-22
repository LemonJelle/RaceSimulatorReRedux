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

        private System.Timers.Timer _timer { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private int _sectionLength = 200;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            //Initialise properties
            Track = track;
            Participants = participants;

            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _timer = new System.Timers.Timer(500);

            //Add event to elapsed timer property
            _timer.Elapsed += OnTimedEvent;
            StartTimer();
            

            //First check if amount of participants doesn't exceed the limit
            CheckAmountOfParticipants();

            //Place participants on start and randomize equipment
            GiveParticipantsStartPosition();
            RandomizeEquipment();

        }

        //Advance participants every time the timer goes off
        public void AdvanceParticipants()
        {
            //I'm using a stack here to put all the sections in, it's more logical to me, and easier to iterate through than a linkedlist
            //This is the reverse of the track linkedlist, as stacks are LIFO, so the first section is at the bottom of the stack.

            //Fill stack
            Stack<Section> stackOfSections = new Stack<Section>();
            foreach (Section section in Track.Sections)
            {
                stackOfSections.Push(section);
            }

            //Declare current section and previous section
            Section currentSection; 
            Section nextSection = Track.Sections.First(); //Start at first section of track, the stack is reversed so this is the next section
            while (stackOfSections.Count > 0) 
            {
                currentSection = stackOfSections.Pop();

                AdvanceTwoParticipants(currentSection, nextSection);

                nextSection = currentSection;
            }
        }

        public void AdvanceTwoParticipants(Section currentSection, Section nextSection)
        {
            //Set section data
            SectionData currentSectionData = GetSectionData(currentSection);
            SectionData nextSectionData = GetSectionData(nextSection);

            //Left participant
            //check if current section data is null, this shouldn't be the case if the participant is in it
            //if it is occupied, add distance
            if (currentSectionData.Left != null)
            {
                currentSectionData.DistanceLeft += CalculateSpeed(currentSectionData.Left.Equipment.Performance, currentSectionData.Left.Equipment.Speed);

                //If distance exceeds 100, check if next section left is null, else move to right
                if (nextSectionData.Left == null)
                {
                    //Transfer participant to next section
                    nextSectionData.Left = currentSectionData.Left;

                    //Set current section data to null
                    currentSectionData.Left = null;

                    //Set distance back to zero
                    currentSectionData.DistanceLeft = 0;

                }
                else if (nextSectionData.Right == null)
                {
                    //Transfer participant to next section but on the right
                    nextSectionData.Right = currentSectionData.Left;

                    //Set current section data to null
                    currentSectionData.Left = null;

                    //Set distance back to zero
                    currentSectionData.DistanceLeft = 0;
                }
                else
                {
                    //Participant can't advance, set distanceleft to 195 so they're at the end
                    currentSectionData.DistanceLeft = 195;
                }


            }

            //Right participant
            //Set section data
            currentSectionData = GetSectionData(currentSection);
            nextSectionData = GetSectionData(nextSection);

            //Right participant
            //check if current section data is null, this shouldn't be the case if the participant is in it
            //if it is occupied, add distance
            if (currentSectionData.Right != null)
            {
                currentSectionData.DistanceRight += CalculateSpeed(currentSectionData.Right.Equipment.Performance, currentSectionData.Right.Equipment.Speed);

                //If distance exceeds 100, check if next section right is null, else move to left
                if (nextSectionData.Right == null)
                {
                    //Transfer participant to next section
                    nextSectionData.Right = currentSectionData.Right;

                    //Set current section data to null
                    currentSectionData.Right = null;

                    //Set distance back to zero
                    currentSectionData.DistanceRight = 0;

                }
                else if (nextSectionData.Left == null)
                {
                    //Transfer participant to next section but on the right
                    nextSectionData.Left = currentSectionData.Right;

                    //Set current section data to null
                    currentSectionData.Right = null;

                    //Set distance back to zero
                    currentSectionData.DistanceRight = 0;
                }
                else
                {
                    //Participant can't advance, set distanceright to 195 so they're at the end
                    currentSectionData.DistanceRight = 195;
                }

            }
        }
        public void FillPositions()
        {

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
            int participantsNumber = Participants.Count;

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
                if (participantsNumber - participantsAlreadyPlaced > 1)
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
                else if (participantsNumber - participantsNumber == 1)
                {
                    sectionData.Left = Participants[participantsNumber];
                    participantsAlreadyPlaced += 1;
                    _positions[section] = sectionData;
                }
            }
        }

        //Event attached to timer, this moves the participants
        public void OnTimedEvent(object sender, EventArgs eea)
        {
            //Move participants
            AdvanceParticipants();

            //Invoke DriversChanged event on this track
            DriversChanged.Invoke(this, new DriversChangedEventArgs()
            {
                EventTrack = Track
            });
            
        }

        //Enable, set autoreset and start timer
        public void StartTimer()
        {
            _timer.Enabled = true;
            _timer.AutoReset = true;
            _timer.Start();
        }

        public int CalculateSpeed(int performance, int speed) 
        {
            return performance * speed;  
        }
    }
}
