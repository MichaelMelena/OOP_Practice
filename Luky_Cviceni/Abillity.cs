using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class Abillity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cooldown { get; set; }
        public int CurrentCooldown { get; set; }
        public int AbillityDuration { get; set; }
        public bool Usable { get; set; }
        public double ResourceCost { get; set; }
        public int Priority { get; set; }
        public int PriorityModifier { get; set; }

        public AttackEffect Effect { get; set; }
      
        public bool IsPassive { get; set; }

        private double StrengthModifier { get; set; }
        private double DexterityModifier { get; set; }
        private double EnduranceModifier { get; set; }
        private double IntellectModifier { get; set; }
        private double SpiritModifier { get; set; }

        /// <summary>
        /// Creates new abillity
        /// </summary>
        /// <param name="name">Name of the abillity</param>
        /// <param name="description">Description for player what abillity does</param>
        /// <param name="strengthModifier">How much will strength affect the damage.</param>
        /// <param name="dexterityModifier">How much will dexterity affect the damage.</param>
        /// <param name="enduranceModifier">How much will endurance affect the damage.</param>
        /// <param name="intelectModifier">How much will intelect affect the damage.</param>
        /// <param name="spirtiModifier">How much will spirit affect the damage.</param>
        /// <param name="attackType">type of attack Magical or physical</param>
        /// <param name="attackEffect">One of 7 attack effects (blind,stun,debuff,buff,silence,heal,none)</param>
        /// <param name="duration">how long will attack effect last</param>
        /// <param name="cost">how many resources abillity uses</param>
        /// <param name="cooldown">amount of rounds until it can be used again</param>
        /// <param name="priority">priority number for AI</param>
        /// <param name="priorityModifier">Ai priority modifier</param>
        public Abillity(string name, string description,double strengthModifier, double dexterityModifier,double enduranceModifier,double intelectModifier,double spirtiModifier,AttackEffect attackEffect,int duration, double cost,int cooldown,bool isPassive=false,int priority=0,int priorityModifier=0)
        {
            this.CurrentCooldown = 0;
            this.Name = name;
            this.Description = description;
            this.StrengthModifier = strengthModifier;
            this.DexterityModifier = dexterityModifier;
            this.EnduranceModifier = enduranceModifier;
            this.IntellectModifier = intelectModifier;
            this.SpiritModifier = spirtiModifier;
           
            this.Effect = attackEffect;
            this.AbillityDuration = duration;
            this.ResourceCost = cost;
            this.Cooldown = cooldown;
            this.IsPassive = isPassive;
            this.Priority = priority;
            this.PriorityModifier = priorityModifier;

        }      


        /// <summary>
        /// Calculates damage output acording to dammage modifiers and character stats
        /// </summary>
        /// <param name="attackPower"></param>
        /// <param name="strength"></param>
        /// <param name="Dexterity"></param>
        /// <param name="endurance"></param>
        /// <param name="intelect"></param>
        /// <param name="spirit"></param>
        /// <returns></returns>
        public double CalculateDamage(double attackPower, double strength, double Dexterity, double endurance, double intelect, double spirit)
        {
            return (attackPower + strength * StrengthModifier + Dexterity * DexterityModifier + endurance * EnduranceModifier + intelect * IntellectModifier + spirit * SpiritModifier);
        }

    }
}
