namespace DirectSort;

internal class Generator
{
    private readonly Random _random = new Random();
    public Generator() { }

    public int Generate(double mb, string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        int count = 0;
        using (var writer = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
        {
            for (int i = 0; i < Math.Pow(2, 18) * mb; i++)
            {
                writer.Write(_random.Next(0, 1_000_000));
                count++;
            }
        }
        Console.WriteLine(fileName + " Filled");
        return count;
    }

    public void GenerateLines(int linesCount)
    {
        string fileName = $"L{linesCount}.dat";
        using var writer = new StreamWriter(fileName);
        for (int i = 0; i < linesCount; i++)
        {
            writer.WriteLine(_random.Next(0, 1000_000_000));
        }
    }
    
    public static int[] GetArrayPart(int start, int size, string fileName)
    {
        var array = new int[size];
        using var sr = new BinaryReader(File.Open(fileName, FileMode.OpenOrCreate));
        for (int i = 0; i < start; i++)
        {
            sr.ReadInt32();
        }

        for (int i = 0; i < size; i++)
        {
            array[i] = sr.ReadInt32();
        }

        return array;
    }

    public static void ShowContent(string fileName, int size)
    {
        var array = GetArrayPart(0, size, fileName);
        Console.Write("[ ");
        foreach (var i in array)
        {
            Console.Write(i + ", ");
        }
        Console.WriteLine("]");
    }
}
    


