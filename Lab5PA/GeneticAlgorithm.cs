namespace Lab5PA;

public class GeneticAlgorithm
{
    private int NumOfCities;
    private int[,] DistanceMatrix;
    private double ChanceOfMutation;
    private int NumOfPopulation;

    public GeneticAlgorithm(int numOfCities, double chanceOfMutation, int numOfPopulation)
    {
        NumOfCities = numOfCities;
        ChanceOfMutation = chanceOfMutation;
        NumOfPopulation = numOfPopulation;
    }
    public void StartAlgorithm(int iterations)
    {
        DistanceMatrix = GenerateWays(NumOfCities);
        var population = GeneratePopulation();
        for (int j = 0; j < iterations; j++)
        {
            
            population = Reproduction(population);
            population = RemoveTheWeak(population);

            Console.WriteLine($"Iteration: {j+1}\n Best Distance: {population[1].Distance()}");
            Console.WriteLine();
        }
        
    }

    private List<Individual> GeneratePopulation()
    {
        var population = new List<Individual>();
        for (var i = 0; i < NumOfPopulation; i++)
        {
            population.Add(new Individual(NumOfCities, DistanceMatrix));
        }

        return population;
    }
    
    public static int[,] GenerateWays(int numOfCities)
    {
        var matrix = new int[numOfCities, numOfCities];
        var random = new Random();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (i == j)
                {
                    matrix[i, j] = 0;
                    continue;
                }

                matrix[i, j] = random.Next(2, 40);
                matrix[j, i] = random.Next(2, 40);
            }
        }

        return matrix;
    }
    

    private List<Individual> RemoveTheWeak(List<Individual> population)
    {
        population.Sort(Individual.CompareById);
        population.RemoveAt(population.Count - 1);
        population.RemoveAt(population.Count - 1);
        return population;
    }

    private List<Individual> Reproduction(List<Individual> population)
    {
        var (parent1, parent2) = GetParent(population);

        var (child1, child2) = GetChildren(parent1, parent2);

        child1 = Mutation(child1);
        child2 = Mutation(child2);
        
        population.Add(child1);
        population.Add(child2);
        return population;
    }


    private (Individual, Individual) GetParent(List<Individual> population)
    {
        var random = new Random();
        
        var rnd1 = random.Next(population.Count);
        int rnd2;
        do
            rnd2 = random.Next(population.Count);
        while (rnd2 == rnd1);
        
        var parent1 = population[rnd1];
        var parent2 = population[rnd2];
        
        return (parent1, parent2);
    }
    private Individual Mutation(Individual child)
    {
        var random = new Random();
        if (random.NextDouble() <= ChanceOfMutation)
        {
            var el1 = random.Next(child.Cities.Count);
            var el2 = random.Next(child.Cities.Count);
            (child.Cities[el1], child.Cities[el2]) = (child.Cities[el2], child.Cities[el1]);
        }

        return child;
    }
    private (Individual, Individual) GetChildren(Individual parent1, Individual parent2)
    {
        var random = new Random();
        var childBreak = random.Next(NumOfCities);

        var child1 = FillChild(parent1, parent2, childBreak);
        var child2 = FillChild(parent2, parent1, childBreak);

        return (child1, child2);
    }

    private Individual FillChild(Individual parent1, Individual parent2, int childBreak)
    {
        var childCities = parent1.Cities.GetRange(0, childBreak);
        
        for (int i = childBreak; i < parent2.Cities.Count; i++)
            if (!childCities.Contains(parent2.Cities[i]))
                childCities.Add(parent2.Cities[i]);

        if (childCities.Count < parent1.Cities.Count)
            for (int i = childBreak; i < parent1.Cities.Count; i++)
                if (!childCities.Contains(parent1.Cities[i]))
                    childCities.Add(parent1.Cities[i]);

        return new Individual(childCities, DistanceMatrix);
    }
}