using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.Threading.Channels;
using ComputerGraphics.Scene;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            Camera camera = new Camera(new Point(0, 0, -10), new Vector(0, 0, 1), 50, 211);
            Scene.Scene scene = new Scene.Scene(camera);
            Sphere sphere = new Sphere(new Point(-2, -2, 100), 4);
            scene.AddObject(sphere);
            Sphere sphere2 = new Sphere(new Point(0, 2, 10), 2);
            scene.AddObject(sphere2);
            Light light = new Light(new Vector(0.5, 0.5, 0.7));
            scene.AddLight(light);

            Plane plane = new Plane(new Point(2, 1, 1), new Vector(1, 0, 0));
            scene.AddObject(plane);

            double[,] screen = scene.getScreenArray();

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 211; j++)
                {
                    if (screen[i, j] <= 0)
                    {
                        Console.Write(' ');
                    }
                    else if (screen[i, j] > 0 && screen[i, j] <= 0.2)
                    {
                        Console.Write('.');
                    }
                    else if (screen[i, j] > 0.2 && screen[i, j] <= 0.3)
                    {
                        Console.Write('-');
                    }
                    else if (screen[i, j] > 0.3 && screen[i, j] <= 0.4)
                    {
                        Console.Write(':');
                    }
                    else if (screen[i, j] > 0.4 && screen[i, j] <= 0.5)
                    {
                        Console.Write('=');
                    }
                    else if (screen[i, j] > 0.5 && screen[i, j] <= 0.6)
                    {
                        Console.Write('+');
                    }
                    else if (screen[i, j] > 0.6 && screen[i, j] <= 0.7)
                    {
                        Console.Write('*');
                    }
                    else if (screen[i, j] > 0.7 && screen[i, j] <= 0.8)
                    {
                        Console.Write('#');
                    }
                    else
                        Console.Write('@');

                }
                Console.WriteLine();
            }

           
        }
    }
}
