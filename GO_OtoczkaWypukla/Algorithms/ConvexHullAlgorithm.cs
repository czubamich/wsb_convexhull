using Blazorise;
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
        var r = Random.Shared.Next(0,100);
        var theta = Random.Shared.NextDouble() * 2 * Math.PI;
        
        X = (int)Math.Floor(0 + r * Math.Cos(theta));
        Y = (int)Math.Floor(0 + r * Math.Sin(theta));
    }
}

public class ConvexHullAlgorithm
{
    //Wyznaczenie orientacji 3 punkt�w wzgl�dem siebie
    private static int Orientation(Point O, Point A, Point B)
    {
        return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
    }

    public static List<Point> CalculateConvexHull(List<Point> points)
    {
        // Je�eli przekazany zbi�r jest null zwr�� null any unikn�� wyj�tku
        if (points == null)
            return null;

        // Je�eli jest mniej ni� 3 punkty zwr�c zbi�r wej�ciowy
        if (points.Count() < 3)
            return points;

        //Deklaracja zmiennych
        int n = points.Count(), k = 0;
        List<Point> H = new List<Point>(new Point[2 * n]);

        //Posortowanie punkt�w wed�ug wsp�rz�dnych X (a je�eli s� r�wne to wed�ug Y)
        points.Sort((a, b) =>
             a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

        // Wyznaczenie g�rnej cz�ci otoczki
        for (int i = 0; i < n; ++i)
        {
            while (k >= 2 && Orientation(H[k - 2], H[k - 1], points[i]) <= 0)
                k--;
            H[k++] = points[i];
        }

        // Wyznaczenie dolnej cz�ci otoczki
        for (int i = n - 2, t = k + 1; i >= 0; i--)
        {
            while (k >= t && Orientation(H[k - 2], H[k - 1], points[i]) <= 0)
                k--;
            H[k++] = points[i];
        }

        return H.Take(k - 1).ToList();
    }
}