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
            float denom = -(normal * direction);

            if (denom > 0)
            {
                Vector k = center - start;
                var t = -(k * normal) / denom;
                return (t >= 0);
            }
            
            return false;
        }

        public Point WhereIntercept(Point start, Vector direction)
        {
            float denom = -(normal * direction);

            if (denom < 0)
                throw new ArgumentException("No interception there");

            Vector k = center - start;
            var t = -(k * normal) / denom;

            if (t < 0)
                throw new ArgumentException("No interception there");

            var x = start.X + t * direction.X;
            var y = start.Y + t * direction.Y;
            var z = start.Z + t * direction.Z;

            return new Point(x, y, z);
        }

        public Vector GetNormal(Point point)
        {
            return normal;
        }
    }
}