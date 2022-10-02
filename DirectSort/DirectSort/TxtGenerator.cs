namespace DirectSort;

internal class Generator
{
    private readonly Random _random = new Random();
    public Generator() { }

    public int Generate(double mb, string fileName)
    {
        int count = 0;
        using (var writer = new StreamWriter(fileName))
        {
            for (int i = 0; i < Math.Pow(2, 18) * mb; i++)
            {
                writer.WriteLine(_random.Next(0, 1_000_000_000));
                count++;
            }
        }
        Console.WriteLine(fileName + " Filled");
        return count;
    }

    public void GenerateLines(int linesCount)
    {
        string fileName = $"L{linesCount}.txt";
        using var writer = new StreamWriter(fileName);
        for (int i = 0; i < linesCount; i++)
        {
            writer.WriteLine(_random.Next(0, 1000_000_000));
        }
    }
}
