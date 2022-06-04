using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Materials
{
    public class Color
    {
        private int r;
        private int g;
        private int b;
        private int mirrorring;

        public int R {
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 255)
                    value = 255;
                r = value;
            }
            get
            {
                return r; 
            }
        }

        public int G {
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 255)
                    value = 255;
                g = value;
            }
            get
            {
                return g;
            }
        }

        public int B {
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 255)
                    value = 255;
                b = value;
            }
            get
            {
                return b;
            }
        }

        public int Mirroring { 
            set
            {
                if (value >= 0 && value <= 100)
                    mirrorring = value;
            }
            get
            {
                return mirrorring;
            }
        }

        public Color(int r, int g, int b, int mirror)
        {
            R = r;
            G = g;
            B = b;
            Mirroring = mirror;
        }
    }
}
