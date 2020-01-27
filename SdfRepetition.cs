using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfRepetition : ISdfObject
    {
        public ISdfObject Primitive;
        public Point3d RepetitionDistance;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfRepetition(ISdfObject primitive, double repetitionDistance)
        {
            Primitive = primitive;
            RepetitionDistance = new Point3d(repetitionDistance, repetitionDistance, repetitionDistance);
        }

        public SdfRepetition(ISdfObject primitive, Point3d repetitionDistance)
        {
            Primitive = primitive;
            RepetitionDistance = repetitionDistance;
        }

        //todo: fix
        public double DistanceFromPoint(Point3d point)
        {
			//Math.IEEERemainder(point.X,RepetitionDistance) - RepetitionDistance/2, 
            Double x = RepetitionDistance.X > 0 ? Math.IEEERemainder(point.X, RepetitionDistance.X) : point.X;
            Double y = RepetitionDistance.Y > 0 ? Math.IEEERemainder(point.Y, RepetitionDistance.Y) : point.Y;
            Double z = RepetitionDistance.Z > 0 ? Math.IEEERemainder(point.Z, RepetitionDistance.Z) : point.Z;
            
            return Primitive.DistanceFromPoint(new Point3d(x,y,z));
        }
    }
}

/*
float opRep( in vec3 p, in vec3 c, in sdf3d primitive )
{
    vec3 q = mod(p,c)-0.5*c;
    return primitve( q );
}
 */