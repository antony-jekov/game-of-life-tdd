using System;
using System.Collections.Generic;

namespace Jekov.Gol.Core
{
  public readonly struct Cell :
      IEquatable<Cell>, IComparable<Cell>
  {
    public Cell(int x, int y) =>
        (X, Y) = (x, y);

    public Cell Bottom => new Cell(X, Y + 1);
    public Cell BottomLeft => new Cell(X - 1, Y + 1);
    public Cell BottomRight => new Cell(X + 1, Y + 1);
    public Cell Left => new Cell(X - 1, Y);
    public Cell Right => new Cell(X + 1, Y);
    public Cell Top => new Cell(X, Y - 1);
    public Cell TopLeft => new Cell(X - 1, Y - 1);
    public Cell TopRight => new Cell(X + 1, Y - 1);

    public IEnumerable<Cell> Neighbours
    {
      get
      {
        yield return Left;
        yield return TopLeft;
        yield return Top;
        yield return TopRight;
        yield return Right;
        yield return BottomRight;
        yield return Bottom;
        yield return BottomLeft;
      }
    }

    public int X { get; }
    public int Y { get; }

    public static Cell operator -(Cell left, Cell right) =>
        new Cell(left.X - right.X, left.Y - right.Y);

    public static bool operator !=(Cell left, Cell right) =>
                (left.X, left.Y) != (right.X, right.Y);

    public static Cell operator +(Cell left, Cell right) =>
        new Cell(left.X + right.X, left.Y + right.Y);

    public static bool operator ==(Cell left, Cell right) =>
        (left.X, left.Y) == (right.X, right.Y);

    public int CompareTo(Cell other) =>
        (X, Y).CompareTo((other.X, other.Y));

    public bool Equals(Cell other) =>
        (X, Y) == (other.X, other.Y);

    public override bool Equals(object other) =>
        other is Cell cell && cell.Equals(this);

    public override int GetHashCode() =>
        31 * X + 17 * Y;

    public override string ToString() => $"X: {X} Y: {Y}";
  }
}