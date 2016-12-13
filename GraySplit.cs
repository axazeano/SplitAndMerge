using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitAndMerge
{
    class GraySplit
    {
        public delegate void SetImage(Bitmap image);
        private Bitmap image;
        private int threshold;
        private bool isSplitFinish;
        private byte[,] grayscaleMap;
        private List<SolidSlice<byte>> readySlices;
        private List<Region> dirtySlices;

        public Bitmap Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        public GraySplit(Bitmap image, int threshold)
        {
            this.image = image;
            this.grayscaleMap = getGrayscaleMap();
            this.threshold = threshold;
            this.readySlices = new List<SolidSlice<byte>>();
            this.dirtySlices = new List<Region>();

        }

        public void split(SetImage setImageDelegate)
        {
            dirtySlices.Add(new Region(0, 0, image.Width, image.Height));
            List<Region> tempSlices;
            int halfWidth;
            int halfHeight;
            while (dirtySlices.Count != 0)
            {
                tempSlices = new List<Region>();
                foreach (Region region in dirtySlices)
                {
                    if (isRegionUniform(region))
                    {
                        byte meanValue = (byte)meanValueForRegion(region);
                        readySlices.Add(new SolidSlice<byte>(region, meanValue));
                    }
                    else
                    {
                        halfWidth = (region.EndX - region.StartX) / 2 + region.StartX;
                        halfHeight = (region.EndY - region.StartY) / 2 + region.StartY;

                        if (halfHeight < 2 && halfWidth < 2)
                        {
                            readySlices.Add(new SolidSlice<byte>(region, (byte)meanValueForRegion(region)));
                        } else
                        {
                            tempSlices.Add(new Region(region.StartX, region.StartY, halfWidth, halfHeight));
                            tempSlices.Add(new Region(halfWidth, region.StartY, region.EndX, halfHeight));
                            tempSlices.Add(new Region(region.StartX, halfHeight, halfWidth, region.EndY));
                            tempSlices.Add(new Region(halfWidth, halfHeight, region.EndX, region.EndY));
                        }
                    }
                }
                dirtySlices = tempSlices;
            }
            Console.Out.Write("Complete");

            foreach (SolidSlice<byte> solidSlice in readySlices)
            {
                for (int x = solidSlice.StartX; x < solidSlice.EndX; x++)
                {
                    for (int y = solidSlice.StartY; y < solidSlice.EndY; y++)
                    {
                        image.SetPixel(x, y, Color.FromArgb(solidSlice.Value, solidSlice.Value, solidSlice.Value));
                    }
                }
            }
            setImageDelegate(image);
        }

        public float[,] imageToHueMap(Bitmap image)
        {
            float[,] result = new float[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    result[x, y] = image.GetPixel(x, y).GetHue();
                }
            }
            return result;
        }

        private bool isRegionUniform(Region region)
        {
            double standartDeviation = 0;
            double meanValue = meanValueForRegion(region);
            Parallel.For(region.StartX, region.EndX, x =>
            {
                for (int y = region.StartY; y < region.EndY; y++)
                {
                    standartDeviation += Math.Pow(grayscaleMap[x, y] - meanValue, 2);
                }
            });
            standartDeviation /= (grayscaleMap.Length - 1);
            return standartDeviation < threshold;
        }

        private double meanValueForRegion(Region region)
        {
            double result = 0;
            for (int x = region.StartX; x < region.EndX; x++)
            {
                for (int y = region.StartY; y < region.EndY; y++)
                {   
                    result += grayscaleMap[x, y];
                }
            }
            return result / (region.Width * region.Height);
        }

        private byte[,] getGrayscaleMap()
        {
            byte[,] result = new byte[image.Width, image.Height];
            Color currentPixel;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    currentPixel = image.GetPixel(x, y);
                    result[x, y] = (byte)((currentPixel.R * 0.299) + (currentPixel.G * 0.587) + (currentPixel.B * 0.144));
                }
            }
            return result;
        }
    }
}
