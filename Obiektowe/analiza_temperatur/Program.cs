using System;
using System.IO;
using System.Linq;

class Temperature
{   
    private string cityName;
    private double[] temperatures;

    //konstruktory
    public Temperature()
    {
        cityName = "Białystok";
        temperatures = new double[7];
    }

    public Temperature(string city)
    {
        cityName = city;
        temperatures = new double[7];
    }

    public Temperature(string city, double[] temps)
    {
        cityName = city;
        temperatures = new double[7];  // Initialize with zeros first
        if (temps != null && temps.Length > 0)
        {
            Array.Copy(temps, temperatures, Math.Min(temps.Length, 7));
        }
    }
    

    //Metody konwersji temperatur
    static double KelvinToCelsius(double kelvin)
    {
        return kelvin - 273.15;
    }

    static double KelvinToFahrenheit(double kelvin)
    {
        return (kelvin - 273.15) * 9/5 + 32;
    }
    

    //Metody odczytu
    public double GetKelvin(int day)
    {
        if (day >= 0 && day < temperatures.Length)
            return temperatures[day];
        return 0;
    }
    public double GetCelsius(int day)
    {
        return KelvinToCelsius(GetKelvin(day));
    }
    public double GetFahrenheit(int day)
    {
        return KelvinToFahrenheit(GetKelvin(day));
    }

    //Metody ustawiania temperatury
    public void SetKelvin(int day, double kelvin)
    {
        if (day >= 0 && day < temperatures.Length)
            temperatures[day] = kelvin;
    }
    public void SetCelsius(int day, double celsius)
    {
        SetKelvin(day, celsius + 273.15);
    }
    public void SetFahrenheit(int day, double fahrenheit)
    {
        SetKelvin(day, (fahrenheit - 32) * 5/9 + 273.15);
    }

    //metody analizy temperatur
    public double GetAverageTemperatureKelvin()
    {
        return temperatures.Average();
    }
    public double GetMinTemperatureKelvin()
    {
        return temperatures.Min();
    }
    public double GetMaxTemperatureKelvin()
    {
        return temperatures.Max();
    }

    public string GetCityName()
    {
        return cityName;
    }

}

class Program
{
    static void Main(string[] args)
    {
    try
    {
        string[] lines = File.ReadAllLines("cities.txt");
        int cityCount = int.Parse(lines[0]);
        List<Temperature> cities = new List<Temperature>();

        for (int i = 1; i <= cityCount; i++)
        {
        string[] data = lines[i].Split(',');
        string cityName = data[0];
        double[] temps = new double[7];
    
        // Convert only the temperature values that exist
        for (int j = 1; j < data.Length && j <= 7; j++)
        {
            if (double.TryParse(data[j], out double temp))
            {
                temps[j-1] = temp;
            }
        }
    
    cities.Add(new Temperature(cityName, temps));
}

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("\n=== Temperature Management System ===");
            Console.WriteLine("1. Show all cities temperatures");
            Console.WriteLine("2. Show temperature for a single city");
            Console.WriteLine("3. Change temperature for a city");
            Console.WriteLine("4. Add new city");
            Console.WriteLine("5. Save data to file");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowAllCities(cities);
                    break;
                case "2":
                    ShowSingleCity(cities);
                    break;
                case "3":
                    ChangeTemperature(cities);
                    break;
                case "4":
                    AddNewCity(cities);
                    break;
                case "5":
                    SaveToFile(cities);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void ShowAllCities(List<Temperature> cities)
{
    foreach (var city in cities)
    {
        Console.WriteLine($"\nCity: {city.GetCityName()}");
        Console.WriteLine($"Average temperature: {city.GetAverageTemperatureKelvin():F2}K");
        Console.WriteLine($"Min temperature: {city.GetMinTemperatureKelvin():F2}K");
        Console.WriteLine($"Max temperature: {city.GetMaxTemperatureKelvin():F2}K");
    }
}

static void ShowSingleCity(List<Temperature> cities)
{
    Console.WriteLine("\nAvailable cities:");
    for (int i = 0; i < cities.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {cities[i].GetCityName()}");
    }

    Console.Write("\nSelect city number: ");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= cities.Count)
    {
        var city = cities[choice - 1];
        Console.WriteLine($"\nCity: {city.GetCityName()}");
        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"Day {i + 1}: {city.GetKelvin(i):F2}K ({city.GetCelsius(i):F2}°C)");
        }
    }
    else
    {
        Console.WriteLine("Invalid city number!");
    }
}

static void ChangeTemperature(List<Temperature> cities)
{
    Console.WriteLine("\nAvailable cities:");
    for (int i = 0; i < cities.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {cities[i].GetCityName()}");
    }

    Console.Write("Select city number: ");
    if (int.TryParse(Console.ReadLine(), out int cityChoice) && cityChoice >= 1 && cityChoice <= cities.Count)
    {
        var city = cities[cityChoice - 1];
        Console.Write("Enter day (1-7): ");
        if (int.TryParse(Console.ReadLine(), out int day) && day >= 1 && day <= 7)
        {
            Console.WriteLine("Select unit:");
            Console.WriteLine("1. Kelvin");
            Console.WriteLine("2. Celsius");
            Console.WriteLine("3. Fahrenheit");
            Console.Write("Choice: ");
            string unit = Console.ReadLine() ?? string.Empty;
            
            Console.Write("Enter temperature value: ");
            if (double.TryParse(Console.ReadLine(), out double temp))
            {
                switch (unit)
                {
                    case "1":
                        city.SetKelvin(day - 1, temp);
                        break;
                    case "2":
                        city.SetCelsius(day - 1, temp);
                        break;
                    case "3":
                        city.SetFahrenheit(day - 1, temp);
                        break;
                    default:
                        Console.WriteLine("Invalid unit choice!");
                        return;
                }
                Console.WriteLine("Temperature updated successfully!");
            }
        }
    }
}

static void AddNewCity(List<Temperature> cities)
{
    Console.Write("Enter city name: ");
    string name = Console.ReadLine() ?? string.Empty;
    double[] temps = new double[7];

    for (int i = 0; i < 7; i++)
    {
        Console.Write($"Enter temperature for day {i + 1} (in Kelvin): ");
        if (double.TryParse(Console.ReadLine(), out double temp))
        {
            temps[i] = temp;
        }
    }

    cities.Add(new Temperature(name, temps));
    Console.WriteLine("City added successfully!");
}

static void SaveToFile(List<Temperature> cities)
{
    List<string> lines = new List<string>();
    lines.Add(cities.Count.ToString());

    foreach (var city in cities)
    {
        string line = city.GetCityName();
        for (int i = 0; i < 7; i++)
        {
            line += "," + city.GetKelvin(i).ToString();
        }
        lines.Add(line);
    }

    File.WriteAllLines("cities.txt", lines);
    Console.WriteLine("Data saved successfully!");
}
}



