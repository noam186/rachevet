using System;

public class Engine
{
    public string LicenseNumber { get; set; }
    public int ManufactureYear { get; set; }

    public Engine(string licenseNumber, int manufactureYear)
    {
        LicenseNumber = licenseNumber;
        ManufactureYear = manufactureYear;
    }

    public Engine(Engine other)
    {
        LicenseNumber = other.LicenseNumber;
        ManufactureYear = other.ManufactureYear;
    }

    public void PrintDetails()
    {
        Console.WriteLine($"Engine: {LicenseNumber} (Year: {ManufactureYear})");
    }
}

public class Carriage
{
    public int SerialNumber { get; set; }
    public int PassengerCapacity { get; set; }

    public Carriage(int serialNumber, int passengerCapacity)
    {
        SerialNumber = serialNumber;
        PassengerCapacity = passengerCapacity;
    }

    public Carriage(Carriage other)
    {
        SerialNumber = other.SerialNumber;
        PassengerCapacity = other.PassengerCapacity;
    }

    public void PrintDetails()
    {
        Console.WriteLine($"   - Carriage #{SerialNumber}: up to {PassengerCapacity} passengers");
    }
}

public class Train
{
    private Engine engine;
    private Carriage[] carriages;
    private int carriageCount;

    public Engine Engine
    {
        get { return engine; }
        set { engine = new Engine(value); }
    }

    public int MaxCarriages
    {
        get { return carriages.Length; }
    }

    public int CarriageCount
    {
        get { return carriageCount; }
    }

    public Train(Engine engine, int maxCarriages)
    {
        Engine = engine;
        carriages = new Carriage[maxCarriages];
        carriageCount = 0;
    }

    public void ChangeEngine(Engine newEngine)
    {
        Engine = newEngine;
    }

    public bool AddCarriage(Carriage carriage)
    {
        if (carriageCount >= carriages.Length)
        {
            return false;
        }

        carriages[carriageCount] = new Carriage(carriage);
        carriageCount++;
        return true;
    }

    public int GetTotalPassengers()
    {
        int total = 0;
        for (int i = 0; i < carriageCount; i++)
        {
            total += carriages[i].PassengerCapacity;
        }
        return total;
    }

    public double GetAveragePassengersPerCarriage()
    {
        if (carriageCount == 0)
        {
            return 0;
        }

        return (double)GetTotalPassengers() / carriageCount;
    }

    public void PrintDetails()
    {
        Console.WriteLine("\nTrain details:");
        engine.PrintDetails();
        Console.WriteLine($"Carriages: {carriageCount} / {carriages.Length}");

        for (int i = 0; i < carriageCount; i++)
        {
            carriages[i].PrintDetails();
        }

        Console.WriteLine($"Total passengers: {GetTotalPassengers()}");
        Console.WriteLine($"Average passengers per carriage: {GetAveragePassengersPerCarriage():F2}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Let's set up the train.\n");

        Console.Write("Engine license number: ");
        string license = Console.ReadLine() ?? "";

        Console.Write("Engine manufacture year: ");
        int year = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Maximum number of carriages: ");
        int maxCarriages = int.Parse(Console.ReadLine() ?? "0");

        Train train = new Train(new Engine(license, year), maxCarriages);

        Console.Write("\nHow many carriages do you want to add right now? ");
        int toAdd = int.Parse(Console.ReadLine() ?? "0");

        for (int i = 0; i < toAdd; i++)
        {
            Console.WriteLine($"\nCarriage {i + 1}:");
            Console.Write("  Serial number: ");
            int serial = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("  Passenger capacity: ");
            int capacity = int.Parse(Console.ReadLine() ?? "0");

            bool added = train.AddCarriage(new Carriage(serial, capacity));
            if (!added)
            {
                Console.WriteLine("  Train is already at full capacity, cannot add more carriages.");
                break;
            }
        }

        train.PrintDetails();

        Console.Write("\nDo you want to replace the engine? (y/n): ");
        string answer = (Console.ReadLine() ?? "").Trim().ToLower();

        if (answer == "y")
        {
            Console.Write("New engine license number: ");
            string newLicense = Console.ReadLine() ?? "";

            Console.Write("New engine manufacture year: ");
            int newYear = int.Parse(Console.ReadLine() ?? "0");

            train.ChangeEngine(new Engine(newLicense, newYear));
            Console.WriteLine("\nUpdated train after replacing engine:");
            train.PrintDetails();
        }
    }
}