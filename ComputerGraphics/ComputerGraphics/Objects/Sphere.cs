using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Tree;
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

            if (a == 0)
                throw new ArgumentException(); //TODO

            var t = (-b - Math.Sqrt(D)) / (2 * a);

            if (t<0)
                throw new ArgumentException("No interception there");

            var x = (float)(start.X + t * direction.X);
            var y = (float)(start.Y + t * direction.Y);
            var z = (float)(start.Z + t * direction.Z);
            return new Point(x, y, z);
        }

        public Vector GetNormal(Point point)
        {
            return Vector.GetVectorWithPoints(center,point);
        }

        public object Translate(Vector direction)
        {
            center = (Point)center.Translate(direction);
            return this;
        }

        public object RotateX(float degree)
        {
            return this;
        }

        public object RotateY(float degree)
        {
            return this;
        }

        public object RotateZ(float degree)
        {
            return this;
        }

        public object Scale(float kx, float ky, float kz)
        {
            radius = kx;
            return this;
        }

        public Point getCoordsofMin()
        {
            return new Point(center.X-radius, center.Y-radius, center.Z-radius);
        }

        public Point getCoordsofMax()
        {
            return new Point(center.X + radius, center.Y + radius, center.Z + radius);
        }

        public bool IsInBox(Box box)
        {
            Point min = getCoordsofMin();
            Point max = getCoordsofMax();

            return (min.X <= box.max.X && max.X >= box.min.X) &&
                   (min.Y <= box.max.Y && max.Y >= box.min.Y) &&
                   (min.Z <= box.max.Z && max.Z >= box.min.Z);
        }
    }
}
