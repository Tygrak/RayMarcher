using System;
using System.Drawing;

namespace RayMarcher{
    public class SdfRotation : ISdfObject
    {
        public ISdfObject Primitive;
        public Point3d Rotation;
        public Color ObjectColor{ get {return Primitive.ObjectColor;} set {Primitive.ObjectColor = value;} } 
 
        public SdfRotation(ISdfObject primitive, Point3d rotation)
        {
            Primitive = primitive;
            Rotation = rotation;
        }

        //todo: fix!
        //https://en.wikipedia.org/wiki/Rotation_matrix#In_three_dimensions
        public double DistanceFromPoint(Point3d point)
        {
            Matrix3d x = new Matrix3d(1, 0, 0, 
                                      0, Math.Cos(Rotation.X), -Math.Sin(Rotation.X), 
                                      0, Math.Sin(Rotation.X), Math.Cos(Rotation.X));
            Matrix3d y = new Matrix3d(Math.Cos(Rotation.Y), 0, Math.Sin(Rotation.Y), 
                                      0, 1, 0, 
                                      -Math.Sin(Rotation.Y), 0, Math.Cos(Rotation.Y));
            Matrix3d z = new Matrix3d(Math.Cos(Rotation.Z), -Math.Sin(Rotation.Z), 0, 
                                      Math.Sin(Rotation.Z), Math.Cos(Rotation.Z), 0, 
                                      0, 0, 1);
            Matrix3d r = z*y*x;
            return Primitive.DistanceFromPoint(r*point);
        }
    }
}