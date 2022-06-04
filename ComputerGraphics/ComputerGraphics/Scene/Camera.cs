using System;
using ComputerGraphics.Materials;
using ComputerGraphics.Types;

namespace ComputerGraphics.Scene
{
    public class Camera
    {
        private Point position;
        private Vector direction;
        private Color [,] screen;

        public Camera(Point position,Vector direction, int height, int weight)
        {
            this.position = position;
            this.direction = Vector.Normilize(direction);
            screen = new Color[height, weight];
        }

        public void RefreshScreen()
        {
            Array.Clear(screen, 0, screen.Length);
        }

        public Color[,] Screen
        {
            get
            {
                return screen;
            }
        }
        public Vector Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public Point Position
        {
            get
            {
                return position;
            }
        }

    }
}