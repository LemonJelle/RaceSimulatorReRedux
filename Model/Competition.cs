using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        //Go to next track, if track queue is empty return null
        public Track NextTrack()
        {
            if (Tracks.Count == 0) 
            {
                return null;
            } 
            else
            {
                return Tracks.Dequeue();
            }
        }
    }
}
