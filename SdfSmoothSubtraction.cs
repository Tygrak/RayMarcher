using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfSmoothSubtraction : ISdfObject
    {
        public ISdfObject A;
        public ISdfObject B;
        public double Blend;
        public Color ObjectColor{ get {return A.ObjectColor;} set {A.ObjectColor = value;} }
 
        public SdfSmoothSubtraction(ISdfObject a, ISdfObject b, double blend)
        {
            A = a;
            B = b;
            Blend = blend;
        }

        public double DistanceFromPoint(Point3d point)
        {
            double d1 = A.DistanceFromPoint(point);
            double d2 = B.DistanceFromPoint(point);
            double h = Math.Max(Blend-Math.Abs(d1-d2), 0) / Blend;
            return Math.Max(d1, -d2) - h*h*h*Blend*(1/6.0);
        }
    }
}
