﻿using Lab2PA.Model;
using Labyrinth.Model;
using Labyrinth.Utils;

namespace Lab2PA.PathSolver;
internal class IDS : IPathSolver
{
    private int _iteration = 0;
    public SearchResult Solve(State state, bool printSteps = false)
    {
        _iteration = 0;
        int states = state.GetDepth().Count;
        for (int depth = 0; depth <= states; depth++)
        {
            SearchResult searchResult = DFS(state, state, depth, 0, printSteps);
            if (searchResult.State != null)
            {
                Console.WriteLine("Iterations: " + _iteration);
                return new SearchResult(searchResult.State, searchResult.State.Generation);
            }
        }
        return new SearchResult(null, int.MaxValue);

    }

    private SearchResult DFS(State init, State current, int depth, int storedCount, bool printSteps)
    {
        if (current.Generation < depth)
        {
            _iteration++;
            if (printSteps)
                current.PrintState(_iteration);

            if (current.Distance == 1)
                return new SearchResult(current, 0, storedCount);
            List<State> children = current.GetChildren();
            foreach (State child in children)
            {
                SearchResult result = DFS(init, child, depth, storedCount + children.Count, printSteps);
                if (result.State != null)
                    return result;
            }
        }
        return new SearchResult(null, int.MaxValue);
    }
}