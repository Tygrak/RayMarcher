using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using RayMarcher.SdfFunctions;
using RayMarcher.SdfObjects;
//using System.Drawing.Drawing2D;

namespace RayMarcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();
            //scene.objects.Add(new SdfRepetition(new Sphere(new Point3d(-2, 0, 0), 0.5, Color.Blue), new Point3d(0, 0, 6)));
            //scene.objects.Add(new SdfRepetition(new Sphere(new Point3d(0, -2, 0), 0.5, Color.Green), new Point3d(0, 0, 6)));
            //scene.objects.Add(new SdfRepetition(new Sphere(new Point3d(0, 2, 0), 0.5, Color.White), new Point3d(0, 0, 6)));
            //scene.objects.Add(new SdfRepetition(new Sphere(new Point3d(2, 0, 0), 0.5, Color.Red), new Point3d(0, 0, 6)));
            /*scene.objects.Add(new Box(new Point3d(10, 5, 30), 10, Color.FromArgb(202, 88, 98)));
            scene.objects.Add(new Box(new Point3d(-11, 7, 25), 10, Color.FromArgb(202, 88, 98)));
            scene.objects.Add(new Box(new Point3d(-18, 2, 38), 10, Color.FromArgb(202, 88, 98)));*/
            /*scene.objects.Add(new SdfTranslation(new SdfRepetition(
                    new SdfSmoothUnion(
                    new SdfSmoothSubtraction(
                    new SdfSmoothUnion(
                    new SdfRotation(new Box(10, Color.FromArgb(202, 88, 98)), new Point3d(0, 1.2, 0)), new Sphere(new Point3d(3,1,1), 9), 3.8),
                    new Sphere(new Point3d(0, -5, 0), 6), 3),
                    new Sphere(new Point3d(0, -14, 0), 3), 13),
                new Point3d(50, 0, 50)), new Point3d(-3, 4, 75)));*/
            //scene.objects.Add(new SdfTranslation(new Box(10), new Point3d(-10, 19, -30)));
            scene.objects.Add(new Box(new Point3d(0, 25, 60), new Point3d(2000, 20, 2000), Color.FromArgb(102, 28, 98)));
            /*scene.objects.Add(new SdfTranslation(
                new SdfRepetition(
                new SdfSubtraction(
                    new SdfElongation(new Sphere(0.5, Color.BlueViolet), new Point3d(1, 6, 6)),
                    new Sphere(5.25)
                ), new Point3d(18, 0, 18)), 
                new Point3d(9, 0, -10)
            ));*/   
            Sphere targetSphere = new Sphere(new Point3d(0, 0, 0), 5.0, Color.BlueViolet);
            scene.objects.Add(new SdfSmoothUnion(
                new SdfUnion(new Sphere(new Point3d(-40, 0, 0), 10.0, Color.BlueViolet), 
                             new Sphere(new Point3d(40, 0, 0), 10.0, Color.BlueViolet)), 
                targetSphere, 5));
            scene.GlobalIllumination = 0.125;
            scene.GlobalLight = new Point3d(-0.67, -1, -0.56);
            scene.CameraPosition = new Point3d(0, 0, -10);
            scene.CameraRotation = new Point3d(0, 0, 0);
            /*for (int i = 0; i < 720; i++)
            {
                targetSphere.Point = new Point3d(Math.Sin(Math.PI*(i/60.0))*32.5, 0, 0);
                scene.CameraPosition = new Point3d(55*Math.Sin(Math.PI*(i/360.0)), 0, -55*Math.Cos(Math.PI*(i/360.0)));
                scene.CameraRotation = new Point3d(0, -Math.PI*(i/360.0), 0);
                scene.DrawScene(800, 600).Save($"output/output{i}.png");
                if (i > 0 && i%5 == 0) {
                    Console.WriteLine($"Created {i}. image");
                    Console.WriteLine($"Image rendered in {scene.LastRunTime}ms ({scene.LastRunTime/1000.0}s)");
                }
            }*/
            //Console.WriteLine($"Done creating {tasks.Count} images!");
            scene.DrawScene(800, 600).Save("output.png");
            Console.WriteLine($"Scene rendered in {scene.LastRunTime}ms ({scene.LastRunTime/1000.0}s)");
        }
    }
}