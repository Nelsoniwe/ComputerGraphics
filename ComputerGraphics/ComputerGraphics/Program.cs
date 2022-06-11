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

            Camera camera = new Camera(new Point(0, 0, -2.1f), new Vector(0, 0, 1), height, width,60);
            Scene.Scene scene = new Scene.Scene(camera);

            Light light = new Light(Vector.Normilize(new Vector(-1, 0.6f, 0.8f)),20);
            scene.AddLight(light);

            Light light2 = new Light(Vector.Normilize(new Vector(-1, -0.6f, 0.8f)), 20);
            scene.AddLight(light2);

            Plane plane = new Plane(new Point(-0.6f, 0, 0), new Vector(1, 0, 0));
            plane.SetColor(255, 255, 255, 50);
            scene.AddObject(plane);

            Sphere sphere = new Sphere(new Point(1, -1.5f, 1), 1);
            sphere.SetColor(255, 255, 136, 50);
            scene.AddObject(sphere);

            //var source = @"C:\Users\lenovo\Downloads\Telegram Desktop\cow.obj";

            string source = @"C:\Users\Denys\Desktop\cow.obj"; //destination
            List<IObject> triangles = ObjectFile.ReadObjectFile(source);
            ObjectsInObject objects = new ObjectsInObject(triangles);
            objects.RotateY(90);
            objects.RotateX(45);
            objects.Scale(2f, 2f, 2f);
            objects.SetColor(147, 112, 219, 0);
            scene.AddObjects(triangles);

            scene.MakeTree();


            Console.WriteLine("tree created");

            Stopwatch s = new Stopwatch();
            s.Start();

            Color[,] screenTree = scene.GetScreenArrayTree(50);
            s.Stop();

            Console.WriteLine($"Render time with tree:: {s.ElapsedMilliseconds}");

            s.Restart();
            Color[,] screen = scene.GetScreenArray(50);
            s.Stop();

            Console.WriteLine($"Render time without tree: {s.ElapsedMilliseconds}");


            string filename = @"C:\Users\Denys\Desktop\task.ppm"; //destination
            string filename1 = @"C:\Users\Denys\Desktop\task1.ppm"; //destination
            WriteImage(filename, screenTree);
            WriteImage(filename1, screen);
        }

        static void WriteImage(string filename, Color[,] screen)
        {
            //string filename = @"C:\Users\lenovo\OneDrive\Рабочий стол\compGraph\task1.ppm"; //destination
            StreamWriter destination = new StreamWriter(filename);
            destination.Write("P3\n{0} {1} {2}\n", screen.GetUpperBound(1), screen.GetUpperBound(0), 255);
            destination.Flush();

            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < screen.GetUpperBound(0); i++)
            {
                for (int j = 0; j < screen.GetUpperBound(1); j++)
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
