using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitAndMerge
{
    class Region
    {
        private int startX;
        private int startY;
        private int endX;
        private int endY;


        public Region(int startX, int startY, int endX, int endY): base()
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
        }



        #region setters and getters
        public int Width
        {
            get
            {
                return endX - startX;
            }

            private set { }
        }

        public int Height
        {
            get
            {
                return endY - startY;
            }

            private set { }
        }

        public int StartX
        {
            get
            {
                return startX;
            }

            private set { }
        }

        public int StartY
        {
            get
            {
                return startY;
            }

            private set {}
        }

        public int EndX
        {
            get
            {
                return endX;
            }

            private set { }
        }

        public int EndY
        {
            get
            {
                return endY;
            }

            private set { }
        }

        #endregion
    }
}
