using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Tree
{
    public class Box
    {
        public List<Box> boxes = new List<Box>();
        public List<IObject> objects = new List<IObject>();

        public Point min;
        public Point max;

        public Box(Point min, Point max)
        {
            this.min = min;
            this.max = max;
        }

        public Box(Point min, Point max, List<IObject> objects)
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

            if (tmin > tmax)
            {
                t = tmax;
                return false;
            }

            t = tmin;
            return true;
        }

        public void DivideBoxes()
        {
            Box b1 = new Box(new Point(min.X, min.Y, min.Z), new Point((min.X + max.X) / 2f, (min.Y + max.Y) / 2f, (min.Z + max.Z) / 2f));
            Box b2 = new Box(new Point(min.X, (min.Y + max.Y) / 2f, min.Z), new Point((min.X + max.X) / 2f, max.Y, (min.Z + max.Z) / 2f));
            Box b3 = new Box(new Point(min.X, (min.Y + max.Y) / 2f, (min.Z + max.Z) / 2f), new Point((min.X + max.X) / 2f, max.Y, max.Z));
            Box b4 = new Box(new Point(min.X, min.Y, (min.Z + max.Z) / 2f), new Point((min.X + max.X) / 2f, (min.Y + max.Y) / 2f, max.Z));

            Box b5 = new Box(new Point((min.X + max.X) / 2f, min.Y, min.Z), new Point(max.X, (min.Y + max.Y) / 2f, (min.Z + max.Z) / 2f));
            Box b6 = new Box(new Point((min.X + max.X) / 2f, (min.Y + max.Y) / 2f, min.Z), new Point(max.X, max.Y, (min.Z + max.Z) / 2f));
            Box b7 = new Box(new Point((min.X + max.X) / 2f, (min.Y + max.Y) / 2f, (min.Z + max.Z) / 2f), new Point(max.X, max.Y, max.Z));
            Box b8 = new Box(new Point((min.X + max.X) / 2f, min.Y, (min.Z + max.Z) / 2f), new Point(max.X, (min.Y + max.Y) / 2f, max.Z));

            List<Box> tempBoxesList = new List<Box>() { b1, b2, b3, b4, b5, b6, b7, b8 };
            foreach (var obj in objects)
            {
                foreach (var box in tempBoxesList)
                {
                    if (obj.IsInBox(box))
                    {
                        box.objects.Add(obj);
                    }
                }
            }

            foreach (var box in tempBoxesList)
            {
                if (box.objects.Count > 0)
                {
                    boxes.Add(box);
                }
            }

            foreach (var box in boxes)
            {
                if (!(box.objects.Count < 20 || box.objects.Count >= objects.Count))
                {
                    box.DivideBoxes();
                }
            }
            objects.Clear();
        }
    }
}