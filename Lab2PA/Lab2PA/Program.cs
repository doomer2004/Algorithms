using System.Diagnostics;
using Lab2PA.Model;
using Lab2PA.PathSolver;
using Labyrinth.Model;
using Labyrinth.Utils;

namespace Lab2PA;

internal static class Program
{
    private static void Main(string[] args)
    {
        string path = @"C:/Data/result.json";
        const bool printSteps = false;

        var maze = Maze.LoadFromFile(path);

        Console.WriteLine(maze + "\n");

        var state = new State(maze, null);
        Console.Write("Choose IDS or A* solving method: ");
        IPathSolver solver = Console.ReadLine()!.ToLower() == "ids" ? new IDS() : new AStar();


        var sw = Stopwatch.StartNew();
        var res = solver.Solve(state, printSteps);
        sw.Stop();
        Console.WriteLine($"Algorithm took {sw.ElapsedMilliseconds} ms");

        if (res.State == null)
        {
            Console.WriteLine("There is no way");
        }
        else
        {
            IEnumerable<State> solutionPath = res.State.GetPath();
            Console.WriteLine(solutionPath.First(st => st.Distance == 1).Maze);
            
            Console.WriteLine("Dead ends: " + state.GetTotalImpasses());
            Console.WriteLine("Total states: " + state.GetTotalNodes().Count);
            Console.WriteLine("Stored states: " + res.Path);
            Console.WriteLine("Path: ");
            Console.WriteLine(string.Join(", ",
                solutionPath.Reverse().Select(part => part.Maze.Selected.Coordinate.ToString())));
        }

        Console.ReadLine();
    }
}