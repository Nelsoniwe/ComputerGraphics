using ComputerGraphics.Types;
using System.Collections.Generic;

namespace ComputerGraphics.Interfaces
{
    public interface IObject : ITransformation
    {
        public bool IsIntersection(Point start,Vector direction);

        public Point WhereIntercept(Point start, Vector direction);

        public Vector GetNormal(Point point);

        
    }
}