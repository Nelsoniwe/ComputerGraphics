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

        public static Vector GetVectorWithPoints(Point p1, Point p2)
        {
            return new Vector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
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

        public static Vector Normilize(Vector vector)
        {
            var mag = vector.Magnitude();
            if (mag >0)
            {
                return new Vector(vector.X / mag, vector.Y / mag, vector.Z / mag);
            }
            return vector;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator +(Vector v1, Point v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator *(double v1, Vector v2)
        {
            return new Vector(v1 * v2.X, v1 * v2.Y, v1 * v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        

        public static float operator *(Vector v1, Vector v2)
        {
            return (float)(v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }
        
        public static Vector Cross(Vector u, Vector v)
        {
            return new Vector(
                u.Y * v.Z - u.Z * v.Y,
                u.Z * v.X - u.X * v.Z,
                u.X * v.Y - u.Y * v.X);
        }

        public static double Len(Vector u)
        {
            return Math.Sqrt(u * u);
        }
    }
}
