using System.Globalization;
using System.IO;
using System.Reflection;
using CsvHelper;

namespace POE_LifeForce_Tracker;

public enum DataType
{
    temp,
    perm
}

public class Csv
{
    private static string PathToData(DataType dataType)
    {
        string currentDirectory = Assembly.GetExecutingAssembly().Location;
        string fileName = dataType == DataType.perm ? "data.csv" : "temp_data.csv";
        string filePath = Path.Combine(Path.GetDirectoryName(currentDirectory), fileName);
        return filePath;        
    }
    
    public static void Write(int[,] entryArray, DataType dataType)
    {
        
        using (var writer = new StreamWriter(PathToData(dataType)))
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
        if (dataType == DataType.perm)
        {
            Console.WriteLine("Data written\n" +
                          "Press any key to go back to menu");
        }
        
    }

    public static void Read(ref int[,] entryArray)
    {
        int[,] tempArray;
        using (var reader = new StreamReader(PathToData(DataType.perm)))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            int lengthOfTempArray = -1; // -1 due to header
            while (csv.Read())
            {
                lengthOfTempArray++;
            }
            tempArray = new int[lengthOfTempArray, Program.Header.GetLength(0)];
        }

        using (var reader = new StreamReader(PathToData(DataType.perm)))
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