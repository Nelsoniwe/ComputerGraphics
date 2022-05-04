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
            Camera camera = new Camera(new Point(0, 0, -10), new Vector(0, 0, 1), 100, 100);
            Scene.Scene scene = new Scene.Scene(camera);
            Sphere sphere = new Sphere(new Point(0, -2, 10), 2);
            scene.AddObject(sphere);
            Sphere sphere2 = new Sphere(new Point(0, 2, 10), 2);
            scene.AddObject(sphere2);

            double[,] screen = scene.getScreenArray();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (screen[i,j] == 1)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
