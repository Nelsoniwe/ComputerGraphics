using System;
using System.Collections.Generic;
using System.Text;
using ComputerGraphics.Interfaces;
using ComputerGraphics.Types;

namespace ComputerGraphics.Scene
{
    public class Scene
    {
        private List<IObject> Objects = new List<IObject>();
        private Camera camera;

        public Scene(Camera camera)
        {
            this.camera = camera;
        }

        public void AddObject(IObject addObject)
        {
            Objects.Add(addObject);
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

                    Vector positionOnPlane = planeOrigin + new Vector(xOnPlane, yOnPlane, 0);
                    Vector ray = positionOnPlane - new Vector(cameraPos.X,cameraPos.Y,cameraPos.Z);

                    for (int i = 0; i < Objects.Count; i++)
                    {
                        if (Objects[i].IsIntersection(cameraPos, ray))
                            screen[x, y] = 1;
                        else if(screen[x, y] != 1)
                            screen[x, y] = 0;
                    }
                }
            }

            return screen;
        }
        
    }
}
