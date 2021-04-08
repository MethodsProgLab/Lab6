using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public struct Point
    {
        public double X;
        public double Y;
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
    public enum TypeAngle
    {
        Right, //прямой угол
        Straight, //развернутый угол
        Custom //другой угол
    }
    class Figure
    {
        private Point[] points;
        private const double EPS = 0.00001;
        public Figure(Point point1, Point point2, Point point3, Point point4)
        {
            if ((point1.X == point2.X && point2.X == point3.X) || (point2.X == point3.X && point3.X == point4.X) || (point3.X == point4.X && point4.X == point1.X) || (point4.X == point1.X && point1.X == point2.X))
                throw new Exception("Точки на одной прямой");
            if ((point1.Y == point2.Y && point2.Y == point3.Y) || (point2.Y == point3.Y && point3.Y == point4.Y) || (point3.Y == point4.Y && point4.Y == point1.Y) || (point4.Y == point1.Y && point1.Y == point2.Y))
                throw new Exception("Точки на одной прямой");
            points = new Point[4];
            points[0] = point1;
            points[1] = point2;
            points[2] = point3;
            points[3] = point4;
        }

        private double getAngle(Point point1, Point point2, Point point3)
        {
            Point a = new Point(point2.X - point1.X, point2.Y - point1.Y);
            Point b = new Point(point2.X - point3.X, point2.Y - point3.Y);

            double cos = (a.X * b.X + a.Y * b.Y) / (Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2)) * Math.Sqrt(Math.Pow(b.X, 2) + Math.Pow(b.Y, 2)));
            return Math.Acos(cos);
        }
        private TypeAngle getAngleType(Point point1, Point point2, Point point3)
        {
            double angle = getAngle(point1, point2, point3);
            if (Math.Abs(angle - Math.PI)<EPS)
                return TypeAngle.Straight;
            if(Math.Abs(angle - Math.PI/2) < EPS)
                return TypeAngle.Right;
            return TypeAngle.Custom;
        }

        private double getDistance(Point point1, Point point2)
        {
            double distance = Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
            return distance;
        }
            
        private bool checkParallelism(Point point1, Point point2, Point point3, Point point4)
        {
            double k1 = (point4.X - point1.X) / (point4.Y - point1.Y);
            double k2 = (point3.X - point2.X) / (point3.Y - point2.Y);

            if(k1==k2)
            //if (getDistance(point1, point4) == getDistance(point2, point3))
                return true;
            return false;
        }

        public TypeFigure GetTypeFigure()
        {
            bool equilateral = true; //равносторонний
            bool isosceles = false; //равнобедренный
            List<TypeAngle> anglesType = new List<TypeAngle>() {TypeAngle.Custom,TypeAngle.Custom, TypeAngle.Custom, TypeAngle.Custom };
            double distanceLast = getDistance(points[0], points[3]);
            for (int i = 0; i < 3; i++)
                if (distanceLast != getDistance(points[i], points[i + 1]))
                    equilateral = false;
            for (int i = 0; i < 2; i++)
                if (getDistance(points[i % 4], points[(i + 1) % 4]) == getDistance(points[(i + 2) % 4], points[(i + 3) % 4]))
                    isosceles = true;
            for (int i = 0; i < 4; i++)
                anglesType[(i + 1) % 4] = getAngleType(points[i % 4], points[(i + 1) % 4], points[(i + 2) % 4]);

            int countRightAngle = anglesType.Where(angle => angle == TypeAngle.Right).Count();


            if (equilateral && countRightAngle == 4)
                return TypeFigure.Square;
            if (isosceles && countRightAngle == 4)
                return TypeFigure.Rectangle;
            if (equilateral)
                return TypeFigure.Rhombus;

            int countParallelismPairs = 0;
            for (int i = 0; i < 2; i++)
                if (checkParallelism(points[i % 4], points[(i + 1) % 4], points[(i + 2) % 4], points[(i + 3) % 4]))
                    countParallelismPairs++;

            if (countParallelismPairs == 2)
                return TypeFigure.Parallelogram;

            if (countParallelismPairs == 1)
            {
                if (isosceles)
                    return TypeFigure.IsoscelesTrapezoid;
                if (countRightAngle == 2)
                    return TypeFigure.RectangularTrapezoid;
                return TypeFigure.GeneralTrapezoid;
            }
            return TypeFigure.GeneralQuadrilateral;
        }
    }
}
