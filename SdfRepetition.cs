using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfRepetition : ISdfObject
    {
        public ISdfObject Primitive;
        public double RepetitionDistance;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfRepetition(ISdfObject primitive, double repetitionDistance)
        {
            Primitive = primitive;
            RepetitionDistance = repetitionDistance;
        }

        //todo: fix
        public double DistanceFromPoint(Point3d point)
        {
            return Primitive.DistanceFromPoint(new Point3d(
                point.X%RepetitionDistance - RepetitionDistance/2, 
                point.Y%RepetitionDistance - RepetitionDistance/2,
                point.Z%RepetitionDistance - RepetitionDistance/2));
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