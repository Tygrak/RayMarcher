using System;
using System.Drawing;

namespace RayMarcher{
    public class Sphere
    {
        public Point3d Point;
        public double Radius;
        public Color Color;
 
        public Sphere(Point3d point, double radius)
        {
            Point = point;
            Radius = radius;
            Color = Color.Black;
        }

        public Sphere(Point3d point, double radius, Color color)
        {
            Point = point;
            Radius = radius;
            Color = color;
        }
    }
}
