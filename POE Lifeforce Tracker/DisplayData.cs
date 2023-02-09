using POE_LifeForce_Tracker;

namespace POE_Lifeforce_Tracker;

public class DisplayData
{
    public static void DataWriteUp(int[,] entryArray)
    {
        Console.Clear();

        Console.WriteLine("Here is some data for your run(s):");

        Total(entryArray);

        Average(entryArray);

        Console.WriteLine("Press any key to go back to the menu");
        Console.ReadLine();
    }

    public static void Total(int[,] entryArray)
    {
        Console.WriteLine();
        Console.WriteLine("You did " + entryArray.GetLength(0) + " maps\n");

        for (var i = 1; i <= 3; i++)
            Console.WriteLine("You gained a total of " + entryArray[entryArray.GetLength(0) - 1, i]
                                                       + " " + Program.Header[i]);
    }

    public static void Average(int[,] entryArray)
    {
        var averageTotal = 0;
        Console.WriteLine();
        for (var i = 1; i <= 3; i++)
        {
            var average = entryArray[entryArray.GetLength(0) - 1, i] / entryArray[entryArray.GetLength(0) - 1, 0];
            Console.WriteLine("With " + average + " average amount of " + Program.Header[i]);
            averageTotal += average;
        }

        Console.WriteLine();
        Console.WriteLine("For a total average of " + averageTotal + " per map");
    }
    
}