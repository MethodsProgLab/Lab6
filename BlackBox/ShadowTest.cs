using NUnit.Framework;
using ShadowLab;

namespace BlackBox
{
    public class ShadowTest
    {
        [Test]
        public void CheckForZeros()
        {
            var shadow = new Shadow();
            var segment = new Segment(0, 0);
            
            Assert.IsFalse(shadow.AddSegment(segment));
        }

        [Test]
        public void CheckForInvalidInterval()
        {
            var shadow = new Shadow();
            var segment = new Segment(10, 0);
            
            Assert.IsFalse(shadow.AddSegment(segment));
            Assert.AreEqual(0,shadow.GetLength());
        }

        [Test]
        public void CheckForSmallInvalidInterval()
        {
            const double eps = 0.0000001;
            var segment = new Segment(1 + eps, 1);
            var shadow = new Shadow();

            Assert.IsFalse(shadow.AddSegment(segment));
            Assert.AreEqual(0,shadow.GetLength());
            
            segment = new Segment(1, 1 - eps);
            Assert.IsFalse(shadow.AddSegment(segment));
            Assert.AreEqual(0,shadow.GetLength());
        }

        [Test]
        public void CheckForLength()
        {
            var shadow = new Shadow();
            var segment = new Segment(0, 5);
            
            Assert.IsTrue(shadow.AddSegment(segment));
            Assert.AreEqual(5, shadow.GetLength());
        }

        [Test]
        public void CheckIntersection()
        {
            var root = new Segment(0, 5);
            var child = new Segment(2, 4);
            var shadow = new Shadow();
            
            Assert.IsTrue(root.Intersection(child));
            Assert.IsTrue(shadow.AddSegment(root) && shadow.AddSegment(child));
            Assert.AreEqual(5, shadow.GetLength());
        }
    }
}