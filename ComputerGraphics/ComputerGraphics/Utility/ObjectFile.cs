using ComputerGraphics.Interfaces;
using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ComputerGraphics.Utility
{
    public static class ObjectFile
    {
        public static List<IObject> ReadObjectFile(string path)
        {
            StreamReader sourceFile = new StreamReader(path);
            List<Point> points = new List<Point>();
            List<Vector> normals = new List<Vector>();
            List<IObject> objects = new List<IObject>();

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
                            float.Parse(lineArray[1], CultureInfo.InvariantCulture),
                            float.Parse(lineArray[2], CultureInfo.InvariantCulture),
                            float.Parse(lineArray[3], CultureInfo.InvariantCulture)));
                    else if (lineArray[0] == "vn")
                        normals.Add(new Vector(
                            float.Parse(lineArray[1], CultureInfo.InvariantCulture),
                            float.Parse(lineArray[2], CultureInfo.InvariantCulture),
                            float.Parse(lineArray[3], CultureInfo.InvariantCulture)));
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

                        objects.Add(new Triangle(
                            new Point(points[pointCoord[0] - 1].X, points[pointCoord[0] - 1].Y, points[pointCoord[0] - 1].Z),
                            new Point(points[pointCoord[1] - 1].X, points[pointCoord[1] - 1].Y, points[pointCoord[1] - 1].Z),
                            new Point(points[pointCoord[2] - 1].X, points[pointCoord[2] - 1].Y, points[pointCoord[2] - 1].Z),
                            new Vector(normals[vectorCoord[0] - 1].X, normals[vectorCoord[0] - 1].Y, normals[vectorCoord[0] - 1].Z),
                            new Vector(normals[vectorCoord[1] - 1].X, normals[vectorCoord[1] - 1].Y, normals[vectorCoord[1] - 1].Z),
                            new Vector(normals[vectorCoord[2] - 1].X, normals[vectorCoord[2] - 1].Y, normals[vectorCoord[2] - 1].Z)));
                    }

                }
            }
            return objects;
        }

    }
}
