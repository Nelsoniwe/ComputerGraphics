using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Types
{
    public class Vector
    {
        private double x;
        private double y;
        private double z;

        public Vector(double x, double y, double z)
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

        public float Magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public static float operator *(Vector v1, Vector v2)
        {
            return (float)(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1.X != v2.X && v1.Y != v2.Y && v1.Z != v2.Z);
        }
    }
}
