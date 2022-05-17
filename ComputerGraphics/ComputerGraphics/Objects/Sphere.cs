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

            if (D < 0)
                return false;
            var t = (-b - Math.Sqrt(D)) / (2 * a);

            if (t >= 0)
                return true;
            return false;
        }

        public Point WhereIntercept(Point start, Vector direction)
        {
            Vector k = start - center; //+
            var a = direction * direction; //+
            var b = 2 * (direction * k);//+
            var c = k * k - radius * radius;//+
            var D = b * b - 4 * a * c;

            if (D < 0)
                throw new ArgumentException("No interception there");

            var t = (-b - Math.Sqrt(D)) / (2 * a);

            if (t<0)
                throw new ArgumentException("No interception there");

            var x = start.X + t * direction.X;
            var y = start.Y + t * direction.Y;
            var z = start.Z + t * direction.Z;
            return new Point(x, y, z);
        }

        public Vector GetNormal(Point point)
        {
            return Vector.GetVectorWithPoints(center,point);
        }
    }
}
