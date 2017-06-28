using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class Dummy:Character
    {
        public Dummy(double maximumHitPoints):base()
        {
            this.Name = "Dummy";
            this.MaximumHitPoints = maximumHitPoints;
            this.CurrentHitPoints = maximumHitPoints;
            this.AttackModifier = 1;
            this.DefenseModifier = 1;
        }
        public override void OnAttack(object source, AttackEventArgs e)
        {
            if (e.Amount <= DefensePower * DefenseModifier)
            {
                Console.WriteLine("dummy was not hit" + Environment.NewLine + "Dummy has: " + CurrentHitPoints + " HP");
                DoLine();
            }
            else if(e.Amount> DefensePower*DefenseModifier)
            {
                HitPointsChanging((DefensePower*DefenseModifier )-e.Amount);
                Console.WriteLine("Dummy was hit for "+e.Amount);

            }
        }
       
        
    }
}
