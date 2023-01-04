namespace DirectSort;
internal class DirectSort
{
    public DirectSort() { }

    public void Sort(int size, string fileA)
    {
        RecSorting(fileA, size, 1);
    }

    private static void RecSorting(string fileA,int size, int sequenceSize)
    {
        while (size > sequenceSize)
        {
            string fileB = "helpB.txt";
            string fileC = "helpC.txt";

            SplitByGroups(fileA, sequenceSize, fileB, fileC);
            MergeParts(fileA, sequenceSize, fileB, fileC);
            sequenceSize *= 2;
        }
    }

    private static void MergeParts(string fileA, int groupCount, string fileB, string fileC)
    {
        using var aWriter = new StreamWriter(fileA);
        using var bReader = new StreamReader(fileB);
        using var cReader = new StreamReader(fileC);
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
                        aWriter.WriteLine(secondNum);
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
                        aWriter.WriteLine(firstNum);
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
                        aWriter.WriteLine(firstNum);
                        firstNum = GetInt(bReader);
                        firstCount++;
                        if (firstCount == groupCount || firstNum==int.MaxValue)
                        {
                            firstFinished = true;
                        }
                    }
                    else
                    {
                        aWriter.WriteLine(secondNum);
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

    private static int GetInt(StreamReader reader)
    {
        return int.Parse(reader.ReadLine() ?? $"{int.MaxValue}");
    }
    private static void SplitByGroups(string fileA, int groupCount, string fileB, string fileC)
    {
        using var aReader = new StreamReader(fileA);
        using var bWriter = new StreamWriter(fileB);
        using var cWriter = new StreamWriter(fileC);
    
        bool isOdd = true;
        int currentNumber = GetInt(aReader);
        while (currentNumber != Int32.MaxValue)
        {
            for (int i = 0; i < groupCount && currentNumber != Int32.MaxValue ; i++)
            {
                
                if (isOdd)
                {
                    bWriter.WriteLine(currentNumber);
                }
                else
                {
                    cWriter.WriteLine(currentNumber);
                }

                currentNumber = GetInt(aReader);
            }

            isOdd = !isOdd;
        }
    }
}