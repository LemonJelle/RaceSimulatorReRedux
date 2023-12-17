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


        public static void Initalise()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        //Go to the next race if there is a next track
        public static void NextRace()
        {
            if (Competition.NextTrack != null)
            {
                CurrentRace = new Race(Competition.NextTrack(), Competition.Participants);
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

        public static void AddTracks()
        {
            //Complicated track
            Competition.Tracks.Enqueue(new Track("Barcelona", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,

            }));
            //Simple circle track
            Competition.Tracks.Enqueue(new Track("Indianapolis Motor Speedway", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner
            }));
        }
    }
}
