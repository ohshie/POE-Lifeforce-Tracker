using CsvHelper;
using POE_Lifeforce_Tracker;
using POE_Lifeforce_Tracker.PoeNinjaDataParser;

namespace POE_LifeForce_Tracker;

internal class Program
{
    // this may be redundant, but i dont know how to go without it
    public static string[] Header = { "map", "Purple", "Yellow", "Blue" };

    // method collects lifeforce and calls checkcontinue method
    private static void UserInputData(ref int[,] entryArray)
    {
        int cycle = entryArray.GetLength(0);

        entryArray[entryArray.GetLength(0) - 1, 0] = cycle;

        Console.WriteLine("Results after " + cycle + " maps");

        for (int x = 1; x < Header.Length; x++)
        {
            Console.WriteLine("Enter amount of " + Header[x] + " Lifeforce (or type same to copy prev data):");
            string userInput = Console.ReadLine();
            if (userInput.ToLowerInvariant() == "same")
                entryArray[cycle - 1, x] = entryArray[cycle - 2, x];
            else
                entryArray[cycle - 1, x] = int.Parse(userInput);
        }
        DataType dataType = DataType.temp;
        Csv.Write(entryArray, dataType);
        Console.Clear();

    }
    private static void ArrayLenghtIncrease(ref int[,] entryArray)
    {
        int[,] tempArray = new int[entryArray.GetLength(0) + 1, entryArray.GetLength(1)];

        for (var y = 0; y < entryArray.GetLength(0); y++)
        for (var x = 0; x < entryArray.GetLength(1); x++)
            tempArray[y, x] = entryArray[y, x];
        entryArray = tempArray;
    }

    public static void Main(string[] args)
    {
        int[,] mainDataArray = new int[1, Header.Length];

        // greetings menu
        while (true)
        {
            Console.WriteLine("PoE Lifeforce counter App (extremely scuffed).\n" +
                              "Idea is to enter amount of LF you have in your inventory after each map.\n" +
                              "Lets start? (enter menu number)\n" +
                              "1. Start mapping\n" +
                              "2. Read previous data from data.csv\n" +
                              "3. Grab data from Poe Ninja\n"+
                              "4. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                {
                    Console.Clear();
                    UserInputData(ref mainDataArray);
                    break;
                }
                case "2":
                {
                    Console.Clear();
                    Csv.Read(ref mainDataArray);
                    break;
                }
                case "3":
                {
                    Console.Clear();
                    PoeNinjaParser.GetDataFromPoeNinja(PoeDataTypes.Currency, "currency");
                    break;
                    
                }
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please choose.");
                    continue;
            }
            
            break;
        }

        // main cycle 
        while (true)
        {
            Console.WriteLine("You've done " + mainDataArray.GetLength(0) + " map(s)\n" +
                              "What now\n" +
                              "1. Next map\n" +
                              "2. Show data\n" +
                              "3. Save data to csv\n" +
                              "4. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                {
                    ArrayLenghtIncrease(ref mainDataArray);
                    Console.Clear();
                    UserInputData(ref mainDataArray);
                    break;
                }
                case "2":
                {
                    Console.Clear();
                    DisplayData.DataWriteUp(mainDataArray);
                    break;
                }
                case "3":
                {
                    DataType dataType = DataType.perm;
                    Console.Clear();
                    Csv.Write(mainDataArray, dataType);
                    break;
                }
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select 1, 2, 3 or 4");
                    continue;
            }
        }
    }
}