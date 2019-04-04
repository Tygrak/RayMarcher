using System;

namespace RayMarcher{
    public struct Point3d
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
 
        public Point3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
 
        public double DistanceFromPoint(double x, double y, double z)
        {
            return Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y) + (z - Z) * (z - Z));
        }

        public double DistanceFromPoint(Point3d point)
        {
            return DistanceFromPoint(point.X, point.Y, point.Z);
        }

        public double DistanceFromSphere(Point3d point, double radius)
        {
            return DistanceFromPoint(point) - radius;
        }

        public double DistanceFromSphere(double x, double y, double z, double radius)
        {
            return DistanceFromPoint(x, y, z) - radius;
        }

        public double DistanceFromSphere(Sphere sphere)
        {
            return DistanceFromSphere(sphere.Point, sphere.Radius);
        }
 
        public double VectorLength()
        {
            return Math.Sqrt(X*X  + Y*Y + Z*Z);
        }
 
        public Point3d VectorNormalize()
        {
            return this/VectorLength();
        }

        public double VectorDot(Point3d point)
        {
            return this.X*point.X+this.Y*point.Y+this.Z*point.Z;
        }

        public Point3d VectorCross(Point3d point)
        {
            return new Point3d((this.Y*point.Z)-(this.Z*point.Y), -(this.X*point.Z)+(this.Z*point.X), (this.X*point.Y)-(this.Y*point.X));
        }

        public Point3d VectorAverage(Point3d point)
        {
            return new Point3d((this.X+point.X)/2, (this.Y+point.Y)/2, (this.Z+point.Z)/2);
        }
 
        public static Point3d operator +(Point3d a, Point3d b)
        {
            return new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
 
        public static Point3d operator -(Point3d a, Point3d b)
        {
            return new Point3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
 
        public static Point3d operator *(Point3d a, double b)
        {
            return new Point3d(a.X * b, a.Y * b, a.Z * b);
        }
 
        public static Point3d operator /(Point3d a, double b)
        {
            return new Point3d(a.X / b, a.Y / b, a.Z / b);
        }
    }
}
