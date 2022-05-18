using ComputerGraphics.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Objects
{
    public class Light
    {
        public Light(Vector vector)
        {
            this.Vector = Vector.Normilize(vector);
        }

        public Vector Vector { get; private set; }

        public void SetLight(Vector newVector)
        {
            Vector = Vector.Normilize(newVector);
        }
    }
}
