using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("Test Track", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
            });
            _competition.Tracks.Enqueue(track);
            Track result = _competition.NextTrack();
            Assert.That(result, Is.EqualTo(track));
        }
    }
}
