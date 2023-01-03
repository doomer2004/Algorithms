using Labyrinth.Model;

namespace Lab2PA.Model;

internal record SearchResult(State? State, int Path, int StoredStates = 0);