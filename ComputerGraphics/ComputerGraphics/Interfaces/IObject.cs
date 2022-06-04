using ComputerGraphics.Types;
using System.Collections.Generic;
using ComputerGraphics.Tree;
using ComputerGraphics.Materials;

namespace ComputerGraphics.Interfaces
{
    public interface IObject : ITransformation
    {
        public bool IsIntersection(Point start,Vector direction);

        public Point WhereIntercept(Point start, Vector direction);

        public Vector GetNormal(Point point);

        public Point getCoordsofMin();
        public Point getCoordsofMax();

        public bool IsInBox(Box box);

        public Color color { get; set; }

        public void SetColor(int r, int g, int b, int mirror);
    }
}