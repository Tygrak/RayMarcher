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
            scene.DrawScene().Save("output.png");
        }
    }
}