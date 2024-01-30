using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;

        public static event EventHandler<NextRaceEventArgs> CompetitionFinished;


        public static void Initalise()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        //Go to the next race if there is a next track, if there is none 
        public static void NextRace()
        {
            CurrentRace?.CleanUp();
            Track nextTrack = Competition.NextTrack();
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
                CurrentRace.RaceIsOver += OnRaceIsOver;
                NextRaceEvent?.Invoke(null, new NextRaceEventArgs()
                {
                    NextEventRace = CurrentRace
                });
            }
            else
            {
                CompetitionFinished?.Invoke(null, new NextRaceEventArgs()
                {
                    NextEventRace = null
                });

            }
        }

        //Drivers are taken from real life Formula 2
        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Dennis Hauger", 0, new Car(20, 20, 20, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Jehan Daruvala", 0, new Car(17, 20, 18, false), TeamColors.Green));
            Competition.Participants.Add(new Driver("Jak Crawford", 0, new Car(16, 20, 18, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Enzo Fittipaldi", 0, new Car(19, 20, 14, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Arthur Leclerc", 0, new Car(16, 20, 17, false), TeamColors.Grey));
            Competition.Participants.Add(new Driver("Frederik Vesti", 0, new Car(20, 20, 17, false), TeamColors.Red));
        }

        //Add tracks. Tracks always start on the top. 
        public static void AddTracks()
        {

            //Very complicated track
            Competition.Tracks.Enqueue(new Track("Spa", new[]
            {
                SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid,
                SectionTypes.Finish, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight,
                SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner,
                SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight,
                SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner,
                SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight,
                SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight
            }));
            //Complicated track
            Competition.Tracks.Enqueue(new Track("Barcelona", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,

            }));
            //Simple circle track
            Competition.Tracks.Enqueue(new Track("Indianapolis Motor Speedway", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            }));
           
        }

        //Executes a next race
        private static void OnRaceIsOver(object sender, EventArgs eventArgs)
        {
            NextRace();
        }
    }
}
