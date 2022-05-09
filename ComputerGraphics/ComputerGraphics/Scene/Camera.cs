using System;
using ComputerGraphics.Types;

namespace ComputerGraphics.Scene
{
    public class Camera
    {
        private Point position;
        private Vector direction;
        private double[,] screen;

        public Camera(Point position,Vector direction, int xRes, int yRes)
        {
            this.position = position;
            this.direction = Vector.Normilize(direction);
            screen = new double[xRes, yRes];
        }

        public void RefreshScreen()
        {
            Array.Clear(screen, 0, screen.Length);
        }

        public double[,] Screen
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