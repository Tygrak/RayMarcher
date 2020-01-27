using System;
using System.Drawing;

namespace RayMarcher{
    public class Box : ISdfObject
    {
        public Point3d Point;
        public Point3d Size;
        private Color color;
        public Color ObjectColor{ get {return color;} set {color = value;} }

        public Box(double size) : this(new Point3d(), new Point3d(size, size, size), Color.White)
        {
        }

        public Box(double size, Color color) : this(new Point3d(), new Point3d(size, size, size), color)
        {
        }
 
        public Box(Point3d point, double size) : this(point, new Point3d(size, size, size), Color.White)
        {
        }

        public Box(Point3d point, double size, Color color) : this(point, new Point3d(size, size, size), color)
        {
        }

        public Box(Point3d point, Point3d size) : this(point, size, Color.White)
        {
        }

        public Box(Point3d point, Point3d size, Color color)
        {
            Point = point;
            Size = size;
            this.color = color;
        }

        public double DistanceFromPoint(Point3d point)
        {
            /*point = point-this.Point;
            Point3d d = new Point3d(Math.Abs(point.X) - Size, Math.Abs(point.Y) - Size, Math.Abs(point.Z) - Size);
            return Math.Max(d.VectorLength(), 0) + Math.Min(Math.Max(d.X, Math.Max(d.Y, d.Z)), 0);*/
            double x = Math.Abs(point.X-Point.X)-Size.X/2.0;
            double y = Math.Abs(point.Y-Point.Y)-Size.Y/2.0;
            double z = Math.Abs(point.Z-Point.Z)-Size.Z/2.0;
            return Math.Max(x, Math.Max(y, z));
            /*double x = Math.Max(point.X - Point.X - Size.X/2.0, Point.X - point.X - Size.X/2.0);
            double y = Math.Max(point.Y - Point.Y - Size.Y/2.0, Point.Y - point.Y - Size.Y/2.0);
            double z = Math.Max(point.Z - Point.Z - Size.Z/2.0, Point.Z - point.Z - Size.Z/2.0);
            double d = x;
            d = Math.Max(d,y);
            d = Math.Max(d,z);
            return d;*/
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

float vmax(float3 v)
{
 return max(max(v.x, v.y), v.z);
}
 
float sdf_boxcheap(float3 p, float3 c, float3 s)
{
 return vmax(abs(p-c) - s);
}
 */