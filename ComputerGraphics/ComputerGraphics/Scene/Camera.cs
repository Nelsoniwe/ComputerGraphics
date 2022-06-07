using System;
using ComputerGraphics.Materials;
using ComputerGraphics.Types;

namespace ComputerGraphics.Scene
{
    public class Camera
    {
        private Point position;
        private Vector direction;
        private Color[,] screen;
        private Vector planeOrigin;

        private int height;
        private int width;

        public Vector GetPlaneOrigin()
        {
            return planeOrigin;
        }

        public float RealPlaneHeight { get; }

        public float RealPlaneWidht { get; }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value > 0)
                    width = value;
                else
                    throw new ArgumentException();
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value > 0)
                    height = value;
                else
                    throw new ArgumentException();
            }
        }

        public Camera(Point position, Vector direction, int height, int width,int fov)
        {
            planeOrigin = position + Vector.Normilize(direction);
            this.Height = height;
            this.Width = width;

            var fovInRad = fov / 180f * Math.PI;
            RealPlaneHeight = (float)Math.Tan(fovInRad);
            RealPlaneWidht = RealPlaneHeight / Height * Width;

            this.position = position;
            this.direction = Vector.Normilize(direction);
            screen = new Color[height, width];
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