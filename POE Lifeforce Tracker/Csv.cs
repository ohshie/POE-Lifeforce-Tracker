using System.Globalization;
using System.IO;
using System.Reflection;
using CsvHelper;

namespace POE_LifeForce_Tracker;


public class Csv
{
    private static string PathToData()
    {
        string currentDirectory = Assembly.GetExecutingAssembly().Location;
        string filePath = Path.Combine(Path.GetDirectoryName(currentDirectory), "data.csv");
        return filePath;
    }
    
    public static void Write(int[,] entryArray)
    {
        
        using (var writer = new StreamWriter(PathToData()))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            for (int i = 0; i < Program.Header.Length; i++) csv.WriteField(Program.Header[i]);

            csv.NextRecord();
            for (int y = 0; y < entryArray.GetLength(0); y++)
            {
                for (int x = 0; x < entryArray.GetLength(1); x++)
                {
                    csv.WriteField(entryArray[y, x]);
                }
                csv.NextRecord();
            }
        }

        Console.WriteLine("Data written\n" +
                          "Press any key to go back to menu");
    }

    public static void Read(ref int[,] entryArray)
    {
        int[,] tempArray;
        using (var reader = new StreamReader(PathToData()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            int lengthOfTempArray = -1; // -1 due to header
            while (csv.Read())
            {
                lengthOfTempArray++;
            }
            tempArray = new int[lengthOfTempArray, Program.Header.GetLength(0)];
        }

        using (var reader = new StreamReader(PathToData()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            for (int y = 0; y < tempArray.GetLength(0); y++)
            {
                csv.Read();
                for (int x = 0; x < tempArray.GetLength(1); x++)
                {
                    tempArray[y, x] = int.Parse(csv.GetField(x));
                }
            }
        }
        entryArray = tempArray;
        Console.WriteLine("Data read\n" +
                          "Press any key to go back to menu");
    }
}