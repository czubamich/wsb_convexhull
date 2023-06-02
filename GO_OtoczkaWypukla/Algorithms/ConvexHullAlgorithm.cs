using System.Collections.Generic;

namespace GO_OtoczkaWypukla.Algorithms;

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = 0;
        Y = 0;
    }

    public Point()
    {
        X = Random.Shared.Next(-10, 10); ;
        Y = Random.Shared.Next(-10, 10); ;
    }
}

public class ConvexHullAlgorithm
{

    private static double Orientation(Point O, Point A, Point B)
    {
        return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
    }

    public static List<Point> CalculateConvexHull(List<Point> points)
    {
        if (points == null)
            return null;

        if (points.Count() <= 1)
            return points;

        int n = points.Count(), k = 0;
        List<Point> H = new List<Point>(new Point[2 * n]);

        points.Sort((a, b) =>
             a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

        // Build lower hull
        for (int i = 0; i < n; ++i)
        {
            while (k >= 2 && Orientation(H[k - 2], H[k - 1], points[i]) <= 0)
                k--;
            H[k++] = points[i];
        }

        // Build upper hull
        for (int i = n - 2, t = k + 1; i >= 0; i--)
        {
            while (k >= t && Orientation(H[k - 2], H[k - 1], points[i]) <= 0)
                k--;
            H[k++] = points[i];
        }

        return H.Take(k - 1).ToList();
    }
}