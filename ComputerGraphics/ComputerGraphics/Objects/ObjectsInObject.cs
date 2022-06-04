using ComputerGraphics.Interfaces;
using ComputerGraphics.Materials;
using ComputerGraphics.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Objects
{
    public class ObjectsInObject : ITransformation
    {
        private List<IObject> objects;

        public Color color { get; set; }

        public ObjectsInObject(List<IObject> objects)
        {
            this.objects = objects;
            color = new Color(255, 255, 255, 0);
        }

        public void SetColor(int r, int g, int b, int mirror)
        {
            foreach(var obj in objects)
            {
                obj.color = new Color(r, g, b, mirror);
            }
        }

        public object RotateX(float degree)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].RotateX(degree);
            }
            return this;
        }

        public object RotateY(float degree)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].RotateY(degree);
            }
            return this;
        }

        public object RotateZ(float degree)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].RotateZ(degree);
            }
            return this;
        }

        public object Scale(float kx, float ky, float kz)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i] = (IObject)objects[i].Scale(kx, ky, kz);
            }
            return this;
        }

        public object Translate(Vector direction)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Translate(direction);
            }
            return this;
        }
    }
}
