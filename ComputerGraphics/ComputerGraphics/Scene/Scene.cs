using System;
using System.Collections.Generic;
using System.Text;
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

        public double[,] getScreenArray()
        {
            camera.RefreshScreen();
            
            var screen = camera.Screen;
            var cameraPos = camera.Position;

            double fov = 60;
            Vector planeOrigin = cameraPos + Vector.Normilize(camera.Direction);

            int width = screen.GetUpperBound(0); //width
            int height = screen.GetUpperBound(1); //height 

            var aspectRatio = width / height;
            
            //Console.WriteLine("a : " + a.X + " " + a.Y + " "+ a.Z);


            for (int x = 0; x < width; x++)
            {
                //var pixelCenter = planePoz;
                for (int y = 0; y < height; y++)
                {

                    var xNorm = (x - width / 2) / (double)width;
                    var yNorm = -(y - height / 2) / (double)height;
                    float distanceToPlaneFromCamera = 1;
                    var fovInRad = fov / 180f * Math.PI;
                    double realPlaneHeight = (distanceToPlaneFromCamera * Math.Tan(fovInRad));
                    double realPlaneWidht = realPlaneHeight / height * width ;

                    var xOnPlane = xNorm * realPlaneWidht / 2;
                    var yOnPlane = yNorm * realPlaneHeight / 2;

                    Vector positionOnPlane = planeOrigin + new Vector(xOnPlane, yOnPlane * 0.5, 0);

                    //трасувальний промінь
                    Vector ray = positionOnPlane - new Vector(cameraPos.X,cameraPos.Y,cameraPos.Z);
                    double minNumber = Int32.MaxValue;
                    double minDistance = Int32.MaxValue;

                    for (int i = 0; i < Objects.Count; i++)
                    {
                        if (Objects[i].IsIntersection(cameraPos, ray))
                        {
                            Point intercept = Objects[i].WhereIntercept(cameraPos, ray);
                            double distance = Point.Distance(intercept, cameraPos);
                            if(distance < minDistance)
                            {
                                minDistance = distance;
                               
                                minNumber = light.Vector * Vector.Normilize(Objects[i].GetNormal(intercept));
                            }
                            
                        }
                            
                    }
                    screen[x, y] = minNumber;
                    if(minDistance == Int32.MaxValue)
                        screen[x, y] = 0;
                    else
                        screen[x, y] = minNumber;
                }
            }

            return screen;
        }
        
    }
}
