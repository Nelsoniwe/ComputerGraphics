using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Tree
{
    class Box
    {
        private List<Box> boxes = new List<Box>();
        private List<IObject> objects = new List<IObject>();

        public Point min;
        public Point max;

        public Box(Point min, Point max)
        {
            this.min = min;
            this.max = max;
        }

        public Box(Point min, Point max,List<IObject> objects)
        {
            this.min = min;
            this.max = max;
            this.objects = objects;
        }

        public Box(Point min, Point max, List<Box> boxes)
        {
            this.min = min;
            this.max = max;
            this.boxes = boxes;
        }

        public int getBoxesCount()
        {
            return boxes.Count;
        }

        public int getobjectsCount()
        {
            return objects.Count;
        }

        public bool IsIntersection(Point start, Vector direction)
        {
            var invDirection = new Vector(1 / direction.X, 1 / direction.Y, 1 / direction.Z);
            direction = invDirection;

            float t1 = (min.X - start.X) * direction.X;
            float t2 = (max.X - start.X) * direction.X;
            float t3 = (min.Y - start.Y) * direction.Y;
            float t4 = (max.Y - start.Y) * direction.Y;
            float t5 = (min.Z - start.Z) * direction.Z;
            float t6 = (max.Z - start.Z) * direction.Z;

            float tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            float tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            float t;
            if (tmax < 0)
            {
                t = tmax;
                return false;
            }

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                t = tmax;
                return false;
            }

            t = tmin;
            return true;
        }

        //TODO
        public void DivideBoxes()
        {

        }
    }
}
