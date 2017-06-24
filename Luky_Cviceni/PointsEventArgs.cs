using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class PointsEventArgs:EventArgs
    {
        public double MaximumPoints { get; set; }
        public double CurrentPoints { get; set; }
    }
}
