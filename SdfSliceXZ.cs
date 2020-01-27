using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfSliceXZ : ISdfObject
    {
        public ISdfObject Primitive;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfSliceXZ(ISdfObject primitive)
        {
            Primitive = primitive;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Math.Max(-point.Y, Primitive.DistanceFromPoint(point));
        }
    }
}