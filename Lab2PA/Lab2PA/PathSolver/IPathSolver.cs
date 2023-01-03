using Lab2PA.Model;
using Labyrinth.Model;

namespace Lab2PA.PathSolver;
internal interface IPathSolver
{
    SearchResult Solve(State state, bool printSteps = false);
}
