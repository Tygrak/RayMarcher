using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfUnion : ISdfObject
    {
        public ISdfObject A;
        public ISdfObject B;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public SdfUnion(ISdfObject a, ISdfObject b)
        {
            A = a;
            B = b;
            color = a.ObjectColor;
        }

        public SdfUnion(ISdfObject a, ISdfObject b, Color color)
        {
            A = a;
            B = b;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Math.Min(A.DistanceFromPoint(point), B.DistanceFromPoint(point));
        }
    }
}
