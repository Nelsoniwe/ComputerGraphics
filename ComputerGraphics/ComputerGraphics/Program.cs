using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using ComputerGraphics.Scene;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Utility;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            ////4k 3840 x 2160 pixel
            ////int width = 1280;
            ////int height = 720;

            //int width = 500;
            //int height = 500;

            //Camera camera = new Camera(new Point(0, 0.2f, -1), new Vector(0, 0, 1), height, width);
            //Scene.Scene scene = new Scene.Scene(camera);

            ////Sphere sphere = new Sphere(new Point(4, -2, 100), 4);
            ////scene.AddObject(sphere);

            ////Sphere sphere4 = new Sphere(new Point(5, 0, 0), 1);
            ////scene.AddObject(sphere4);
            ////Sphere sphere5 = new Sphere(new Point(0, 5, 0), 1);
            ////scene.AddObject(sphere5);

            ////Sphere sphere6 = new Sphere(new Point(0, 0, -20), 1);
            ////scene.AddObject(sphere6);

            ////Sphere sphere2 = new Sphere(new Point(2, 2, 10), 2);
            ////scene.AddObject(sphere2);
            ////Sphere sphere3 = new Sphere(new Point(4, -2, 8), 2);
            ////scene.AddObject(sphere3);

            //Light light = new Light(new Vector(-1, 1, 1));
            //scene.AddLight(light);

            ////Triangle triangle = new Triangle(new Point(1, 0, 0), new Point(0, 0, 0), new Point(0, 1, -1),new Vector(1,0,-1), new Vector(1, 0, -1), new Vector(-1, 0, -1));
            ////Triangle triangle = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(0, 0, 0));

            ////scene.AddObject(triangle);

            ////Plane plane = new Plane(new Point(-200, 1, 1), new Vector(1, 0, 0));
            ////scene.AddObject(plane);

            ////string source = @"C:\Users\Denys\Desktop\cow.obj";
            //string source = @"C:\Users\Denys\Desktop\cow.obj";
            //List<IObject> triangles = ObjectFile.ReadObjectFile(source);
            //ObjectsInObject objects = new ObjectsInObject(triangles);
            //scene.AddObjects(triangles);

            //objects.RotateY(90);
            //objects.RotateX(45);
            //objects.Scale(1.3f, 1.3f, 1.3f);


            //Stopwatch s = new Stopwatch();
            //s.Start();

            //float [,] screen = scene.GetScreenArray(50);
            //s.Stop();
            //Console.WriteLine($"Render time: {s.ElapsedMilliseconds}");


            ////string filename = @"C:\Users\Denys\Desktop\task.ppm"; //destination
            //string filename = @"C:\Users\lenovo\OneDrive\Рабочий стол\compGraph\task.ppm"; //destination
            //StreamWriter destination = new StreamWriter(filename);
            //destination.Write("P3\n{0} {1} {2}\n", width, height, 255);
            //destination.Flush();

            //StringBuilder output = new StringBuilder("");
            //for (int i = 0; i < height; i++)
            //{
            //    for (int j = 0; j < width; j++)
            //    {
            //        int a = Convert.ToInt32(screen[i, j] * 255);
            //        if (screen[i, j] == 0)  //if no interception
            //            a = 255;

            //        if (a > 255)
            //            a = 255;
            //        if (a < 0)
            //            a = 0;

            //        output.Append(a);
            //        output.Append(" ");
            //        output.Append(a);
            //        output.Append(" ");
            //        output.Append(a);
            //        output.Append(" ");
            //    }
            //    destination.WriteLine(output);
            //    output.Clear();

            //}

            //destination.Close();



            string source = @"C:\Users\Denys\Desktop\cow.obj";
            List<IObject> triangles = ObjectFile.ReadObjectFile(source);
            ObjectsInObject objects = new ObjectsInObject(triangles);
            Tree.Tree tree = new Tree.Tree(triangles);
            Console.WriteLine($"{tree.mainBox.min.X},{tree.mainBox.min.Y},{tree.mainBox.min.Z}");
            Console.WriteLine($"{tree.mainBox.max.X},{tree.mainBox.max.Y},{tree.mainBox.max.Z}");
            Console.WriteLine(tree.mainBox.getBoxesCount());
            Console.WriteLine(tree.mainBox.getobjectsCount());
        }
        

    }
}
