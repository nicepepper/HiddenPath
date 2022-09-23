using System;

namespace Map.Pathfinding
{
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }
    
        public bool Equals(Point other)
        {
            return other != null && X == other.X && Y == other.Y;
        }
    
        public static bool operator == (Point point1, Point point2)
        {
            if (((object) point1) == null || ((object) point2) == null)
            {
                return Object.Equals(point1, point2);
            }
            return point1.Equals(point2);
        }
    
        public static bool operator != (Point point1, Point point2)
        {
            if (((object) point1) == null || ((object) point2) == null)
            {
                return ! Object.Equals(point1, point2);
            }
            return ! (point1.Equals(point2));
        }
    
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
