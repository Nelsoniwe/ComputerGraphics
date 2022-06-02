using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Tree
{
    public class Tree
    {
        public Box mainBox;
        public Tree(List<IObject> objects)
        {
            Point min = new Point(0,0,0);
            Point max = new Point(0, 0, 0);
            if (objects.Count != 0)
            {
                min = objects[0].getCoordsofMin();
                max = objects[0].getCoordsofMax();
            }
            

            for (int i = 1; i < objects.Count; i++)
            {
                var cordsObjMin = objects[i].getCoordsofMin();
                var cordsObjMax = objects[i].getCoordsofMax();

                if (min.X > cordsObjMin.X)
                    min.X = cordsObjMin.X;
                if (min.Y > cordsObjMin.Y)
                    min.Y = cordsObjMin.Y;
                if (min.Z > cordsObjMin.Z)
                    min.Z = cordsObjMin.Z;

                if (max.X < cordsObjMax.X)
                    max.X = cordsObjMax.X;
                if (max.Y < cordsObjMax.Y)
                    max.Y = cordsObjMax.Y;
                if (max.Z < cordsObjMax.Z)
                    max.Z = cordsObjMax.Z;
            }


            mainBox = new Box(min, max, objects);

            mainBox.DivideBoxes();
        }

        public List<IObject> GetInterceptionObject(Point origin, Vector direction)
        {
            List<IObject> interceptedObjects = new List<IObject>();
            if (mainBox.IsIntersection(origin,direction))
            {
                GetInterceptionObjectRecursive(mainBox, origin, direction,ref interceptedObjects);
            }

            return interceptedObjects;
        }

        private void GetInterceptionObjectRecursive(Box box, Point origin, Vector direction,ref List<IObject> interceptedObjects)
        {
            Queue<Box> boxesQueue = new Queue<Box>();
            foreach (var item in box.boxes)
            {
                if (item.IsIntersection(origin, direction))
                {
                    boxesQueue.Enqueue(item);
                }
            }

            if (box.objects.Count > 0)
            {
                foreach (var item in box.objects)
                {
                    if (item.IsIntersection(origin,direction))
                    {
                        if (!interceptedObjects.Contains(item))
                            interceptedObjects.Add(item);
                    }
                }
            }
            
            while (boxesQueue.Count > 0)
            {
                Box queueValue = boxesQueue.Dequeue();
                GetInterceptionObjectRecursive(queueValue, origin, direction,ref interceptedObjects);
            }
        }
    }
}
