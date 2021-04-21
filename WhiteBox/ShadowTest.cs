using NUnit.Framework;
using ShadowLab;

namespace WhiteBox
{
    public class ShadowTest
    {
        [Test]
        public void PathForPoint()
        {
            var shadow = new Shadow();
            var segment = new Segment(0, 0);
            
            Assert.IsFalse(shadow.AddSegment(segment));
        }

        [Test]
        public void PathForInvalidInterval()
        {
            var shadow = new Shadow();
            var segment = new Segment(10, 0);
            
            Assert.IsFalse(shadow.AddSegment(segment));
            Assert.AreEqual(0,shadow.GetLength());
        }

        [Test]
        public void PathForLength()
        {
            var shadow = new Shadow();
            var segment = new Segment(0, 5);
            
            Assert.IsTrue(shadow.AddSegment(segment));
            Assert.AreEqual(5, shadow.GetLength());
        }

        [Test]
        public void PathIntersection()
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