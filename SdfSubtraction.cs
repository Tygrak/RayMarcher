using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfSubtraction : ISdfObject
    {
        public ISdfObject A;
        public ISdfObject B;
        public Color ObjectColor{ get {return A.ObjectColor;} set {A.ObjectColor = value;} }
 
        public SdfSubtraction(ISdfObject a, ISdfObject b)
        {
            A = a;
            B = b;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Math.Max(-B.DistanceFromPoint(point), A.DistanceFromPoint(point));
        }
    }
}
