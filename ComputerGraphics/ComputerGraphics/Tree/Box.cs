using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Tree
{
    class Box : IObject
    {
        private List<IObject> objects;

        private float minX;
        private float minZ;
        private float minY;

        private float maxX;
        private float maxZ;
        private float maxY;

        public Box(List<IObject> objects)
        {
            this.objects = objects;


        }

        public int getobjectsCount()
        {
            return objects.Count;
        }

        public Vector GetNormal(Point point)
        {
            throw new NotImplementedException();
        }

        public bool IsIntersection(Point start, Vector direction)
        {
            throw new NotImplementedException();
        }

        public Point WhereIntercept(Point start, Vector direction)
        {
            throw new NotImplementedException();
        }
    }
}
