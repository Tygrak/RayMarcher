using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfSmoothUnion : ISdfObject
    {
        public ISdfObject A;
        public ISdfObject B;
        public double Blend;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public SdfSmoothUnion(ISdfObject a, ISdfObject b, double blend)
        {
            A = a;
            B = b;
            Blend = blend;
            color = Color.White;
        }

        public SdfSmoothUnion(ISdfObject a, ISdfObject b, double blend, Color color)
        {
            A = a;
            B = b;
            Blend = blend;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            double d1 = A.DistanceFromPoint(point);
            double d2 = B.DistanceFromPoint(point);
            double h = Math.Max(Blend-Math.Abs(d1-d2), 0) / Blend;
            return Math.Min(d1, d2) - h*h*h*Blend*(1/6.0);
        }
    }
}

/*
float sdf_blend(float d1, float d2, float a)
{
 return a * d1 + (1 - a) * d2;
}
 */