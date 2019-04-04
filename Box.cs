using System;
using System.Drawing;

namespace RayMarcher{
    public class Box : ISdfObject
    {
        public Point3d Point;
        public double Size;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }
 
        public Box(Point3d point, double size)
        {
            Point = point;
            Size = size;
            color = Color.White;
        }

        public Box(Point3d point, double size, Color color)
        {
            Point = point;
            Size = size;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            point = point-this.Point;
            Point3d d = new Point3d(Math.Abs(point.X) - Size, Math.Abs(point.Y) - Size, Math.Abs(point.Z) - Size);
            return Math.Max(d.VectorLength(), 0) + Math.Min(Math.Max(d.X, Math.Max(d.Y, d.Z)), 0);
        }
    }
}

/*
float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return length(max(d,0.0))
         + min(max(d.x,max(d.y,d.z)),0.0); // remove this line for an only partially signed sdf 
}
 */