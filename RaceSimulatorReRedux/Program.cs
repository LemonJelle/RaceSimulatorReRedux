using Controller;
using RaceSimulatorReRedux;

Data.Initalise();
Data.NextRace();

////Display track name for level 2-6
//Console.WriteLine("Current track name:");
//Console.WriteLine(Data.CurrentRace.Track.Name);

//Call Visualisation.DrawTrack() to draw track for level 4-2
Visualisation.Initialise(Data.CurrentRace);
Visualisation.DrawTrack(Data.CurrentRace.Track);

for (; ; )
{
    Thread.Sleep(100);
}
