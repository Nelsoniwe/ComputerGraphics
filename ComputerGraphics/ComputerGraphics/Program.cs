using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {

            Sphere sphere = new Sphere(new Point(1, 1, 1), 1);
            Vector vector = new Vector(0, 0, 1);
            Console.WriteLine(sphere.IsIntersection(new Point(1, 0, 1), vector));
        }
    }
}
