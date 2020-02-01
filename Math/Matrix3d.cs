using System;

namespace RayMarcher{
    public struct Matrix3d
    {
        public readonly double X1;
        public readonly double X2;
        public readonly double X3;
        public readonly double Y1;
        public readonly double Y2;
        public readonly double Y3;
        public readonly double Z1;
        public readonly double Z2;
        public readonly double Z3;

        public Matrix3d(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3) 
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
            Y1 = y1;
            Y2 = y2;
            Y3 = y3;
            Z1 = z1;
            Z2 = z2;
            Z3 = z3;
        }
 
        public Matrix3d(Point3d x, Point3d y, Point3d z)
        {
            X1 = x.X;
            X2 = x.Y;
            X3 = x.Z;
            Y1 = y.X;
            Y2 = y.Y;
            Y3 = y.Z;
            Z1 = z.X;
            Z2 = z.Y;
            Z3 = z.Z;
        }

        public static Matrix3d operator +(Matrix3d a, Matrix3d b)
        {
            return new Matrix3d(a.X1 + b.X1, a.X2 + b.X2, a.X3 + b.X3, 
                                a.Y1 + b.Y1, a.Y2 + b.Y2, a.Y3 + b.Y3, 
                                a.Z1 + b.Z1, a.Z2 + b.Z2, a.Z3 + b.Z3);
        }
 
        public static Matrix3d operator -(Matrix3d a, Matrix3d b)
        {
            return new Matrix3d(a.X1 - b.X1, a.X2 - b.X2, a.X3 - b.X3, 
                                a.Y1 - b.Y1, a.Y2 - b.Y2, a.Y3 - b.Y3, 
                                a.Z1 - b.Z1, a.Z2 - b.Z2, a.Z3 - b.Z3);
        }
 
        public static Matrix3d operator *(double a, Matrix3d b)
        {
            return new Matrix3d(a * b.X1, a * b.X2, a * b.X3, a * b.Y1, a * b.Y2, a * b.Y3, a * b.Z1, a * b.Z2, a * b.Z3);
        }
 
        public static Matrix3d operator *(Matrix3d a, Matrix3d b)
        {
            return new Matrix3d(a.X1*b.X1+a.X2*b.Y1+a.X3*b.Z1, a.X1*b.X2+a.X2*b.Y2+a.X3*b.Z2, a.X1*b.X3+a.X2*b.Y3+a.X3*b.Z3,
                                a.Y1*b.X1+a.Y2*b.Y1+a.Y3*b.Z1, a.Y1*b.X2+a.Y2*b.Y2+a.Y3*b.Z2, a.Y1*b.X3+a.Y2*b.Y3+a.Y3*b.Z3,
                                a.Z1*b.X1+a.Z2*b.Y1+a.Z3*b.Z1, a.Z1*b.X2+a.Z2*b.Y2+a.Z3*b.Z2, a.Z1*b.X3+a.Z2*b.Y3+a.Z3*b.Z3);
        }

        public static Point3d operator *(Matrix3d a, Point3d b)
        {
            return new Point3d(a.X1*b.X+a.X2*b.Y+a.X3*b.Z, a.Y1*b.X+a.Y2*b.Y+a.Y3*b.Z, a.Z1*b.X+a.Z2*b.Y+a.Z3*b.Z);
        }

        public override string ToString()
        {
            return $"({X1}, {X2}, {X3}\n {Y1}, {Y2}, {Y3}\n {Z1}, {Z2}, {Z3})";
        } 
    }
}
