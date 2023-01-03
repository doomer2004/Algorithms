using Lab2PA.Model;
using Labyrinth.Model;
using Labyrinth.Utils;

namespace Lab2PA.PathSolver;

internal class AStar : IPathSolver
{
    private int _iteration = 0;

    public SearchResult Solve(State state, bool printSteps = false)
    {
        _iteration = 0;
        return SolveAStar(state, printSteps);
    }

    private SearchResult SolveAStar(State state, bool printSteps)
    {
        var open = new HashSet<State>();
        var closed = new HashSet<State>();
        open.Add(state);
        while (open.Count != 0)
        {
            var current = open.MinBy(s => s.Evaluation)!;
            open.Remove(current);
            closed.Add(current);
            
            _iteration++;
            if (printSteps)
                current.PrintState();
            
            if (current.Distance == 1)
                return new SearchResult(current, _iteration, open.Count + closed.Count);
            
            foreach (var child in current.GetChildren())
            {
                if (closed.Contains(child))
                    continue;
                
                if (!open.Contains(child))
                    open.Add(child);
            }
        }

        return new SearchResult(null, int.MaxValue, int.MaxValue);
    }
}