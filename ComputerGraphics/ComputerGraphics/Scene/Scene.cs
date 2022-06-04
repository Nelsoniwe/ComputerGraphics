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
        private List<IObject> Objects = new List<IObject>();
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
                Objects.Add(addObject);

        }

        public void MakeTree()
        {
            List<IObject> copy = new List<IObject>(Objects);
            tree = new Tree.Tree(copy);
        }

        public void AddObjects(List<IObject> objectsCollection)
        {
            for (int i = 0; i < objectsCollection.Count; i++)
            {
                Objects.Add(objectsCollection[i]);
            }
        }

        public void AddLight(Light light)
        {
            this.light = light;
        }

        public static float TheNearest(Vector vector, List<IObject> objects, Point camera, out IObject obj, out Point intercept)
        {
            float minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].IsIntersection(camera, vector))
                {
                    Point tempIntercept = objects[i].WhereIntercept(camera, vector);
                    float distance = Point.Distance(tempIntercept, camera);
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

        public static float TheNearestTree(Vector vector, Tree.Tree tree, Point camera, out IObject obj, out Point intercept)
        {
            float minDistance = Int32.MaxValue;
            obj = null;
            intercept = null;
            List<IObject> objects = tree.GetInterceptionObject(camera, vector);

            if (objects.Count ==0)
            {
                return Int32.MaxValue;
            }
            for (int i = 0; i < objects.Count; i++)
            {
                Point tempIntercept = objects[i].WhereIntercept(camera, vector);

                float distance = Point.Distance(tempIntercept, camera);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    obj = objects[i];
                    intercept = tempIntercept;
                }
            }
            return minDistance;
        }

        public Color[,] GetScreenArray(int taskCount)
        {
            camera.RefreshScreen();

            var screen = camera.Screen;
            var cameraPos = camera.Position;

            float fov = 60;
            Vector planeOrigin = cameraPos + Vector.Normilize(camera.Direction);

            int width = screen.GetUpperBound(1) + 1; //width
            int height = screen.GetUpperBound(0) + 1; //height 

            var aspectRatio = (float)width / height;

            float distanceToPlaneFromCamera = 1;
            var fovInRad = fov / 180f * Math.PI;
            float realPlaneHeight = (float)(distanceToPlaneFromCamera * Math.Tan(fovInRad));
            float realPlaneWidht = realPlaneHeight / height * width;

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
                        screen[j, partCoordinatesCounter] = part[j, l];
                    }

                    partCoordinatesCounter++;
                }
            }

            return screen;
        }

        public Color[,] GetPartScreenArray(
            Vector planeOrigin,
            float realPlaneWidht,
            float realPlaneHeight,
            float aspectRatio,
            int startWidth,
            int endWidth,
            int width,
            int height)
        {
            Color[,] partScreen = new Color[height, endWidth - startWidth];
            
            int yIterator = 0;

            Vector right_vec = Vector.Normilize(Vector.Cross(camera.Direction, Vector.Normilize(new Vector(1, 0, 0))));
            Vector up = Vector.Cross(camera.Direction, Vector.Negate(right_vec));

            var w = camera.Direction;
            var u = Vector.Cross(w, up);
            var v = Vector.Cross(u, w);

            var pixelHeight = realPlaneHeight / height;
            var pixelWidth = realPlaneWidht / width;

            var scanlineStart = planeOrigin - (width / 2) * pixelWidth * u + startWidth * pixelWidth * u + (pixelWidth / 2) * u +
                (height / 2) * pixelHeight * v - (pixelHeight / 2) * v;
            var scanlineStartPoint = new Point(scanlineStart.X, scanlineStart.Y, scanlineStart.Z);

            var pixelWidthU = pixelWidth * u;
            var pixelHeightV = pixelHeight * v;

            for (int x = 0; x < height; x++)
            {
                yIterator = 0;
                var pixelCenter = new Point(scanlineStartPoint.X, scanlineStartPoint.Y, scanlineStartPoint.Z);

                //var pixelCenter = planePoz;
                for (int y = startWidth; y < endWidth; y++)
                {
                    var ray = pixelCenter - camera.Position;

                    float minDistance = TheNearestTree(ray, tree, camera.Position, out IObject nearestObj, out Point nearestIntercept);
                    //float minDistance = TheNearest(ray, Objects, camera.Position, out IObject nearestObj, out Point nearestIntercept);

                    

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

                    Color newColor = new Color(0, 0, 0, 0);

                    if (nearestObj != null && nearestObj.color.Mirroring > 0)
                    {
                        MirrorVector(ref nearestObj, ref nearestIntercept, camera.Position, ray, ref newColor, minDistance);
                    }

                    if (nearestIntercept != null)
                    {
                        float minNumber = 0;
                        if (light != null)
                        {
                            Vector shadowVector = Vector.Negate(light.Vector);
                            bool isShadow = false;

                            if (tree.GetInterceptionObject(nearestIntercept, shadowVector).Count > 1)
                                isShadow = true;

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
                        if(newColor.Mirroring > 0)
                            partScreen[x, yIterator] = newColor; 
                        else
                            partScreen[x, yIterator] = new Color(
                            (int)(minNumber * nearestObj.color.R),
                            (int)(minNumber * nearestObj.color.G),
                            (int)(minNumber * nearestObj.color.B),
                            nearestObj.color.Mirroring);
                    }

                    if(partScreen[x, yIterator] == null)
                    {
                        partScreen[x, yIterator] = new Color(135, 206, 250, 0);
                        if(newColor.Mirroring > 0)
                            partScreen[x, yIterator] = new Color(
                                newColor.R + (int)135 * (100 - newColor.Mirroring) / 100,
                                newColor.G + (int)206 * (100 - newColor.Mirroring) / 100,
                                newColor.B + (int)250 * (100 - newColor.Mirroring) / 100,
                                newColor.Mirroring
                                );
                    }
                        

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

            if (color.Mirroring >= 100)
                return;

            var normal = obj.GetNormal(intercept);
            Vector reflected = direct - 2 * (direct * normal)*normal;
            
            var minDistance = TheNearestTree(reflected, tree, intercept, out IObject newObject, out Point newIntercept);

            

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

            var minNumber = -(light.Vector * Vector.Normilize(obj.GetNormal(intercept)));

            if (obj != null && obj.color.Mirroring == 0)
            {
                color.R += (int)(obj.color.R * (100 - color.Mirroring) / 100 * minNumber);
                color.G += (int)(obj.color.G * (100 - color.Mirroring) / 100 * minNumber);
                color.B += (int)(obj.color.B * (100 - color.Mirroring) / 100 * minNumber);

                return;
            }

            if (obj != null && obj.color.Mirroring > 0)
            {
                MirrorVector(ref obj, ref intercept, origin, reflected, ref color, minNumber);
            }

            

        }


    }
}
