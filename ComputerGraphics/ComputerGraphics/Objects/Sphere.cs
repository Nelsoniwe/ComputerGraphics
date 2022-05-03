using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Objects
{
    public class Sphere : IObject
    {
        private Point center;
        private float radius;

        public Sphere(Point center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public float Radius
        {
            get { return radius; }
        }

        public bool IsIntersection(Point start, Vector direction)
        {
            Vector k = start - center; //+
            var a = direction * direction; //+
            var b = 2 * (direction * k);//+
            var c = k * k - radius * radius;//+
            var D = b * b - 4 * a * c;
            return D >= 0;
        }
    }
}
