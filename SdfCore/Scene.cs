using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
 
namespace RayMarcher
{
    class Scene
    {
        const double renderDistance = 600;

        public List<ISdfObject> objects = new List<ISdfObject>() {};
        public double GlobalIllumination = 0;
        public Point3d GlobalLight = new Point3d(0.53, -1, 0.1);
        public Point3d CameraPosition = new Point3d();
        public Point3d CameraRotation = new Point3d();
        public double CameraFocalLength = 0;
        public long LastRunTime {get {return lastRunTime;}}

        private long lastRunTime;
        private Matrix3d cameraRotationMatrix = new Matrix3d();

        private double DistanceFromScene(Point3d point)
        {
            if (objects.Count == 0) return 100000;
            double min = point.DistanceFromSdfObject(objects[0]);
            for (int i = 1; i < objects.Count; i++)
            {
                min = Math.Min(min, point.DistanceFromSdfObject(objects[i]));
            }
            return min;
        }

        private ISdfObject ClosestObject(Point3d point)
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
 
        private (double distance, int steps, Point3d finalPoint) RayMarch(Point3d x, Point3d y, double maxDist)
        {
            return RayMarch(x, y, maxDist, 0.0001);
        }

        private (double distance, int steps, Point3d finalPoint) RayMarch(Point3d x, Point3d y, double maxDist, double cutoff)
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

        private double LambertNdotL(Point3d normal, Point3d light)
        {
            return Math.Min(Math.Max(normal.VectorDot(light), 0), 1);
        }

        public Bitmap DrawScene()
        {
            Point3d camera = new Point3d(0, 0, -2);
            return DrawScene(600, 600);
        }
 
        public Bitmap DrawScene(int width, int height)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            DirectBitmap bmp = new DirectBitmap(width, height);
            UpdateMatrix();
            GlobalLight = GlobalLight.VectorNormalize();
            Color[] resultImage = new Color[width*height];
            List<Task> pointCalculationTasks = new List<Task>(bmp.Size.Height*bmp.Size.Width);
            for (int y = 0; y < bmp.Size.Height; y++)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    Point3d viewPoint = cameraRotationMatrix*new Point3d(
                        (x-bmp.Size.Width/2) / (bmp.Size.Height*0.5), 
                        (y-bmp.Size.Height/2) / (bmp.Size.Height*0.5),
                        1)+CameraPosition;
                    Point3d cameraPoint = CameraFocalLength > 0 ? cameraRotationMatrix*new Point3d(
                        (x-bmp.Size.Width/2) / (CameraFocalLength*bmp.Size.Height*0.5), 
                        (y-bmp.Size.Height/2) / (CameraFocalLength*bmp.Size.Height*0.5),
                        0)+CameraPosition : CameraPosition;
                    int index = x + (y * bmp.Size.Width);
                    pointCalculationTasks.Add(Task.Run(() => { resultImage[index] = RayMarchSceneViewPoint(cameraPoint, viewPoint); }));
                }
            }
            for (int i = 0; i < pointCalculationTasks.Count; i++)
            {
                pointCalculationTasks[i].Wait();
            }
            bmp.SetAllPixels(resultImage);
            watch.Stop();
            lastRunTime = watch.ElapsedMilliseconds;
            return bmp.Bitmap;
        }

        private Color RayMarchSceneViewPoint (Point3d startPoint, Point3d viewPoint) 
        {
            (double distance, int steps, Point3d hitPoint) = RayMarch(startPoint, viewPoint, renderDistance);
            if (distance >= renderDistance-0.5)
            {
                Color color = Color.FromArgb(0, 0, 0);
                return color;
            }
            else
            {
                Color color = PointToColorRayAndFakeShadows(hitPoint);
                return color;
            }
        }

        private Color PointToColorFakeShadows(Point3d hitPoint)
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

        private Color PointToColorRayShadows(Point3d hitPoint)
        {
            ISdfObject sdfObject = ClosestObject(hitPoint);
            Point3d normal = new Point3d(
                (hitPoint + new Point3d(0.001, 0, 0)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0.001, 0, 0)).DistanceFromSdfObject(sdfObject),
                (hitPoint + new Point3d(0, 0.001, 0)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0, 0.001, 0)).DistanceFromSdfObject(sdfObject),
                (hitPoint + new Point3d(0, 0, 0.001)).DistanceFromSdfObject(sdfObject) - (hitPoint - new Point3d(0, 0, 0.001)).DistanceFromSdfObject(sdfObject)
            ).VectorNormalize();
            Point3d lightPoint = hitPoint+(GlobalLight)*renderDistance;
            (double distance, int steps, Point3d lightHitPoint) = RayMarch(lightPoint, hitPoint, renderDistance*2);
            double light = (hitPoint-lightHitPoint).VectorLength();
            light = (1-Math.Min(light, 1.0))/1;
            light = light > GlobalIllumination ? light : GlobalIllumination;
            return Color.FromArgb((int) (sdfObject.ObjectColor.R*light), (int) (sdfObject.ObjectColor.G*light), (int) (sdfObject.ObjectColor.B*light));
        }

        private Color PointToColorRayAndFakeShadows(Point3d hitPoint)
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
            light = light*NdotL > GlobalIllumination ? light*NdotL : GlobalIllumination;
            return Color.FromArgb((int) (sdfObject.ObjectColor.R*light), (int) (sdfObject.ObjectColor.G*light), (int) (sdfObject.ObjectColor.B*light));
        }

        private void UpdateMatrix() {
            Matrix3d x = new Matrix3d(1, 0, 0, 
                                      0, Math.Cos(CameraRotation.X), -Math.Sin(CameraRotation.X), 
                                      0, Math.Sin(CameraRotation.X), Math.Cos(CameraRotation.X));
            Matrix3d y = new Matrix3d(Math.Cos(CameraRotation.Y), 0, Math.Sin(CameraRotation.Y), 
                                      0, 1, 0, 
                                      -Math.Sin(CameraRotation.Y), 0, Math.Cos(CameraRotation.Y));
            Matrix3d z = new Matrix3d(Math.Cos(CameraRotation.Z), -Math.Sin(CameraRotation.Z), 0, 
                                      Math.Sin(CameraRotation.Z), Math.Cos(CameraRotation.Z), 0, 
                                      0, 0, 1);
            cameraRotationMatrix = z*y*x;
        }
    }
}