using ComputerGraphics.Types;

namespace ComputerGraphics.Interfaces
{
    public interface IObject
    {
        public bool IsIntersection(Point start,Vector direction);
    }
}