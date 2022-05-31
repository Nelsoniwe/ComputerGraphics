using ComputerGraphics.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Interfaces
{
    public interface ITransformation
    {
        public object RotateX(float degree);

        public object RotateY(float degree);

        public object RotateZ(float degree);

        public object Scale(float kx, float ky, float kz);

        public object Translate(Vector direction);
    }
}
