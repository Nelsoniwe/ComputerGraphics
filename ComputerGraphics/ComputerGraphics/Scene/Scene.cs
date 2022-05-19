using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Objects;
using ComputerGraphics.Types;

namespace ComputerGraphics.Scene
{
    public class Scene
    {
        private List<IObject> Objects = new List<IObject>();
        private Camera camera;
        private Light light;

        public Scene(Camera camera)
        {
            this.camera = camera;
        }

        public void AddObject(IObject addObject)
        {
            Objects.Add(addObject);
        }

        public void AddLight(Light light)
        {
            this.light = light;
        }

        public static double TheNearest(Vector vector, List<IObject> objects, Point camera, out IObject obj, out Point intercept)
        {
            double minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].IsIntersection(camera, vector))
                {
                    Point tempIntercept = objects[i].WhereIntercept(camera, vector);
                    double distance = Point.Distance(tempIntercept, camera);
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

        public double[,] GetScreenArray(int taskCount)
        {
            camera.RefreshScreen();

            var screen = camera.Screen;
            var cameraPos = camera.Position;

            double fov = 60;
            Vector planeOrigin = cameraPos + Vector.Normilize(camera.Direction);

            int width = screen.GetUpperBound(1)+1; //width
            int height = screen.GetUpperBound(0)+1; //height 
            
            var aspectRatio = (float)width / height;

            float distanceToPlaneFromCamera = 1;
            var fovInRad = fov / 180f * Math.PI;
            double realPlaneHeight = (distanceToPlaneFromCamera * Math.Tan(fovInRad));
            double realPlaneWidht = realPlaneHeight / height * width;

            int partPixelCount = height / taskCount;

            if (width % taskCount > 0)
            {
                taskCount++;
            }

            Task[] tasks = new Task[taskCount];

            
            int partPixelCounter = 0;
            for (int i = 0; i < taskCount; i++)
            {
                if (i + 1 == taskCount)
                {
                    partPixelCount = width - partPixelCounter;
                }

                int j = i;
                int taskPixelStart = partPixelCounter;
                int taskpartPixelCount = partPixelCount;

                tasks[j] = Task.Run(() => GetPartScreenArray(
                    planeOrigin,
                    realPlaneWidht,
                    realPlaneHeight,
                    aspectRatio,
                    taskPixelStart,
                    taskPixelStart + taskpartPixelCount,
                    width,
                    height));

                partPixelCounter += partPixelCount;

                //if (i+1== taskCount && partPixelCounter + partPixelCount > height)
                //    partPixelCount = height - partPixelCounter;
            }

            Task.WaitAll(tasks);

            int partCoordinatesCounter = 0;
            for (int i = 0; i < taskCount; i++)
            {
                var part = ((Task<double[,]>)tasks[i]).Result;

                for (int l = 0; l < part.GetUpperBound(1)+1; l++)
                {
                    for (int j = 0; j < part.GetUpperBound(0)+1; j++)
                    {
                        screen[j, partCoordinatesCounter] = part[j, l];
                    }

                    partCoordinatesCounter++;
                }
            }

            return screen;
        }

        public double[,] GetPartScreenArray(
            Vector planeOrigin,
            double realPlaneWidht,
            double realPlaneHeight,
            float aspectRatio,
            int startWidth,
            int endWidth,
            int width,
            int height)
        {
            double[,] partScreen = new double[height, endWidth - startWidth];
            int xIterator = 0;
            int yIterator = 0;
            for (int x = 0; x < height; x++)
            {
                yIterator = 0;
                //var pixelCenter = planePoz;
                for (int y = startWidth; y < endWidth; y++)
                {
                    var xNorm = -(x - width / 2) / (double)width;
                    var yNorm = (y - height / 2) / (double)height;

                    var xOnPlane = xNorm * realPlaneWidht / 2;
                    var yOnPlane = yNorm * realPlaneHeight / 2;

                    Vector positionOnPlane = planeOrigin + new Vector(xOnPlane, yOnPlane, 0);

                    //трасувальний промінь
                    Vector ray = positionOnPlane - new Vector(camera.Position.X, camera.Position.Y, camera.Position.Z);
                    double minDistance = TheNearest(ray, Objects, camera.Position, out IObject nearestObj, out Point nearestIntercept);
                    if (nearestIntercept != null)
                    {
                        double minNumber = 0;
                        if (light != null)
                        {
                            Vector shadowVector = Vector.Negate(light.Vector);
                            bool isShadow = false;

                            //foreach (var obj in Objects)
                            //{
                            //    if (obj.IsIntersection(nearestIntercept, shadowVector))
                            //        isShadow = true;
                            //}

                            minNumber = -(light.Vector * Vector.Normilize(nearestObj.GetNormal(nearestIntercept)));
                            if (isShadow)
                                minNumber /= 2;
                        }
                        else
                            minNumber = 0;
                        partScreen[x, yIterator] = minNumber;
                    }

                    yIterator++;

                }
            }
            return partScreen;
        }


    }
}
