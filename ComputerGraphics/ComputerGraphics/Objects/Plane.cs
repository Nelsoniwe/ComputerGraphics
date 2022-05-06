using System;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Objects
{
    public class Plane : IObject
    {
        private Point center;
        private Vector normal;

        public Plane(Point center, Vector vector)
        {
            this.center = center;
            this.normal = Vector.Normilize(vector);
        }
        public bool IsIntersection(Point start, Vector direction)
        {
            if (direction * normal == 0)
            {
                return false;
            }
            Vector k = start - center;
            var t = -((k * normal) / (direction * normal));

            if (t > 0)
            {
                return true;
            }

            return false;
        }

        public Point WhereIntercept(Point start, Vector direction)
        {
            if (direction * normal == 0)
            {
                throw new ArgumentException("No interception there");
            }
            Vector k = start - center;
            var t = -((k * normal) / (direction * normal));

            if (t < 0)
                throw new ArgumentException("No interception there");

            var x = start.X + t * direction.X;
            var y = start.Y + t * direction.Y;
            var z = start.Z + t * direction.Z;
            return new Point(x, y, z);
        }

        public Vector GetNormal(Point point)
        {
            return normal + new Vector(point.X, point.Y, point.Z);
        }
    }
}