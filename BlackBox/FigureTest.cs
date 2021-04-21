using FigureLab;
using NUnit.Framework;

namespace BlackBox
{
    public class FigureTest
    {
        [Test]
        public void CheckForZeros()
        {
            var points = new Point[4];
            for (var i = 0; i < 4; i++)
            {
                points[i] = new Point(0, 0);
            }

            Assert.Catch(() => new Figure(points));
        }

        [Test]
        public void CheckForInvalid()
        {
            var points = new Point[4];
            for (var i = 0; i < 4; i++)
            {
                points[i] = new Point(i, 0);
            }

            Assert.Catch(() => new Figure(points));

            for (var i = 0; i < 4; i++)
            {
                points[i] = new Point(0, i);
            }

            Assert.Catch(() => new Figure(points));
        }
        
        [Test]
        public void CheckForGeneralQuadrilateral()
        {
            var points = new Point[4];
            points[0] = new Point(4, 0);
            points[1] = new Point(-1, -7);
            points[2] = new Point(-3, -7);
            points[3] = new Point(-4, -2);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.GeneralQuadrilateral, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForGeneralTrapezoid()
        {
            var points = new Point[4];
            points[0] = new Point(1, 1);
            points[1] = new Point(10, 1);
            points[2] = new Point(8, 6);
            points[3] = new Point(5, 6);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.GeneralTrapezoid, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForSquare()
        {
            var points = new Point[4];
            points[0] = new Point(0, 0);
            points[1] = new Point(0, 2);
            points[2] = new Point(2, 0);
            points[3] = new Point(2, 2);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.Square, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForRectangle()
        {
            var points = new Point[4];
            points[0] = new Point(-4, -2);
            points[1] = new Point(-4, 2);
            points[2] = new Point(5, 2);
            points[3] = new Point(5, -2);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.Rectangle, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForParallelogram()
        {
            var points = new Point[4];
            points[0] = new Point(2, 5);
            points[1] = new Point(5, 2);
            points[2] = new Point(10, 2);
            points[3] = new Point(7, 5);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.Parallelogram, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForRhombus()
        {
            var points = new Point[4];
            points[0] = new Point(0, 6);
            points[1] = new Point(5, 3);
            points[2] = new Point(10, 6);
            points[3] = new Point(5, 9);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.Rhombus, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForIsoscelesTrapezoid()
        {
            var points = new Point[4];
            points[0] = new Point(0, 0);
            points[1] = new Point(3, 5);
            points[2] = new Point(11, 5);
            points[3] = new Point(14, 0);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.IsoscelesTrapezoid, figure.GetTypeFigure());
        }

        [Test]
        public void CheckForRectangularTrapezoid()
        {
            var points = new Point[4];
            points[0] = new Point(3, 3);
            points[1] = new Point(10, 3);
            points[2] = new Point(9, 9);
            points[3] = new Point(3, 9);

            var figure = new Figure(points);
            Assert.AreEqual(TypeFigure.RectangularTrapezoid, figure.GetTypeFigure());
        }
    }
}