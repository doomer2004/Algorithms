using System.Collections;

namespace Lab5PA;

public class Individual
{
    //порядок вершин для проходження графу
    public List<int> Cities { get; set; }
    private int[,] DistanceMatrix;
    
    public Individual(List<int> cities, int[,] distanceMatrix)
    {
        DistanceMatrix = distanceMatrix;
        Cities = cities;
    }
    public Individual(int numOfCities, int[,] distanceMatrix)
    {
        DistanceMatrix = distanceMatrix;
        Cities = Generate(numOfCities);
    }

    private List<int> Generate(int numOfCities)
    {
        var citiesList = new int[numOfCities];
        var rnd = new Random();
        for (var i = 0; i < numOfCities; i++)
        {
            var j = rnd.Next(i + 1);
            if (j != i)
                citiesList[i] = citiesList[j];
            citiesList[j] = i;
        }

        

        return new List<int>(citiesList);
    }
    
    public int Distance()
    {
        var d = 0;
        for (int i = 1; i < Cities.Count; i++)
            d += DistanceMatrix[Cities[i - 1], Cities[i]];

        return d;
    }
    
    public static int CompareById(Individual ind1, Individual ind2)
    {
        return ind1.Distance().CompareTo(ind2.Distance());
    }

}