using System;
using System.Collections.Generic;
using System.Linq;

namespace FigureLab
{
    public readonly struct Point
    {
        public readonly double X;
        public readonly double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public enum TypeFigure
    {
        Square, //квадрат
        Rectangle, //прямоугольник
        Parallelogram, //параллелограмм
        Rhombus, //ромб
        IsoscelesTrapezoid, //равнобедренная трапеция 
        RectangularTrapezoid, //прямоугольная трапеция
        GeneralTrapezoid, //трапеция общего вида
        GeneralQuadrilateral //четырехугольник общего вида
    }

    internal enum TypeAngle
    {
        Right, //прямой угол
        Straight, //развернутый угол
        Custom //другой угол
    }

    public class Figure
    {
        public Point[] Points { get; }

        private const double Eps = 0.00001;

        public Figure(Point point1, Point point2, Point point3, Point point4)
        {
            if (point1.X == point2.X && point2.X == point3.X || point2.X == point3.X && point3.X == point4.X ||
                point3.X == point4.X && point4.X == point1.X || point4.X == point1.X && point1.X == point2.X)
                throw new Exception("Points on a straight line!");
            if (point1.Y == point2.Y && point2.Y == point3.Y || point2.Y == point3.Y && point3.Y == point4.Y ||
                point3.Y == point4.Y && point4.Y == point1.Y || point4.Y == point1.Y && point1.Y == point2.Y)
                throw new Exception("Points on a straight line!");
            Points = new Point[4];
            Points[0] = point1;
            Points[1] = point2;
            Points[2] = point3;
            Points[3] = point4;
            SortPoints();
            SortPoints();
        }

        public Figure(IReadOnlyList<Point> points) : this(points[0], points[1], points[2], points[3])
        {
            if (points.Count != 4) throw new ArgumentException("invalid points");
        }

        private void SortPoints()
        {
            var xCentral = (Points[0].X + Points[1].X + Points[2].X) / 3;
            var yCentral = (Points[0].Y + Points[1].Y + Points[2].Y) / 3;
            var pointCentral = new Point(xCentral, yCentral);
            var angles = new double[4];
            for (var j = 0; j < Points.Length; j++)
            {
                for (var i = 0; i < Points.Length; i++)
                {
                    if (i == j)
                        continue;
                    
                    angles[i] = GetAngle(Points[j], pointCentral, Points[i]);
                }

                for (var i = 0; i < Points.Length; i++)
                {
                    if (i == j || i == (j + 1) % Points.Length)
                        continue;

                    if (!(angles[i] < angles[(j + 1) % Points.Length])) 
                        continue;
                    
                    var tmp = Points[i];
                    Points[i] = Points[(j + 1) % Points.Length];
                    Points[(j + 1) % Points.Length] = tmp;
                }
            }
        }

        private static double GetAngle(Point point1, Point point2, Point point3)
        {
            var a = new Point(point2.X - point1.X, point2.Y - point1.Y);
            var b = new Point(point2.X - point3.X, point2.Y - point3.Y);

            var cos = (a.X * b.X + a.Y * b.Y) / (Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2)) *
                                                 Math.Sqrt(Math.Pow(b.X, 2) + Math.Pow(b.Y, 2)));
            return Math.Acos(cos);
        }

        private static TypeAngle GetAngleType(Point point1, Point point2, Point point3)
        {
            var angle = GetAngle(point1, point2, point3);
            if (Math.Abs(angle - Math.PI) < Eps)
                return TypeAngle.Straight;
            return Math.Abs(angle - Math.PI / 2) < Eps ? TypeAngle.Right : TypeAngle.Custom;
        }

        private static double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        private static bool CheckParallelism(Point point1, Point point2, Point point3, Point point4)
        {
            var k1 = (point4.X - point1.X) / (point4.Y - point1.Y);
            var k2 = (point3.X - point2.X) / (point3.Y - point2.Y);

            return k1 == k2;
        }

        public TypeFigure GetTypeFigure()
        {
            var equilateral = true; //равносторонний
            var isosceles = false; //равнобедренный
            var anglesType = new List<TypeAngle>()
                {TypeAngle.Custom, TypeAngle.Custom, TypeAngle.Custom, TypeAngle.Custom};
            var distanceLast = GetDistance(Points[0], Points[3]);
            for (var i = 0; i < 3; i++)
                if (distanceLast != GetDistance(Points[i], Points[i + 1]))
                    equilateral = false;
            for (var i = 0; i < 2; i++)
                if (GetDistance(Points[i % 4], Points[(i + 1) % 4]) ==
                    GetDistance(Points[(i + 2) % 4], Points[(i + 3) % 4]))
                    isosceles = true;
            for (var i = 0; i < 4; i++)
                anglesType[(i + 1) % 4] = GetAngleType(Points[i % 4], Points[(i + 1) % 4], Points[(i + 2) % 4]);

            var countRightAngle = anglesType.Count(angle => angle == TypeAngle.Right);


            if (equilateral && countRightAngle == 4)
                return TypeFigure.Square;
            if (isosceles && countRightAngle == 4)
                return TypeFigure.Rectangle;
            if (equilateral)
                return TypeFigure.Rhombus;

            var countParallelismPairs = 0;
            for (var i = 0; i < 2; i++)
                if (CheckParallelism(Points[i % 4], Points[(i + 1) % 4], Points[(i + 2) % 4], Points[(i + 3) % 4]))
                    countParallelismPairs++;

            if (countParallelismPairs == 2)
                return TypeFigure.Parallelogram;

            if (countParallelismPairs != 1) 
                return TypeFigure.GeneralQuadrilateral;
            
            if (isosceles)
                return TypeFigure.IsoscelesTrapezoid;
            
            return countRightAngle == 2 ? TypeFigure.RectangularTrapezoid : TypeFigure.GeneralTrapezoid;
        }
    }
}