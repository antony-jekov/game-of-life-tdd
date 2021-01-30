using System;
using System.Collections.Generic;

namespace Jekov.Gol.Core
{
  public readonly struct Location :
      IEquatable<Location>, IComparable<Location>
  {
    public Location(int x, int y) =>
        (X, Y) = (x, y);

    public Location Bottom => new Location(X, Y + 1);
    public Location BottomLeft => new Location(X - 1, Y + 1);
    public Location BottomRight => new Location(X + 1, Y + 1);
    public Location Left => new Location(X - 1, Y);
    public Location Right => new Location(X + 1, Y);
    public Location Top => new Location(X, Y - 1);
    public Location TopLeft => new Location(X - 1, Y - 1);
    public Location TopRight => new Location(X + 1, Y - 1);

    public IEnumerable<Location> Neighbours
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

    public static Location operator -(Location left, Location right) =>
        new Location(left.X - right.X, left.Y - right.Y);

    public static bool operator !=(Location left, Location right) =>
                (left.X, left.Y) != (right.X, right.Y);

    public static Location operator +(Location left, Location right) =>
        new Location(left.X + right.X, left.Y + right.Y);

    public static bool operator ==(Location left, Location right) =>
        (left.X, left.Y) == (right.X, right.Y);

    public int CompareTo(Location other) =>
        (X, Y).CompareTo((other.X, other.Y));

    public bool Equals(Location other) =>
        (X, Y) == (other.X, other.Y);

    public override bool Equals(object other) =>
        other is Location location && location.Equals(this);

    public override int GetHashCode() =>
        31 * X + 17 * Y;

    public override string ToString() => $"X: {X} Y: {Y}";
  }
}