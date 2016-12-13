using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitAndMerge
{
    class HSB
    {
        public double Hue;
        public double Saturation;
        public double Brightness;

        public HSB(double hue, double saturation, double brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }

        public HSB(Color color)
        {
            Hue = color.GetHue();
            Saturation = color.GetSaturation();
            Brightness = color.GetBrightness();
        }

        public HSB()
        {
            Hue = 0.0;
            Saturation = 0.0;
            Brightness = 0.0;
        }

        public Color getRGB()
        {
            double R, G, B;
            if (Brightness <= 0)
            { R = G = B = 0; }
            else if (Saturation <= 0)
            {
                R = G = B = Brightness;
            }
            else
            {
                double hf = Hue / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = Brightness * (1 - Saturation);
                double qv = Brightness * (1 - Saturation * f);
                double tv = Brightness * (1 - Saturation * (1 - f));
                switch (i)
                {
                    case 0:
                        R = Brightness;
                        G = tv;
                        B = pv;
                        break;
                    case 1:
                        R = qv;
                        G = Brightness;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = Brightness;
                        B = tv;
                        break;
                    case 3:
                        R = pv;
                        G = qv;
                        B = Brightness;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = Brightness;
                        break;
                    case 5:
                        R = Brightness;
                        G = pv;
                        B = qv;
                        break;
                    case 6:
                        R = Brightness;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = Brightness;
                        G = pv;
                        B = qv;
                        break;
                    default:
                        R = G = B = Brightness;
                        break;
                }
            }

            int r = Clamp((int)(R * 255.0));
            int g = Clamp((int)(G * 255.0));
            int b = Clamp((int)(B * 255.0));
            return Color.FromArgb(r, g, b);
        }

        private int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}

