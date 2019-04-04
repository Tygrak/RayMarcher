using System;
using System.Drawing;

//A lot of these are taken from: http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
//Some stuff from here: https://www.alanzucconi.com/2016/07/01/signed-distance-functions/

namespace RayMarcher{
    public interface ISdfObject
    {
        Color ObjectColor{ get; set; }
        double DistanceFromPoint(Point3d point);   
    }
}