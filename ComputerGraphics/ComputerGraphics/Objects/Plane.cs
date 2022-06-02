using System;
using System.Collections.Generic;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Tree;
using ComputerGraphics.Types;

namespace ComputerGraphics.Objects
{
    public class Plane : IObject, ITransformation
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

        public object RotateX(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);
            center.RotateX(degree);
            normal.RotateX(degree);
            return this;
        }

        public object RotateY(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);
            center.RotateY(degree);
            normal.RotateY(degree);
            return this;
        }

        public object RotateZ(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);
            center.RotateZ(degree);
            normal.RotateZ(degree);
            return this;
        }

        public object Scale(float kx, float ky, float kz)
        {
            return this;
        }

        public object Translate(Vector direction)
        {
            center.Translate(direction);
            normal.Translate(direction);
            return this;
        }

        public Point getCoordsofMin()
        {
            return center;
        }

        public Point getCoordsofMax()
        {
            return center;
        }

        public bool IsInBox(Box box)
        {
            return true;
        }
    }
}