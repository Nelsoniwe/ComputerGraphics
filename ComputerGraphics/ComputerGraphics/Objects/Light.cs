using ComputerGraphics.Types;
using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Materials;

namespace ComputerGraphics.Objects
{
    public class Light : ILight
    {
        public Light(Vector vector, int power)
        {
            Power = power;
            this.Vector = Vector.Normilize(vector);
        }

        public Vector Vector { get; private set; }

        public void SetLight(Vector newVector,int power)
        {
            Power = power;
            Vector = Vector.Normilize(newVector);
        }

        private int power = 0;

        public int Power
        {
            get
            {
                return power;
            }
            set
            {
                if (value > 100)
                    power = 100;
                else if (value < 0)
                    power = 0;
                else
                    power = value;
            }
        }

        public Color Сolor { get; set; }

        public Vector GetDirection(Point point)
        {
            return Vector;
        }

        public Vector GetDirectionToLight(Point point)
        {
            return Vector.Negate(Vector);
        }

        public Color GetShadow(Color originColor)
        {
            return new Color(
                originColor.R * (100 - Power) / 100,
                originColor.G * (100 - Power) / 100,
                originColor.B * (100 - Power) / 100,
                originColor.Mirroring
            );
        }
    }
}
