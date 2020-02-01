using System;
using System.Drawing;

namespace RayMarcher.SdfObjects {
    public class HalfSpace : ISdfObject
    {
        private Point3d point;
        private Point3d normal;
        public Point3d Point {get {return point;} set {point = value; CalculateArgument();}}
        public Point3d Normal {get {return normal;} set {normal = value; CalculateArgument();}}
        private Color color;
        private double argument;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public HalfSpace(Point3d point, Point3d normal)
        {
            this.point = point;
            this.normal = normal;
            color = Color.White;
            CalculateArgument();
        }

        public HalfSpace(Point3d point, Point3d normal, Color color)
        {
            this.point = point;
            this.normal = normal;
            this.color = color;
            CalculateArgument();
        }

        private void CalculateArgument()
        {
            argument = -normal.VectorDot(point);
        }

        public double DistanceFromPoint(Point3d point)
        {
            return (normal.X*point.X+normal.Y*point.Y+normal.Z*point.Z+argument)/(Math.Sqrt(normal.X*normal.X+normal.Y*normal.Y+normal.Z*normal.Z));
        }
    }
}
