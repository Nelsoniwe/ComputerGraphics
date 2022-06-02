using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.IO;
using System.Text;
using ComputerGraphics.Scene;
using System.Diagnostics;

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

            Camera camera = new Camera(new Point(1, 0, -2f), new Vector(0, 0, 1), height, width);
            Scene.Scene scene = new Scene.Scene(camera);

            Light light = new Light(Vector.Normilize(new Vector(-1, 0, 1)));
            scene.AddLight(light);

            Plane plane = new Plane(new Point(-1, 0, 0), new Vector(1, 0, 0));
            scene.AddObject(plane);

            //string source = @"C:\Users\Denys\Desktop\asdadsa.obj";
            //string source = @"C:\Users\Denys\Desktop\cow.obj";
            //List<IObject> triangles = ObjectFile.ReadObjectFile(source);
            //ObjectsInObject objects = new ObjectsInObject(triangles);
            //objects.RotateZ(0);
            //objects.RotateX(0);
            //objects.RotateY(90);
            //scene.AddObjects(triangles);

            scene.MakeTree();
            Console.WriteLine("tree created");

            Stopwatch s = new Stopwatch();
            s.Start();

            float[,] screen = scene.GetScreenArray(1);
            s.Stop();
            Console.WriteLine($"Render time: {s.ElapsedMilliseconds}");

            string filename = @"C:\Users\Denys\Desktop\task.ppm"; //destination
            //string filename = @"C:\Users\lenovo\OneDrive\Рабочий стол\compGraph\task.ppm"; //destination
            StreamWriter destination = new StreamWriter(filename);
            destination.Write("P3\n{0} {1} {2}\n", width, height, 255);
            destination.Flush();

            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int a = Convert.ToInt32(screen[i, j] * 255);
                    if (screen[i, j] == 0)  //if no interception
                        a = 255;

                    if (a > 255)
                        a = 255;
                    if (a < 0)
                        a = 0;

                    output.Append(a);
                    output.Append(" ");
                    output.Append(a);
                    output.Append(" ");
                    output.Append(a);
                    output.Append(" ");
                }
                destination.WriteLine(output);
                output.Clear();

            }

            destination.Close();
        }
    }
}
