using System;
using System.Collections.Generic;
using System.Linq;

class Car
{
    public string DriverName { get; set; }
    public LapTimer LapTimer { get; private set; }

    public Car(string driverName)
    {
        DriverName = driverName;
        LapTimer = new LapTimer(this);
    }
}

class Track
{
    public string Name { get; set; }
    public double LengthKM { get; set; }
    public int TotalLaps { get; set; }

    public Track(string name, double lengthKm, int totalLaps)
    {
        Name = name;
        LengthKM = lengthKm;
        TotalLaps = totalLaps;
    }
}

class LapTimer
{
    private Car _car;
    private List<TimeSpan> _lapTimes = new List<TimeSpan>();

    public IReadOnlyList<TimeSpan> LapTimes => _lapTimes.AsReadOnly();

    public LapTimer(Car car)
    {
        _car = car;
    }

    public void AddLapTime(TimeSpan time)
    {
        _lapTimes.Add(time);
    }

    public TimeSpan GetTotalTime()
    {
        return new TimeSpan(_lapTimes.Sum(t => t.Ticks));
    }
}

class Race
{
    private Track _track;
    private List<Car> _cars;

    public Race (Track track, List<Car> cars)
    {
        _track = track;
        _cars = cars;
    }

    public void StartRace()
    {
        Random rng = new Random();

        foreach (var car in _cars)
        {
            for(int i = 0; i < _track.TotalLaps; i++)
            {
                int seconds = rng.Next(90, 121);
                TimeSpan lapTime = TimeSpan.FromSeconds(seconds);
                car.LapTimer.AddLapTime(lapTime);
                Console.WriteLine($"{car.DriverName} - Lap {i + 1}: {lapTime}");
            }
            Console.WriteLine($"{car.DriverName} - Total Time: {car.LapTimer.GetTotalTime()}\n");
        }
    }

    public void ShowPodium()
    {
        var podium = _cars.OrderBy(c => c.LapTimer.GetTotalTime()).Take(3).ToList();

        Console.WriteLine("\n Podium: ");
        for (int i = 0; i < podium.Count; i++)
        {
            var car = podium[i];
            Console.WriteLine($"{i + 1}, {car.DriverName} - {car.LapTimer.GetTotalTime()}");
        }
    }
}

class Program
{
    static void Main()
    {
        var track = new Track("Grand Prix Circuit", 5.0, 3);

        var cars = new List<Car>
        {
            new Car("Lewis Hamilton"),
            new Car("Max Verstappen"),
            new Car("Charles Leclerc"),
            new Car("Lando Norris")
        };

        var race = new Race(track, cars);

        race.StartRace();
        race.ShowPodium();
    }
}
