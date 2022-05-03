using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {

            //Sphere sphere = new Sphere(new Point(1, 1, 1), 1);
            //Vector vector = new Vector(0, 0, 1);
            //Console.WriteLine(sphere.IsIntersection(new Point(1, 0, 1), vector));

            //Vector v = new Vector(0, 100, 100);
            //v = Vector.Normilize(v);
            //Console.WriteLine(v.X);
            //Console.WriteLine(v.Y);
            //Console.WriteLine(v.Z);

            Vector v = new Vector(0, 1, 1);
            Plane a = new Plane(new Point(0, 0, 0), new Vector(0, 1, 0));
            Console.WriteLine(a.IsIntersection(new Point(0, -4, 0), v));
            
        }
    }
}
