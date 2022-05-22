using ComputerGraphics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Types
{
    public class Vector: ITransformation
    {
        private float x;
        private float y;
        private float z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector GetVectorWithPoints(Point p1, Point p2)
        {
            return new Vector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
        }

        public float X
        {
            get { return this.x; }
        }
        public float Y
        {
            get { return this.y; }
        }
        public float Z
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

        public static Vector operator *(float v1, Vector v2)
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

        public static float Len(Vector u)
        {
            return (float)(Math.Sqrt(u * u));
        }

        public static Vector Negate(Vector u)
        {
            return new Vector(-u.X, -u.Y, -u.Z);
        }

        public object RotateX(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);
            float newY = (float)(y * Math.Cos(degree) - z * Math.Sin(degree));
            float newZ = (float)(y * Math.Sin(degree) + z * Math.Cos(degree));
            y = newY;
            z = newZ;
            return this;

        }

        public object RotateZ(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);
            float newX = (float)(x * Math.Cos(degree) + z * Math.Sin(degree));
            float newZ = (float)(z * Math.Cos(degree) - x * Math.Sin(degree));
            x = newX;
            z = newZ;
            return this;
        }

        public object RotateY(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);

            float newX = (float)(x * Math.Cos(degree) - y * Math.Sin(degree));
            float newY = (float)(y * Math.Cos(degree) + x * Math.Sin(degree));
            x = newX;
            y = newY;
            return this;
        }

        public object Scale(float kx, float ky, float kz)
        {
            x *= kx;
            y *= ky;
            z *= kz;
            return this;
        }

        public object Translate(Vector direction)
        {
            x += direction.X;
            y += direction.Y;
            z += direction.Z;
            return this;
        }
    }
}
