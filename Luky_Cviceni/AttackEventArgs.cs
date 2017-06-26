using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class AttackEventArgs : EventArgs
    {
        public double Amount { get; set; }
        public AttackEffect Effect { get; set; }
        public int AttackEffectDurations {get;set;}
    }
}
