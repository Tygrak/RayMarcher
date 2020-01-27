using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfElongation : ISdfObject
    {
        public ISdfObject Primitive;
        public Point3d Elongation;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfElongation(ISdfObject primitive, Point3d elongation)
        {
            Primitive = primitive;
            Elongation = elongation;
        }

        public double DistanceFromPoint(Point3d point)
        {
            Double x = Math.Abs(point.X)-Elongation.X;
            Double y = Math.Abs(point.Y)-Elongation.Y;
            Double z = Math.Abs(point.Z)-Elongation.Z;
            Double primDist = Primitive.DistanceFromPoint(new Point3d(Math.Max(x, 0), Math.Max(y, 0), Math.Max(z, 0)));
            return primDist + Math.Min(Math.Max(x, Math.Max(y, z)), 0);
        }
    }
}

/*
float opElongate( in sdf3d primitive, in vec3 p, in vec3 h )
{
    vec3 q = abs(p)-h;
    return primitive( max(q,0.0) ) + min(max(q.x,max(q.y,q.z)),0.0);
}
 */