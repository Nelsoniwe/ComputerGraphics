using ComputerGraphics.Interfaces;
using System;

namespace ComputerGraphics.Types
{
    public class Point: ITransformation
    {
        private float x;
        private float y;
        private float z;

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X
        {
            get { return this.x; }
            set { x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { y = value; }
        }
        public float Z
        {
            get { return this.z; }
            set { z = value; }
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


        public static float Distance(Point pointOne, Point pointTwo)
        {
            float deltaX = pointTwo.X - pointOne.X;
            float deltaY = pointTwo.Y - pointOne.Y;
            float deltaZ = pointTwo.Z - pointOne.Z;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
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

        public object RotateY(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);
            float newX = (float)(x * Math.Cos(degree) + z * Math.Sin(degree));
            float newZ = (float)(z * Math.Cos(degree) - x * Math.Sin(degree));
            x = newX;
            z = newZ;
            return this;
        }

        public object RotateZ(float degree)
        {
            degree = (float)(degree * Math.PI / 180.0);

            float newX = (float)(x * Math.Cos(degree) - y * Math.Sin(degree));
            float newY = (float)(y * Math.Cos(degree) + x * Math.Sin(degree));
            x = newX;
            y = newY;
            return this;
        }

        public object Translate(Vector direction)
        {
            x += direction.X;
            y += direction.Y;
            z += direction.Z;
            return this;
        }

        public object Scale(float kx, float ky, float kz)
        {
            x *= kx;
            y *= ky;
            z *= kz;
            return this;
        }
    }
}
