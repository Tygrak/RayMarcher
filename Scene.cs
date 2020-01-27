using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
 
namespace RayMarcher
{
    class Scene
    {
        const double renderDistance = 600;

        public List<ISdfObject> objects = new List<ISdfObject>() {};
        public double GlobalIllumination = 0;
        public Point3d GlobalLight = new Point3d(0.53, -1, 0.1);

        public double DistanceFromScene(Point3d point)
        {
            if (objects.Count == 0) return 100000;
            double min = point.DistanceFromSdfObject(objects[0]);
            for (int i = 1; i < objects.Count; i++)
            {
                min = Math.Min(min, point.DistanceFromSdfObject(objects[i]));
            }
            return min;
        }

        public ISdfObject ClosestObject(Point3d point)
        {
            if (objects.Count == 0) return null;
            ISdfObject closest = objects[0];
            double min = point.DistanceFromSdfObject(objects[0]);
            for (int i = 1; i < objects.Count; i++)
            {
                double dist = point.DistanceFromSdfObject(objects[i]);
                if (min > dist)
                {
                    min = dist;
                    closest = objects[i];
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

        public double LambertNdotL(Point3d normal, Point3d light)
        {
            return Math.Min(Math.Max(normal.VectorDot(light), 0), 1);
        }

        public Bitmap DrawScene()
        {
            Point3d camera = new Point3d(0, 0, -2);
            return DrawScene(600, 600, camera);
        }
 
        public Bitmap DrawScene(int width, int height, Point3d cameraPosition)
        {
            Bitmap bmp = new Bitmap(width, height);
            Point3d camera = cameraPosition;
            GlobalLight = GlobalLight.VectorNormalize();
            for (int y = 0; y < bmp.Size.Height; y++)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    Point3d viewPoint = new Point3d((x-bmp.Size.Width/2) / (bmp.Size.Height*0.5) + camera.X, 
                        (y-bmp.Size.Height/2) / (bmp.Size.Height*0.5) + camera.Y,
                        1 + camera.Z);
                    (double distance, int steps, Point3d hitPoint) = RayMarch(camera, viewPoint, renderDistance);
                    //Console.WriteLine(distance);
                    if (distance >= renderDistance-0.5)
                    {
                        //Color color = Color.FromArgb(Math.Min(steps, 250), Math.Min(steps, 250), Math.Min(steps, 250));
                        Color color = Color.FromArgb(0, 0, 0);
                        bmp.SetPixel(x, y, color);
                    }
                    else
                    {
                        //Color color = ColorFromPointFakeShadows(hitPoint);
                        Color color = ColorFromPointShadows(hitPoint);
                        bmp.SetPixel(x, y, color);
                    }
                }
            }
            return bmp;
        }

        public Color ColorFromPointFakeShadows(Point3d hitPoint)
        {
            ISdfObject sdfObject = ClosestObject(hitPoint);
            Point3d normal = new Point3d(
                (hitPoint + new Point3d(0.001, 0, 0)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0.001, 0, 0)).DistanceFromSdfObject(sdfObject),
                (hitPoint + new Point3d(0, 0.001, 0)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0, 0.001, 0)).DistanceFromSdfObject(sdfObject),
                (hitPoint + new Point3d(0, 0, 0.001)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0, 0, 0.001)).DistanceFromSdfObject(sdfObject)
            ).VectorNormalize();
            double NdotL = LambertNdotL(normal, GlobalLight);
            NdotL = NdotL > GlobalIllumination ? NdotL : GlobalIllumination;
            return Color.FromArgb((int) (sdfObject.ObjectColor.R*NdotL), (int) (sdfObject.ObjectColor.G*NdotL), (int) (sdfObject.ObjectColor.B*NdotL));
        }

        public Color ColorFromPointShadows(Point3d hitPoint)
        {
            ISdfObject sdfObject = ClosestObject(hitPoint);
            Point3d normal = new Point3d(
                (hitPoint + new Point3d(0.001, 0, 0)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0.001, 0, 0)).DistanceFromSdfObject(sdfObject),
                (hitPoint + new Point3d(0, 0.001, 0)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0, 0.001, 0)).DistanceFromSdfObject(sdfObject),
                (hitPoint + new Point3d(0, 0, 0.001)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0, 0, 0.001)).DistanceFromSdfObject(sdfObject)
            ).VectorNormalize();
            double NdotL = LambertNdotL(normal, GlobalLight);
            Point3d lightPoint = hitPoint+(GlobalLight)*renderDistance;
            (double distance, int steps, Point3d lightHitPoint) = RayMarch(lightPoint, hitPoint, renderDistance*2);
            double light = (hitPoint-lightHitPoint).VectorLength();
            light = (1-Math.Min(light, 1.0))/1;
            return Color.FromArgb((int) (sdfObject.ObjectColor.R*light*NdotL), (int) (sdfObject.ObjectColor.G*light*NdotL), (int) (sdfObject.ObjectColor.B*light*NdotL));
        }
    }
}