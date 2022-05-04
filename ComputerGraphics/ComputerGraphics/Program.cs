using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.Threading.Channels;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {

            Sphere sphere = new Sphere(new Point(0, 1, 5), 1);
            Vector vector = new Vector(0, 0, 1);


            Console.WriteLine(sphere.IsIntersection(new Point(0,0,0),vector));

            var f = sphere.WhereIntercept(new Point(0, 0, 0), vector);
            Console.WriteLine(f.X);
            Console.WriteLine(f.Y);
            Console.WriteLine(f.Z);

        }
    }
}
