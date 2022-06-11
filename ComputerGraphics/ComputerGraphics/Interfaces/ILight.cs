using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Materials;
using ComputerGraphics.Types;

namespace ComputerGraphics.Interfaces
{
    public interface ILight
    {
        public int Power { get; set; }
        public Color Сolor { get; set; }

        public Color GetShadow(Color originColor);

        public Vector GetDirection(Point point);
        public Vector GetDirectionToLight(Point point);
    }
}