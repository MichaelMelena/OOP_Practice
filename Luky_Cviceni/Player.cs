using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class Player:Character
    {
        public Player()
        {
            
        }
        protected void OfferAbillities()// tohle se da predelat pomoci linq
        {
            CheckAbillities();
            Console.WriteLine("Abillities");
            for (int i = 0; i < Abillities.Length; i++)
            {
                if (Abillities[i].Usable)
                {
                    Console.WriteLine(i + ": " + AbillityInfoPrint(Abillities[i]));
                }
            }
            DoLine();

        }
       
        protected virtual void ChooseAction()
        {
            int abillityIndex = 0;
            bool parseResult = false;
            string chosenOption = " ";
            while (chosenOption.ToUpper() != "Q")
            {
                OfferAbillities();
                Console.WriteLine("Choose a attack or type Q to skip your turn");

              parseResult =  int.TryParse(Console.ReadLine(), out abillityIndex);
                if (parseResult)
                {
                    double damage = Abillities[abillityIndex].CalculateDamage(this.AttackPower, this.Strength, this.Dexterity, this.Endurance, this.Intellect, this.Spirit);
                    CurrentStaminaChanging( Abillities[abillityIndex].StaminaCost);
                    CurrentHitPointsChanging(  Abillities[abillityIndex].HealthCost);
                    Attacking(new AttackEventArgs() { Amount = damage, Effect = Abillities[abillityIndex].Effect, AttackEffectDurations = Abillities[abillityIndex].AbillityDuration });
                    break;
                }
               
            }

        }
        protected virtual void AttackMessage(AttackEventArgs args)
        {
            Console.WriteLine(string.Format("{0} Attacks for: {1}. attack causes: {2} for {3} rounds",this.Name,Math.Round(args.Amount) ,args.Effect,args.AttackEffectDurations ));
        }

        protected string AbillityInfoPrint(Abillity abillity)
        {
            return string.Format("{0} === {1}  Cost: {2} Effect: {3} Duration: {4} Cooldown: {5}", abillity.Name.ToUpper(), abillity.Description, abillity.StaminaCost, abillity.Effect, abillity.AbillityDuration, abillity.Cooldown);
        }

        protected override void Attacking(AttackEventArgs args)
        {
            AttackMessage(args);
            base.Attacking(args);
        }
    }
}
