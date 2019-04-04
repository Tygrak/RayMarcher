using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfIntersection : ISdfObject
    {
        public ISdfObject A;
        public ISdfObject B;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public SdfIntersection(ISdfObject a, ISdfObject b)
        {
            A = a;
            B = b;
            color = Color.White;
        }

        public SdfIntersection(ISdfObject a, ISdfObject b, Color color)
        {
            A = a;
            B = b;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Math.Max(A.DistanceFromPoint(point), B.DistanceFromPoint(point));
        }
    }
}
