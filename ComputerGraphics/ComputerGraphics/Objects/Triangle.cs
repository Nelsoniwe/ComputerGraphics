using System;
using System.Collections.Generic;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Objects
{
    public class Triangle : IObject, ITransformation
    {
        private Point point1;
        private Point point2;
        private Point point3;
        private Vector vector1;
        private Vector vector2;
        private Vector vector3;

        public Triangle(Point point1, Point point2, Point point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }

        public Triangle(Point point1, Point point2, Point point3, Vector vector1, Vector vector2, Vector vector3) : this(point1, point2, point3)
        {
            this.vector1 = vector1;
            this.vector2 = vector2;
            this.vector3 = vector3;
        }

        public Point getCoordsofMax()
        {
            float maxX = point1.X;
            float maxY = point1.Y;
            float maxZ = point1.Z;
            Point[] points = { point2, point3 };

            for (int i = 0; i < 2; i++)
            {
                if (points[i].X > maxX)
                    maxX = points[i].X;
                if (points[i].Y > maxY)
                    maxY = points[i].Y;
                if (points[i].Z > maxZ)
                    maxZ = points[i].Z;
            }

            return new Point(maxX, maxY, maxZ);
        }

        public Point getCoordsofMin()
        {
            float minX = point1.X;
            float minY = point1.Y;
            float minZ = point1.Z;
            Point[] points = {point2,point3};

            for (int i = 0; i < 2; i++)
            {
                if (points[i].X < minX)
                    minX = points[i].X;
                if (points[i].Y < minY)
                    minY = points[i].Y;
                if (points[i].Z < minZ)
                    minZ = points[i].Z;
            }

            return new Point(minX, minY, minZ);
        }

        public Vector GetNormal(Point point)
        {

            if (vector1 != null && vector2 != null && vector3 != null)
            {
                var s1 = Vector.Len(point1 - point);
                var s2 = Vector.Len(point2 - point);
                var s3 = Vector.Len(point3 - point);
                var s_sum = s1 + s2 + s3;

                s1 = s1 / s_sum;
                s2 = s2 / s_sum;
                s3 = s3 / s_sum;

                var b = (s1 * vector1) + (s2 * vector2) + (s3 * vector3);
                return b;
            }

            var e1 = point2 - point1;
            var e2 = point3 - point1;
            var a = Vector.Normilize(Vector.Cross(e1, e2));
            return a;
        }

        public bool IsIntersection(Point start, Vector direction)
        {
            var e1 = point2 - point1;
            var e2 = point3 - point1;

            var p = Vector.Cross(direction, e2);
            var det = e1 * p;

            if (det == 0)
            {
                return false;
            }
            else
            {
                var inv_det = 1 / det;
                var T = start - point1;

                var u = T * p * inv_det;
                if (u < 0 || u > 1)
                {
                    return false;
                }
                else
                {
                    var q = Vector.Cross(T, e1);
                    var v = direction * q * inv_det;
                    if (v < 0 || u + v > 1)
                    {
                        return false;
                    }
                    else
                    {
                        var t = e2 * q * inv_det;
                        return true;
                    }
                }
            }
        }

        public object RotateX(float degree)
        {
            point1.RotateX(degree);
            point2.RotateX(degree);
            point3.RotateX(degree);
            vector1.RotateX(degree);
            vector2.RotateX(degree);
            vector3.RotateX(degree);
            return this;
        }

        public object RotateY(float degree)
        {
            point1.RotateY(degree);
            point2.RotateY(degree);
            point3.RotateY(degree);
            vector1.RotateY(degree);
            vector2.RotateY(degree);
            vector3.RotateY(degree);
            return this;
        }

        public object RotateZ(float degree)
        {
            
            point1.RotateZ(degree);
            point2.RotateZ(degree);
            point3.RotateZ(degree);
            vector1.RotateZ(degree);
            vector2.RotateZ(degree);
            vector3.RotateZ(degree);
            return this;
        }

        public object Scale(float kx, float ky, float kz)
        {
            point1.Scale(kx, ky, kz);
            point2.Scale(kx, ky, kz);
            point3.Scale(kx, ky, kz);
            return this;
        }

        public object Translate(Vector direction)
        {
            point1.Translate(direction);
            point2.Translate(direction);
            point3.Translate(direction);
            return this;
        }

        public Point WhereIntercept(Point start, Vector direction)
        {
            var e1 = point2 - point1;
            var e2 = point3 - point1;

            var p = Vector.Cross(direction, e2);
            var det = e1 * p;

            if (det == 0)
            {
                throw new ArgumentException("No interception there");
            }
            else
            {
                var inv_det = 1 / det;
                var T = start - point1;

                var u = T * p * inv_det;
                if (u < 0 || u > 1)
                {
                    throw new ArgumentException("No interception there");
                }
                else
                {
                    var q = Vector.Cross(T, e1);
                    var v = direction * q * inv_det;
                    if (v < 0 || u + v > 1)
                    {
                        throw new ArgumentException("No interception there");
                    }
                    else
                    {
                        var t = e2 * q * inv_det;

                        var x = start.X + t * direction.X;
                        var y = start.Y + t * direction.Y;
                        var z = start.Z + t * direction.Z;
                        return new Point(x, y, z);
                    }
                }
            }
        }
    }
}