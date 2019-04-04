using System;
using System.Drawing;

namespace RayMarcher{
    public class Sphere : ISdfObject
    {
        public Point3d Point;
        public double Radius;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public Sphere(Point3d point, double radius)
        {
            Point = point;
            Radius = radius;
            color = Color.White;
        }

        public Sphere(Point3d point, double radius, Color color)
        {
            Point = point;
            Radius = radius;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            return point.DistanceFromPoint(this.Point) - this.Radius;
        }
    }
}
