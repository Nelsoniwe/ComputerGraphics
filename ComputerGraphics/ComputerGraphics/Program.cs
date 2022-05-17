using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using ComputerGraphics.Scene;
using System.Collections.Generic;
using System.Globalization;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            //4k 3840 x 2160 pixel
            //int width = 1280;
            //int height = 720;

            int width = 500;
            int height = 500;

            Camera camera = new Camera(new Point(2, 0, -450), new Vector(0, 0, 1), height, width);
            Scene.Scene scene = new Scene.Scene(camera);
            //Sphere sphere = new Sphere(new Point(4, -2, 100), 4);
            //scene.AddObject(sphere);
            //Sphere sphere2 = new Sphere(new Point(2, 2, 10), 2);
            //scene.AddObject(sphere2);
            //Sphere sphere3 = new Sphere(new Point(4, -2, 8), 2);
            //scene.AddObject(sphere3);
            
            Light light = new Light(new Vector(-1, 1, 1));
            scene.AddLight(light);

            //Triangle triangle = new Triangle(new Point(1, 0, 0), new Point(0, 0, 0), new Point(0, 1, -1),new Vector(1,0,-1), new Vector(1, 0, -1), new Vector(-1, 0, -1));
            //Triangle triangle = new Triangle(new Point(0, 1, 0), new Point(-1, 0, 0), new Point(0, 0, 0));

            //scene.AddObject(triangle);

            Plane plane = new Plane(new Point(-200, 1, 1), new Vector(1, 0, 0));
            scene.AddObject(plane);
            string source = @"C:\Users\lenovo\Downloads\Telegram Desktop\task.obj";
            List<Triangle> triangles = ReadObjectFile(source);

            for (int i = 0; i < triangles.Count; i++)
            {
                scene.AddObject(triangles[i]);
            }

            double[,] screen = scene.getScreenArray();

            Console.WriteLine("render done");

            string filename = @"C:\Users\lenovo\OneDrive\Рабочий стол\compGraph\task.ppm"; //destination
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

                Console.SetCursorPosition(0, 0);
                Console.Write(Convert.ToInt32(i));
                Console.Write("/");
                Console.Write(height);
            }

            destination.Close();
            Console.WriteLine("file created");
        }

        public static List<Triangle> ReadObjectFile(string path)
        {
            StreamReader sourceFile = new StreamReader(path);
            List<Point> points = new List<Point>();
            List<Vector> normals = new List<Vector>();
            List<Triangle> triangles = new List<Triangle>();

            using (sourceFile)
            {
                string line;
                while ((line = sourceFile.ReadLine()) != null)
                {
                    if (line == "")
                        continue;
                    string[] lineArray = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (lineArray[0] == "v")
                        points.Add(new Point(
                            Double.Parse(lineArray[1], CultureInfo.InvariantCulture),
                            Double.Parse(lineArray[2], CultureInfo.InvariantCulture),
                            Double.Parse(lineArray[3], CultureInfo.InvariantCulture)));
                    else if (lineArray[0] == "vn")
                        normals.Add(new Vector(
                            Double.Parse(lineArray[1], CultureInfo.InvariantCulture),
                            Double.Parse(lineArray[2], CultureInfo.InvariantCulture),
                            Double.Parse(lineArray[3], CultureInfo.InvariantCulture)));
                    else if (lineArray[0] == "f")
                    {
                        int[] pointCoord = new int[3];
                        int[] vectorCoord = new int[3];
                        for (int i = 1; i < lineArray.Length; i++)
                        {
                            string[] coordinates = lineArray[i].Split("/");
                            pointCoord[i - 1] = Int32.Parse(coordinates[0]);
                            vectorCoord[i - 1] = Int32.Parse(coordinates[2]);
                        }

                        triangles.Add(new Triangle(
                            points[pointCoord[0] - 1],
                            points[pointCoord[1] - 1],
                            points[pointCoord[2] - 1],
                            normals[vectorCoord[0] - 1],
                            normals[vectorCoord[1] - 1],
                            normals[vectorCoord[2] - 1]
                            ));
                    }

                }
            }
            return triangles;
        }

    }
}
