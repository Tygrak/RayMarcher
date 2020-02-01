using System;
using System.Drawing;

namespace RayMarcher.SdfFunctions {
    public class SdfOnion : ISdfObject
    {
        public ISdfObject Primitive;
        public double Thickness;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfOnion(ISdfObject primitive, double thickness)
        {
            Primitive = primitive;
            Thickness = thickness;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Math.Abs(Primitive.DistanceFromPoint(point))-Thickness;
        }
    }
}

/*
float opOnion( in float sdf, in float thickness )
{
    return abs(sdf)-thickness;
}
 */