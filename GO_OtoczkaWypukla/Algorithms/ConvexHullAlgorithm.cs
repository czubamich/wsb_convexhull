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

    // Function to find orientation of three points (p, q, r)
    private static int Orientation(Point p, Point q, Point r)
    {
        double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

        if (Math.Abs(val) < double.Epsilon)
            return 0; // Collinear
        else if (val > 0)
            return 1; // Clockwise
        else
            return 2; // Counterclockwise
    }

    public static List<Point> CalculateConvexHull(List<Point> givenPoints)
    {
        var points = new List<Point>(givenPoints);

        int n = points.Count;
        if (n < 3)
            return points;

        List<Point> hull = new List<Point>();

        int leftmost = 0;
        for (int i = 1; i < n; i++)
        {
            if (points[i].X < points[leftmost].X)
                leftmost = i;
        }

        int p = leftmost, q;
        do
        {
            hull.Add(points[p]);
            q = (p + 1) % n;
            for (int i = 0; i < n; i++)
            {
                if (Orientation(points[p], points[i], points[q]) == 2)
                    q = i;
            }
            p = q;

        } while (p != leftmost);

        return hull;
    }
}