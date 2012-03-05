
using System.Drawing;
using System;
namespace AroLibraries.ExtensionMethods.Drawing
{
    public static class PointExt
    {
        public static Point AddX(this Point iPoint, int value)
        {
            return new Point(iPoint.X + value, iPoint.Y);
        }
        public static Point AddY(this Point iPoint, int value)
        {
            return new Point(iPoint.X, iPoint.Y + value);
        }
        public static Point Add(this Point iPoint, int valueX,int valueY)
        {
            return new Point(iPoint.X + valueX, iPoint.Y + valueY);
        }

        public static double GetDistance(this Point iPoint, Point iSecondPoint)
        {
            return GetDistanceManhattan(iPoint, iSecondPoint);
        }

        public static double GetDistanceEuklides(this Point iPoint, Point iSecondPoint)
        {
            double X = Math.Pow(iPoint.X - iSecondPoint.X,2);
            double Y = Math.Pow(iPoint.Y - iSecondPoint.Y,2);
            return Math.Sqrt(X + Y);
        }
        
        public static double GetDistanceManhattan(this Point iPoint, Point iSecondPoint)
        {
            double X = Math.Abs(iPoint.X - iSecondPoint.X);
            double Y = Math.Abs(iPoint.Y - iSecondPoint.Y);
            return X + Y;
        }
        public static double GetDistanceMaksimum(this Point iPoint, Point iSecondPoint)
        {
            double X = Math.Abs(iPoint.X - iSecondPoint.X);
            double Y = Math.Abs(iPoint.Y - iSecondPoint.Y);
            return Math.Max(X,Y);
        }

        public static bool AnyCoordinates(this Point iPoint, Predicate<int> predicate)
        {
            return predicate(iPoint.X) || predicate(iPoint.Y); 
        }
        
        public static bool AllCoordinates(this Point iPoint, Predicate<int> predicate)
        {
            return predicate(iPoint.X) && predicate(iPoint.Y); 
        }


    }
}
