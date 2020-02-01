using System;
using System.Drawing;

namespace RayMarcher.SdfFunctions {
    public class SdfBlend : ISdfObject
    {
        public ISdfObject A;
        public ISdfObject B;
        public double Blend;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public SdfBlend(ISdfObject a, ISdfObject b, double blend)
        {
            A = a;
            B = b;
            Blend = blend;
            color = Color.White;
        }

        public SdfBlend(ISdfObject a, ISdfObject b, double blend, Color color)
        {
            A = a;
            B = b;
            Blend = blend;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Blend * A.DistanceFromPoint(point) + (1 - Blend) * B.DistanceFromPoint(point);
        }
    }
}

/*
float sdf_blend(float d1, float d2, float a)
{
 return a * d1 + (1 - a) * d2;
}
 */