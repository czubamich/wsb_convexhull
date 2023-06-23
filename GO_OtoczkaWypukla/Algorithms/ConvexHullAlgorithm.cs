using System.Collections.Generic;

namespace GO_OtoczkaWypukla.Algorithms;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = 0;
        Y = 0;
    }

    public Point()
    {
        X = Random.Shared.Next(-20, 20); ;
        Y = Random.Shared.Next(-20, 20); ;
    }
}

public class ConvexHullAlgorithm
{

    private static int Orientation(Point O, Point A, Point B)
    {
        return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
    }

    public static List<Point> CalculateConvexHull(List<Point> points)
    {
        // Je�eli przekazany zbi�r jest null zwr�� null any unikn�� wyj�tku
        if (points == null)
            return null;

        // Je�eli jest mniej ni� 2 punkty zwr�c zbi�r wej�ciowy
        if (points.Count() <= 1)
            return points;

        //zmiennych
        int n = points.Count(), k = 0;
        List<Point> H = new List<Point>(new Point[2 * n]);

        //Posortowanie punkt�w wedg��g wsp�rz�dnych X (a je�eli s� r�wne to wed�ug Y)
        points.Sort((a, b) =>
             a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

        // Wyznaczenie dolnej cz�ci otoczki
        for (int i = 0; i < n; ++i)
        {
            while (k >= 2 && Orientation(H[k - 2], H[k - 1], points[i]) <= 0)
                k--;
            H[k++] = points[i];
        }

        // Wyznaczenie g�rnej cz�ci otoczki
        for (int i = n - 2, t = k + 1; i >= 0; i--)
        {
            while (k >= t && Orientation(H[k - 2], H[k - 1], points[i]) <= 0)
                k--;
            H[k++] = points[i];
        }

        return H.Take(k - 1).ToList();
    }
}