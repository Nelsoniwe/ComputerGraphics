using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.IO;
using System.Text;
using ComputerGraphics.Scene;
using System.Diagnostics;
using ComputerGraphics.Interfaces;
using System.Collections.Generic;
using ComputerGraphics.Utility;
using ComputerGraphics.Materials;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            ////4k 3840 x 2160 pixel
            int width = 1280;
            int height = 720;

            //int width = 500;
            //int height = 500;

            Camera camera = new Camera(new Point(0, 0, -2.1f), new Vector(0, 0, 1), height, width);
            Scene.Scene scene = new Scene.Scene(camera);

            Light light = new Light(Vector.Normilize(new Vector(-1, 0, 1)));
            scene.AddLight(light);

            Plane plane = new Plane(new Point(-0.6f, 0, 0), new Vector(1, 0, 0));
            plane.SetColor(248, 252, 239, 0);
            scene.AddObject(plane);

            //Sphere sphere = new Sphere(new Point(1,1,1), 1);
            //sphere.SetColor(255, 255, 136, 0);
            //scene.AddObject(sphere);

            var source = @"C:\Users\lenovo\Downloads\Telegram Desktop\cow.obj";
            List<IObject> triangles = ObjectFile.ReadObjectFile(source);
            ObjectsInObject objects = new ObjectsInObject(triangles);
            objects.RotateY(90);
            objects.RotateX(45);
            objects.Scale(2f, 2f, 2f);
            objects.SetColor(147, 112, 219, 99);
            scene.AddObjects(triangles);

            scene.MakeTree();
            Console.WriteLine("tree created");

            Stopwatch s = new Stopwatch();
            s.Start();

            Color[,] screen = scene.GetScreenArray(50);
            s.Stop();
            Console.WriteLine($"Render time: {s.ElapsedMilliseconds}");

            //string filename = @"C:\Users\Denys\Desktop\task.ppm"; //destination
            string filename = @"C:\Users\lenovo\OneDrive\Рабочий стол\compGraph\task1.ppm"; //destination
            StreamWriter destination = new StreamWriter(filename);
            destination.Write("P3\n{0} {1} {2}\n", width, height, 255);
            destination.Flush();

            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    
                    output.Append(screen[i, j].R);
                    output.Append(" ");
                    output.Append(screen[i, j].G);
                    output.Append(" ");
                    output.Append(screen[i, j].B);
                    output.Append(" ");
                }
                destination.WriteLine(output);
                output.Clear();

            }

            destination.Close();
        }
    }
}
