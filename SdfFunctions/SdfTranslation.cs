using System;
using System.Drawing;

namespace RayMarcher.SdfFunctions {
    public class SdfTranslation : ISdfObject
    {
        public ISdfObject Primitive;
        public Point3d Translation;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfTranslation(ISdfObject primitive, Point3d translation)
        {
            Primitive = primitive;
            Translation = translation;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return Primitive.DistanceFromPoint(point-Translation);
        }
    }
}