using ComputerGraphics.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Objects
{
    public class Light
    {
        private Vector vector;

        public Light(Vector vector)
        {
            this.vector = Vector.Normilize(vector);
        }

        public Vector Vector
        {
            get { return vector; }
        }

        public void setLight(Vector newVector)
        {
            vector = Vector.Normilize(newVector);
        }
    }
}
