﻿using Lab2PA.Model;
using Labyrinth.Enums;

namespace Labyrinth.Model;
internal class State : IEquatable<State>
{
    private List<State> _children = null!;

    public Maze Maze { get; set; } = null!;
    public State? Parent { get; set; }
    public int Distance => (int)Math.Ceiling(Cell.DistanceBetween(Maze.Selected, Maze.Destination));
    public int Generation { get; set; }
    public int Evaluation { get; set; }
    public List<State> GetDepth()
    {
        List<State> path = new List<State>();
        foreach (State node in GetChildren())
        {
            List<State> tmp = node.GetDepth();
            if (tmp.Count > path.Count)
                path = tmp;
        }
        path.Insert(0, this);
        return path;
    }
    public State(Maze maze, State? parent)
    {
        Maze = maze ?? throw new ArgumentNullException(nameof(maze));
        Parent = parent;
        Generation = 1 + parent?.Generation ?? 0;
        Evaluation = Distance + Generation;
    }
    public List<State> GetChildren()
    {
        if (_children != null)
            return _children;
        _children = new List<State>(3);
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (TryMove(direction, out State? currentChild) && currentChild!.Equals(Parent) == false)
            {
                _children.Add(currentChild);
            }
        }
        return _children;
    }
    public List<State> GetPath()
    {
        List<State> result = new List<State>();
        State? curr = this;
        do
        {
            result.Add(curr);
            curr = curr.Parent;
        } while (curr != null);
        return result;
    }

    private bool TryMove(Direction dir, out State? newState)
    {
        (int horizontal, int vertical) = (0, 0);
        switch (dir)
        {
            case Direction.Up:
                vertical--;
                break;
            case Direction.Down:
                vertical++;
                break;
            case Direction.Left:
                horizontal--;
                break;
            case Direction.Right:
                horizontal++;
                break;
        }

        Cell selected = Maze.Selected;
        Cell? below = Maze[selected.Coordinate.X + vertical, selected.Coordinate.Y + horizontal];

        newState = null;

        if (below == null)
            return false;
        if (below.Type is not CellType.Empty)
            return false;

        Maze moved = Maze.Clone();
        moved.MoveSelection(dir);

        newState = new State(moved, this);
        return true;
    }

    public bool Equals(State? other)
    {
        return other is not null
               && Maze.Equals(other.Maze)
               && Parent == other.Parent;
    }
    public override string ToString()
    {
        return $"Generation: {Generation}, Distance: {Distance}, F: {Evaluation}";
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as State);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Maze, Parent, Distance, Generation, Evaluation);
    }
}
