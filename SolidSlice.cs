using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitAndMerge
{
    class SolidSlice<T> : Region
    {
        private T value;

        public T Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public SolidSlice(int startX, int startY, int endX, int endY, T value) :
            base(startX, startY, endX, endY)
        {
            this.Value = value;
        }

        public SolidSlice(Region region, T value): 
            base(region.StartX, region.StartY, region.EndX, region.EndY)
        {
            this.Value = value;
        }
    }
}
