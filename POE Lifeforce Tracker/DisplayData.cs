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

        Gained(entryArray);

        Console.WriteLine("Press any key to go back to the menu");
        Console.ReadLine();
    }

    private static void Total(int[,] entryArray)
    {
        Console.WriteLine();
        Console.WriteLine("You did " + entryArray.GetLength(0) + " maps\n");

        for (var i = 1; i <= 3; i++)
            Console.WriteLine("You gained a total of " + entryArray[entryArray.GetLength(0) - 1, i]
                                                       + " " + Program.Header[i]);
    }

    private static void Average(int[,] entryArray)
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

    private static void Gained(int[,] entryArray)
    {
        Console.WriteLine();
        Console.WriteLine("Amount of lifeforce gained in each Map");
        int[,] gainedArray = new int[entryArray.GetLength(0), entryArray.GetLength(1)];
        
        for (int y = 0; y < entryArray.GetLength(0); y++)
        {
            gainedArray[y, 0] = entryArray[y, 0];
        }

        for (int y = 1; y < entryArray.GetLength(0); y++)
        {
            for (int x = 1; x < entryArray.GetLength(1); x++)
            {
                gainedArray[y, x] = entryArray[y, x] - entryArray[y-1, x];
            }
        }
        for (int y = 0; y < entryArray.GetLength(0); y++)
        {
            for (int x = 0; x < entryArray.GetLength(1); x++)
            {
                Console.Write(gainedArray[y,x]+"\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    
}