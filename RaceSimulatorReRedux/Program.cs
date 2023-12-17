using Controller;

Data.Initalise();
Data.NextRace();

//Display track name for level 2-6
Console.WriteLine("Current track name:");
Console.WriteLine(Data.CurrentRace.Track.Name);

for (; ; )
{
    Thread.Sleep(100);
}
