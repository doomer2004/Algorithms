namespace DirectSort;
internal class DirectSort
{
    public DirectSort() { }

    public void Sort(int size,int shareSize, string fileA)
    {
        string outputFile = "Output.dat";
        SortParts(fileA, outputFile, size, shareSize);
        RecSorting(outputFile, size,shareSize);
    }

    private static void RecSorting(string fileA,int size, int sequenceSize)
    {
        while (size > sequenceSize)
        {
            string fileB = "helpB.dat";
            string fileC = "helpC.dat";

            SplitByGroups(fileA, sequenceSize, fileB, fileC);
            MergeParts(fileA, sequenceSize, fileB, fileC);
            sequenceSize *= 2;
        }
    }

    private static void MergeParts(string fileA, int groupCount, string fileB, string fileC)
    {
        if (File.Exists(fileA))
        {
            File.Delete(fileA);
        }
        using var aWriter = new BinaryWriter(File.Open(fileA, FileMode.OpenOrCreate));
        using var bReader = new BinaryReader(File.Open(fileB, FileMode.OpenOrCreate));
        using var cReader = new BinaryReader(File.Open(fileC, FileMode.OpenOrCreate));
        var firstNum = GetInt(bReader);
        var secondNum = GetInt(cReader);
        while (firstNum != int.MaxValue || secondNum != int.MaxValue)
        {
            bool firstFinished = firstNum == int.MaxValue;
            bool secondFinished = secondNum == int.MaxValue;
            int firstCount = 0;
            int seconCount = 0;
            for (int i = 0; i < groupCount * 2 && (!firstFinished || !secondFinished) ; i++)
            {
                if (firstFinished)
                {
                    while (!secondFinished)
                    {
                        aWriter.Write(secondNum);
                        secondNum = GetInt(cReader);
                        seconCount++;
                        if (seconCount == groupCount || secondNum==int.MaxValue)
                        {
                            secondFinished = true;
                        }
                    }
                }
                else if(secondFinished)
                {
                    while (!firstFinished)
                    {
                        aWriter.Write(firstNum);
                        firstNum = GetInt(bReader);
                        firstCount++;
                        if (firstCount == groupCount || firstNum==int.MaxValue)
                        {
                            firstFinished = true;
                        }
                    }
                }
                else
                {
                    if (firstNum < secondNum)
                    {
                        aWriter.Write(firstNum);
                        firstNum = GetInt(bReader);
                        firstCount++;
                        if (firstCount == groupCount || firstNum==int.MaxValue)
                        {
                            firstFinished = true;
                        }
                    }
                    else
                    {
                        aWriter.Write(secondNum);
                        secondNum = GetInt(cReader);
                        seconCount++;
                        if (seconCount == groupCount || secondNum==int.MaxValue)
                        {
                            secondFinished = true;
                        }
                    }
                }
            }
        }
    }

    private static int GetInt(BinaryReader reader)
    {
        try
        {
            return reader.ReadInt32();
        }
        catch (EndOfStreamException)
        {
            return int.MaxValue;
        }
    }
    private static void SplitByGroups(string fileA, int groupCount, string fileB, string fileC)
    {
        if (File.Exists(fileB))
        {
            File.Delete(fileB);
        }
        if (File.Exists(fileC))
        {
            File.Delete(fileC);
        }
        using var aReader = new BinaryReader(File.Open(fileA, FileMode.OpenOrCreate));
        using var bWriter = new BinaryWriter(File.Open(fileB, FileMode.OpenOrCreate));
        using var cWriter = new BinaryWriter(File.Open(fileC, FileMode.OpenOrCreate));
    
        bool isOdd = true;
        int currentNumber = GetInt(aReader);
        while (currentNumber != Int32.MaxValue)
        {
            for (int i = 0; i < groupCount && currentNumber != Int32.MaxValue ; i++)
            {
                
                if (isOdd)
                {
                    bWriter.Write(currentNumber);
                }
                else
                {
                    cWriter.Write(currentNumber);
                }

                currentNumber = GetInt(aReader);
            }

            isOdd = !isOdd;
        }
    }
    
    public static void SortParts(string fileName, string outputFileName, int size, int shareSize)
    {
        if (File.Exists(outputFileName))
        {
            File.Delete(outputFileName);
        }
        
        var array = new int[shareSize];
        using var reader = new BinaryReader(File.Open(fileName, FileMode.Open));
        using var writer = new BinaryWriter(File.Open(outputFileName, FileMode.OpenOrCreate));
        for (int i = 0; i < size / shareSize; i++)
        {
            for (int j = 0; j < shareSize; j++)
            {
                array[j] = reader.ReadInt32();
            }
            Array.Sort(array);
            for (int j = 0; j < shareSize; j++)
            {
                writer.Write(array[j]);
            }
        }
    }

}