using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Objects
{
    public class Sphere: IObject
    {
        private Point center;

        public Sphere(Point center)
        {
            this.center = center;
        }

        //TODO
        public bool IsIntersection(Point start, Vector direction)
        {
            throw new NotImplementedException();
        }
    }
}
