using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
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
            scene.objects.Add(new Box(new Point3d(0, 40, 60), new Point3d(20, 20, 20), Color.Red));
            scene.GlobalIllumination = 1;
            /*scene.objects.Add(
                new SdfIntersection(new HalfSpace(new Point3d(20, 0, 0), new Point3d(1, 0, 0), Color.MediumSeaGreen),
                new SdfIntersection(new HalfSpace(new Point3d(-20, 0, 0), new Point3d(-1, 0, 0), Color.MediumSeaGreen),
                new SdfIntersection(new HalfSpace(new Point3d(0, 60, 0), new Point3d(0, -1, 0), Color.MediumSeaGreen), 
                new SdfIntersection(new HalfSpace(new Point3d(0, 70, 0), new Point3d(0,  1, 0), Color.MediumSeaGreen),
                new SdfIntersection(new HalfSpace(new Point3d(0, 0, 60), new Point3d(0, 0, 1), Color.MediumSeaGreen), 
                                    new HalfSpace(new Point3d(0, 0, 50), new Point3d(0, 0, -1), Color.MediumSeaGreen)))))));*/
            /*for (int i = 0; i < 120; i++)
            {
                scene.DrawScene(800, 600, new Point3d(0, 0, -2+(15.0*i/60.0))).Save($"output/output{i}.png");
            }*/
            scene.DrawScene().Save("output.png");
        }
    }
}