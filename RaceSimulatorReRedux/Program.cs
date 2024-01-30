using Controller;
using RaceSimulatorReRedux;

namespace RaceSimulatorReRedux;

public class Program
{
    static void Main(string[] args)
    {
        Data.Initalise();
        Console.Clear();
        Console.SetBufferSize(Console.WindowLeft + Console.WindowWidth, Console.WindowTop + Console.WindowHeight);
        Console.SetWindowSize(100, 80);

        ////Display track name for level 2-6
        //Console.WriteLine("Current track name:");
        //Console.WriteLine(Data.CurrentRace.Track.Name);

        //Call Visualisation.DrawTrack() to draw track for level 4-2
        //Visualisation.Initialise(Data.CurrentRace);
        //Visualisation.DrawTrack(Data.CurrentRace.Track);

        Data.CompetitionFinished += OnCompetitionFinished;
        //Call the next race event to properly handle the races for level 5-6
        Data.NextRaceEvent += Visualisation.OnNextRaceEvent;
        Data.NextRace();
        for (; ; )
        {
            Thread.Sleep(100);
        }
    }

    private static void OnCompetitionFinished(object sender, NextRaceEventArgs e)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("All races are finished!");
    }
}



