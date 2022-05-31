using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Tree
{
    class Tree
    {
        public Box mainBox;
        public Tree(List<IObject> objects)
        {
            Point min = objects[0].getCoordsofMin();
            Point max = objects[0].getCoordsofMax();

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

            mainBox = new Box(min, max,objects);
        }
    }
}
