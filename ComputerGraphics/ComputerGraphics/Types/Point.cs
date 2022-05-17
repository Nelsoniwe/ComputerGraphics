using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ComputerGraphics.Types
{
    public class Point
    {
        private double x;
        private double y;
        private double z;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double X
        {
            get { return this.x; }
        }
        public double Y
        {
            get { return this.y; }
        }
        public double Z
        {
            get { return this.z; }
        }

        public static Vector operator +(Point point, Vector vector)
        {
            return new Vector(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Vector operator -(Point point, Vector vector)
        {
            return new Vector(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
        }

        public static Vector operator -(Point pointOne, Point pointTwo)
        {
            return new Vector(pointOne.X - pointTwo.X, pointOne.Y - pointTwo.Y, pointOne.Z - pointTwo.Z);
        }

        public static double Distance(Point pointOne, Point pointTwo)
        {
            double deltaX = pointTwo.X - pointOne.X;
            double deltaY = pointTwo.Y - pointOne.Y;
            double deltaZ = pointTwo.Z - pointOne.Z;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }
}
