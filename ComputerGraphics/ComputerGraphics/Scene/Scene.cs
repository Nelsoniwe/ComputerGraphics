using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using ComputerGraphics.Tree;
using ComputerGraphics.Materials;

namespace ComputerGraphics.Scene
{
    public class Scene
    {
        private List<IObject> objects = new List<IObject>();
        private List<IObject> planes = new List<IObject>();
        private Tree.Tree tree;
        private Camera camera;
        private Light light;

        public Scene(Camera camera)
        {
            this.camera = camera;
        }

        public void AddObject(IObject addObject)
        {
            if (addObject is Plane)
                planes.Add(addObject);
            else
                objects.Add(addObject);

        }

        public void MakeTree()
        {
            List<IObject> copy = new List<IObject>(objects);
            tree = new Tree.Tree(copy);
        }

        public void AddObjects(List<IObject> objectsCollection)
        {
            for (int i = 0; i < objectsCollection.Count; i++)
            {
                objects.Add(objectsCollection[i]);
            }
        }

        public void AddLight(Light light)
        {
            this.light = light;
        }

        public float TheNearest(Vector vector, out IObject obj, out Point intercept)
        {
            float minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].IsIntersection(camera.Position, vector))
                {
                    Point tempIntercept = objects[i].WhereIntercept(camera.Position, vector);
                    float distance = Point.Distance(tempIntercept, camera.Position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        obj = objects[i];
                        intercept = tempIntercept;
                    }
                }
            }
            return minDistance;
        }

        public float TheNearestTree(Vector vector, out IObject obj, out Point intercept)
        {
            float minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            List<IObject> objects = tree.GetInterceptionObject(camera.Position, vector);

            if (objects.Count == 0)
            {
                return Int32.MaxValue;
            }
            for (int i = 0; i < objects.Count; i++)
            {
                Point tempIntercept = objects[i].WhereIntercept(camera.Position, vector);

                float distance = Point.Distance(tempIntercept, camera.Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    obj = objects[i];
                    intercept = tempIntercept;
                }
            }
            return minDistance;
        }

        public float TheNearestTriangleIgnoreFirstTree(Vector vector, Point startPoint, IObject startObject, out IObject obj, out Point intercept)
        {
            float minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            List<IObject> objects = tree.GetInterceptionObject(startPoint, vector);

            if (objects.Count == 0)
            {
                return Int32.MaxValue;
            }
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != startObject)
                {
                    Point tempIntercept = objects[i].WhereIntercept(startPoint, vector);

                    float distance = Point.Distance(tempIntercept, startPoint);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        obj = objects[i];
                        intercept = tempIntercept;
                    }
                }
            }
            return minDistance;
        }

        public Color[,] GetScreenArray(int taskCount)
        {
            camera.RefreshScreen();

            int partPixelCount = camera.Height / taskCount;

            if (camera.Width % taskCount > 0)
            {
                taskCount++;
            }

            Task[] tasks = new Task[taskCount];

            int partPixelCounter = 0;
            for (int i = 0; i < taskCount; i++)
            {
                if (i + 1 == taskCount)
                {
                    partPixelCount = camera.Width - partPixelCounter;
                }

                int j = i;
                int taskPixelStart = partPixelCounter;
                int taskpartPixelCount = partPixelCount;

                tasks[j] = Task.Run(() => GetPartScreenArray(
                    taskPixelStart,
                    taskPixelStart + taskpartPixelCount,
                    camera.Width,
                    camera.Height));

                partPixelCounter += partPixelCount;
            }

            Task.WaitAll(tasks);

            int partCoordinatesCounter = 0;
            for (int i = 0; i < taskCount; i++)
            {
                var part = ((Task<Color[,]>)tasks[i]).Result;

                for (int l = 0; l < part.GetUpperBound(1) + 1; l++)
                {
                    for (int j = 0; j < part.GetUpperBound(0) + 1; j++)
                    {
                        camera.Screen[j, partCoordinatesCounter] = part[j, l];
                    }

                    partCoordinatesCounter++;
                }
            }

            return camera.Screen;
        }

        delegate float GetNearest(Vector vector, out IObject obj, out Point intercept);
        private Color GetColorInterception(Point origin, Vector ray, GetNearest getNearest)
        {
            Color result = new Color(0, 0, 0, 0);
            float minDistance = getNearest(ray, out IObject nearestObj, out Point nearestIntercept);

            if (planes.Count >= 1)
            {
                float planeDist = float.MaxValue;
                foreach (var item in planes)
                {
                    if (item.IsIntersection(camera.Position, ray))
                    {
                        var planeIntercept = item.WhereIntercept(camera.Position, ray);
                        planeDist = Point.Distance(planeIntercept, camera.Position);
                        if (planeDist < minDistance)
                        {
                            minDistance = planeDist;
                            nearestObj = item;
                            nearestIntercept = planeIntercept;
                        }
                    }
                }
            }


            // if no intersection
            if (nearestObj == null)
                return new Color(135, 206, 250, 0);

            if (nearestObj.color.Mirroring != 0)
            {
                MirrorVector(ref nearestObj, ref nearestIntercept, camera.Position, ray, ref result, -(light.Vector * Vector.Normilize(nearestObj.GetNormal(nearestIntercept))));
            }
            else
            {
                var normal = -(light.Vector * Vector.Normilize(nearestObj.GetNormal(nearestIntercept)));
                return new Color(
                    (int)(normal * nearestObj.color.R),
                    (int)(normal * nearestObj.color.G),
                    (int)(normal * nearestObj.color.B),
                    nearestObj.color.Mirroring);
            }


            if (nearestObj==null)
            {
                //mix sky color with intersected objects color
                return new Color(
                            result.R + (int)135 * (100 - result.Mirroring) / 100,
                            result.G + (int)206 * (100 - result.Mirroring) / 100,
                            result.B + (int)250 * (100 - result.Mirroring) / 100,
                            result.Mirroring
                            );
            }
            else
            {
                //
                var minNumber = -(light.Vector * Vector.Normilize(nearestObj.GetNormal(nearestIntercept)));
                return new Color(
                    (int)(minNumber * result.R),
                    (int)(minNumber * result.G),
                    (int)(minNumber * result.B),
                    nearestObj.color.Mirroring);
            }

        }

        public Color[,] GetPartScreenArray(int startWidth, int endWidth, int width, int height)
        {
            //Calculation start point
            Color[,] partScreen = new Color[height, endWidth - startWidth];

            Vector right_vec = Vector.Normilize(Vector.Cross(camera.Direction, Vector.Normilize(new Vector(1, 0, 0))));
            Vector up = Vector.Cross(camera.Direction, Vector.Negate(right_vec));

            var w = camera.Direction;
            var u = Vector.Cross(w, up);
            var v = Vector.Cross(u, w);

            var pixelHeight = camera.RealPlaneHeight / height;
            var pixelWidth = camera.RealPlaneWidht / width;

            var scanlineStart = camera.GetPlaneOrigin() - (width / 2) * pixelWidth * u + startWidth * pixelWidth * u + (pixelWidth / 2) * u +
                (height / 2) * pixelHeight * v - (pixelHeight / 2) * v;
            var scanlineStartPoint = new Point(scanlineStart.X, scanlineStart.Y, scanlineStart.Z);

            var pixelWidthU = pixelWidth * u;
            var pixelHeightV = pixelHeight * v;
            
            //Screen iteration
            for (int x = 0; x < height; x++)
            {
                int yIterator = 0;
                var pixelCenter = new Point(scanlineStartPoint.X, scanlineStartPoint.Y, scanlineStartPoint.Z);

                for (int y = startWidth; y < endWidth; y++)
                {
                    var ray = pixelCenter - camera.Position;

                    Color result = new Color(0, 0, 0, 0);

                    result = GetColorInterception(camera.Position, ray, TheNearestTree);
                    partScreen[x, yIterator] = result;

                    yIterator++;

                    pixelCenter = new Point(pixelCenter.X + pixelWidthU.X, pixelCenter.Y + pixelWidthU.Y, +pixelCenter.Z + pixelWidthU.Z);
                }
                scanlineStartPoint = new Point(scanlineStartPoint.X - pixelHeightV.X, scanlineStartPoint.Y - pixelHeightV.Y, +scanlineStartPoint.Z - pixelHeightV.Z);
            }
            return partScreen;
        }

        public void MirrorVector(ref IObject obj, ref Point intercept, Point origin, Vector direct, ref Color color, float distance)
        {
            color.R += (int)(obj.color.R * (100 - obj.color.Mirroring) / 100 * distance);
            color.G += (int)(obj.color.G * (100 - obj.color.Mirroring) / 100 * distance);
            color.B += (int)(obj.color.B * (100 - obj.color.Mirroring) / 100 * distance);
            color.Mirroring += (100 - obj.color.Mirroring);
            color.Mirroring += 1;

            if (color.Mirroring >= 100)
                return;

            var normal = obj.GetNormal(intercept);
            Vector reflected = direct - 2 * (direct * normal) * normal;

            var minDistance = TheNearestTriangleIgnoreFirstTree(reflected, intercept, obj, out IObject newObject, out Point newIntercept);

            origin = new Point(intercept.X, intercept.Y, intercept.Z);

            obj = newObject;
            intercept = newIntercept;

            if (planes.Count >= 1)
            {
                float planeDist = float.MaxValue;
                foreach (var item in planes)
                {
                    if (item.IsIntersection(origin, reflected))
                    {
                        var planeIntercept = item.WhereIntercept(origin, reflected);
                        planeDist = Point.Distance(planeIntercept, origin);
                        if (planeDist < minDistance)
                        {
                            minDistance = planeDist;
                            obj = item;
                            intercept = planeIntercept;
                        }
                    }
                }
            }


            float minNumber = 0;
            if (obj != null)
                minNumber = -(light.Vector * Vector.Normilize(obj.GetNormal(intercept)));

            if (obj != null && obj.color.Mirroring == 0)
            {
                var mirrorCoef = (100f - color.Mirroring) / 100;
                color.R += (int)(obj.color.R * mirrorCoef);
                color.G += (int)(obj.color.G * mirrorCoef);
                color.B += (int)(obj.color.B * mirrorCoef);
                return;
            }

            if (obj != null && obj.color.Mirroring > 0)
            {
                MirrorVector(ref obj, ref intercept, origin, reflected, ref color, minNumber);
            }
        }
    }
}
