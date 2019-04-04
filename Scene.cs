using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
 
namespace RayMarcher
{
    class Scene
    {
        const double renderDistance = 200;
        public List<Sphere> spheres = new List<Sphere>() { new Sphere(new Point3d(2, -1, 5), 0.9, Color.Red),
            new Sphere(new Point3d(1, 2, 7), 1, Color.Blue), new Sphere(new Point3d(-1, 2, 2), 1.1, Color.Green), 
            new Sphere(new Point3d(-5, -4, 13), 1.7, Color.Purple), new Sphere(new Point3d(2, 21, 58), 28, Color.Orange),
            new Sphere(new Point3d(-40, 36, 198), 108, Color.Yellow)};
 
        public double DistanceFromScene(Point3d point)
        {
            if (spheres.Count == 0) return 100000;
            double min = point.DistanceFromSphere(spheres[0]);
            for (int i = 1; i < spheres.Count; i++)
            {
                min = Math.Min(min, point.DistanceFromSphere(spheres[i]));
            }
            return min;
        }

        public Sphere ClosestObject(Point3d point)
        {
            if (spheres.Count == 0) return null;
            Sphere closest = spheres[0];
            double min = point.DistanceFromSphere(spheres[0]);
            for (int i = 1; i < spheres.Count; i++)
            {
                double dist = point.DistanceFromSphere(spheres[i]);
                if (min > dist)
                {
                    min = dist;
                    closest = spheres[i];
                }
            }
            return closest;
        }
 
        public (double distance, int steps, Point3d finalPoint) RayMarch(Point3d x, Point3d y, double maxDist)
        {
            return RayMarch(x, y, maxDist, 0.0001);
        }

        public (double distance, int steps, Point3d finalPoint) RayMarch(Point3d x, Point3d y, double maxDist, double cutoff)
        {
            Point3d vector = (y - x).VectorNormalize();
            Point3d currPoint = x;
            double dist = 0;
            int steps = 0;
            while (dist < maxDist)
            {
                double pointDist = DistanceFromScene(currPoint);
                dist += pointDist;
                currPoint += vector * pointDist;
                steps++;
                if (pointDist < cutoff) break;
            }
            return (dist, steps, currPoint);
        }

        public Point3d RayMarchToObject(Point3d x, Point3d y, double maxDist, Sphere sphere)
        {
            return RayMarchToObject(x, y, maxDist, sphere, 0.0001);
        }

        public Point3d RayMarchToObject(Point3d x, Point3d y, double maxDist, Sphere sphere, double cutoff)
        {
            Point3d vector = (y - x).VectorNormalize();
            Point3d currPoint = x;
            double dist = 0;
            int steps = 0;
            while (dist < maxDist)
            {
                double pointDist = currPoint.DistanceFromSphere(sphere);
                dist += pointDist;
                currPoint += vector * pointDist;
                steps++;
                if (pointDist < cutoff) break;
            }
            return currPoint;
        }

        public double LambertNdotL(Point3d normal, Point3d light)
        {
            return Math.Min(Math.Max(normal.VectorDot(light), 0), 1);
        }
 
        public Bitmap DrawScene()
        {
            Point3d light = new Point3d(0.03, -1, 0.1);
            Bitmap bmp = new Bitmap(1000, 1000);
            Point3d camera = new Point3d(0, 0, -1);
            for (int y = bmp.Size.Height-1; y >= 0; y--)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    Point3d viewPoint = new Point3d((x-bmp.Size.Width/2) / (bmp.Size.Width/2d) + camera.X, 
                        (y-bmp.Size.Height/2) / (bmp.Size.Height/2d) + camera.Y,
                        1 + camera.Z);
                    (double distance, int steps, Point3d hitPoint) = RayMarch(camera, viewPoint, renderDistance);
                    if (distance >= renderDistance-0.5)
                    {
                        //Color color = Color.FromArgb(Math.Min(steps, 250), Math.Min(steps, 250), Math.Min(steps, 250));
                        Color color = Color.FromArgb(0, 0, 0);
                        bmp.SetPixel(x, y, color);
                    }
                    else
                    {
                        Sphere sphere = ClosestObject(hitPoint);
                        Point3d lNormalPoint = (sphere.Point - hitPoint-new Point3d(-0.0001, 0, 0));
                        Point3d rNormalPoint = (sphere.Point - hitPoint-new Point3d(0.0001, 0, 0));
                        Point3d uNormalPoint = (sphere.Point - hitPoint-new Point3d(0, 0.0001, 0));
                        Point3d dNormalPoint = (sphere.Point - hitPoint-new Point3d(0, -0.0001, 0));
                        Point3d normal = new Point3d(
                            (hitPoint + new Point3d(0.001, 0, 0)).DistanceFromSphere(sphere) - (hitPoint - new Point3d(0.001, 0, 0)).DistanceFromSphere(sphere),
                            (hitPoint + new Point3d(0, 0.001, 0)).DistanceFromSphere(sphere) - (hitPoint - new Point3d(0, 0.001, 0)).DistanceFromSphere(sphere),
                            (hitPoint + new Point3d(0, 0, 0.001)).DistanceFromSphere(sphere) - (hitPoint - new Point3d(0, 0, 0.001)).DistanceFromSphere(sphere)
                        ).VectorNormalize();
                        double NdotL = LambertNdotL(normal, light);
                        Color color = Color.FromArgb((int) (sphere.Color.R*NdotL), (int) (sphere.Color.G*NdotL), (int) (sphere.Color.B*NdotL));
                        //Console.WriteLine(NdotL);
                        double sphereDist = camera.DistanceFromSphere(sphere);
                        bmp.SetPixel(x, y, color);
                    }
                }
            }
            return bmp;
        }
    }
}