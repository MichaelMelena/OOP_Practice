using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    enum AttackEffect { Blind, Stun, Debuff, Buff, Silence, Heal, None }
   
    /// <summary>
    /// Abstract class intended to be inherited
    /// </summary>
    abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int SkillPoints { get; set; }
        //Druhy útoků

        public enum SpiritEffect { Staminapool, Manapool }

        //Atributy ze ,kterých se počítají staty
        protected double Strength { get; set; }
        protected double Dexterity { get; set; }
        protected double Endurance { get; set; }
        protected double Intellect { get; set; }
        protected double Spirit { get; set; }

        //Staty
        protected double CurrentHitPoints { get; set; }
        protected double MaximumHitPoints { get; set; }
        protected double CurrentStamina { get; set; }
        protected double MaximumStamina { get; set; }

        
        protected double PhysicalAttackPower { get; set; }
        protected double PhysicalDefensePower { get; set; }

        

        protected double Initiative { get; set; }
        protected double PhysicalAttackModifier { get; set; }
        protected double physicalDefenseModifier { get; set; }

       

        protected SpiritEffect CurrentSpiritEffect { get; set; }

        //proměné effektů
        protected bool IsBlinded { get; set; }
        protected int BlindDuration { get; set; }
        protected bool IsStunned { get; set; }
        protected int StunDuration { get; set; }
        protected bool IsSilenced { get; set; }
        protected int SilenceDuration { get; set; }
        protected bool IsBuffed { get; set; }
        protected int BuffDuration { get; set; }
        protected bool ISDebuff { get; set; }
        protected int DebuffDuration { get; set; }
        protected bool IsHealed { get; set; }
        protected int HealDuration { get; set; }

        protected Abillity[] PhysicalAbillities { get; set; }
     

        //Konstructory
        public Character()
        {

        }

        /// <summary>
        /// Calculates all the attributes
        /// </summary>
        protected void CalculateAll()
        {
            CalculateMaximumHitPoints();
            CalculateMaximumStamina();
            CalculateMaximumStamina();
            CalculatePhysicalAttackPower();
            CalculatePhysicalDefensePower();
          
            CalculateInitiative();
            CalculateSpirtiEffect();
        }
        protected void CheckAbillities()
        {
            foreach (Abillity abillity in PhysicalAbillities)
            {


                if (abillity.CurrentCooldown == 0 && abillity.ResourceCost <= CurrentStamina && IsBlinded == false)
                    abillity.Usable = true;
                else
                    abillity.Usable = false;

            }

          


        }
        protected void OfferAbillities()// tohle se da predelat pomoci linq
        {
            Console.WriteLine("Physical abillities");
            for (int i = 0; i < PhysicalAbillities.Length; i++)
            {
                if (PhysicalAbillities[i].Usable)
                {
                    Console.WriteLine(i+": "+AbillityInfoPrint(PhysicalAbillities[i]));
                }
            }
            DoLine();
          
        }
        protected void DoLine()
        {
            Console.WriteLine("--------------------------------------------------------");
        }
        protected virtual void ChooseAction()
        {
            string chosenOption = " ";
            while (chosenOption.ToUpper() != "Q")
            {
                OfferAbillities();
                Console.WriteLine("Choose a attack or type Q to skip your turn");
            }
            
        }

        protected string AbillityInfoPrint(Abillity abillity)
        {
            return string.Format("{0} === {1}  Cost: {2} Effect: {3} Duration: {4} Cooldown: {5}", abillity.Name.ToUpper(),abillity.Description,abillity.ResourceCost,abillity.Effect,abillity.AbillityDuration,abillity.Cooldown);
        }
        //Virtuální metody ,které je možno přepsat
        protected virtual void CalculateMaximumHitPoints()
        {
            MaximumHitPoints = this.Endurance * 1 + this.Strength * 1;
        }
        protected virtual void CalculateMaximumStamina()
        {
            MaximumStamina = this.Endurance * 1 + this.Dexterity * 1 + this.Strength * 1;
        }
       
        protected virtual void CalculatePhysicalAttackPower()
        {
            PhysicalAttackPower = this.Strength * 1 + this.Dexterity * 1;
        }
        protected virtual void CalculatePhysicalDefensePower()
        {
            PhysicalDefensePower = this.Endurance * 1 + this.Dexterity * 1;
        }
       
       
        protected virtual void CalculateInitiative()
        {
            Initiative = this.Dexterity * 1 + this.Intellect * 1 + this.Spirit * 1;
        }
        protected virtual void CalculateSpirtiEffect()
        {
            /*if (this.MaximumStamina == this.MaximumMana || this.MaximumStamina > this.MaximumMana)
                this.CurrentSpiritEffect = SpiritEffect.Staminapool;
            else
                this.CurrentSpiritEffect = SpiritEffect.Manapool;*/
        }


        //Eventy
        public event EventHandler<AttackEventArgs> PhysicalAttack;
        protected virtual void PhysicalAttacking(AttackEventArgs args)
        {
            if (PhysicalAttack != null)
            {
                PhysicalAttack(this, args);
            }
        }

     
        public event EventHandler<PointsEventArgs> CurrentHitPointsChange;
        protected virtual void CurrentHitPointsChanging(PointsEventArgs args)
        {
            if (CurrentHitPointsChange != null)
            {
                CurrentHitPointsChange(this, args);
            }
        }

        public event EventHandler<PointsEventArgs> CurrentStaminaChange;
        protected virtual void CurrentStaminaChanging(PointsEventArgs args)
        {
            if (CurrentStaminaChange != null)
            {
                CurrentStaminaChange(this, args);
            }
        }

       

        public event EventHandler Blind;
        protected virtual void Blinded()
        {
            if (Blind != null)
            {
                Blind(this, EventArgs.Empty);
            }

        }

        public event EventHandler Stun;
        protected virtual void Stunned()
        {
            if (Stun != null)
            {
                Stun(this, EventArgs.Empty);
            }

        }

        public event EventHandler Silence;
        protected virtual void Silenceded()
        {
            if (Silence != null)
            {
                Silence(this, EventArgs.Empty);
            }

        }
        public event EventHandler Buff;
        protected virtual void Buffed()
        {
            if (Buff != null)
            {
                Buff(this, EventArgs.Empty);
            }

        }

        public event EventHandler Debuff;
        protected virtual void Debuffed()
        {
            if (Debuff != null)
            {
                Debuff(this, EventArgs.Empty);
            }

        }

        public event EventHandler Heal;
        protected virtual void Healed()
        {
            if (Heal != null)
            {
                Heal(this, EventArgs.Empty);
            }

        }

        //subscribers
        public virtual void OnPhysicalAttack(object source, AttackEventArgs e)
        {

        }
       
        public virtual void OnCurrentHitPointsChange(object source, PointsEventArgs e)
        {

        }
        public virtual void OnCurrentStaminaChange(object source, PointsEventArgs e)
        {

        }
      
        public virtual void OnBlind(object source, EventArgs e)
        {

        }
        public virtual void OnStun(object source, EventArgs e)
        {

        }
        public virtual void OnSilence(object source, EventArgs e)
        {

        }
        public virtual void OnBuff(object source, EventArgs e)
        {

        }
        public virtual void OnDebuff(object source, EventArgs e)
        {

        }
        public virtual void OnHeal(object source, EventArgs e)
        {

        }

        

        protected virtual void OnEndofRound(object sourece, EventArgs e)
        {
            BlindDuration--;
            StunDuration--;
            DebuffDuration--;
            BuffDuration--;
            SilenceDuration--;
            HealDuration--;
            CheckForEffectReset();

        }
        protected virtual void OnStartOfRounds(object source, EventArgs e)
        {

        }


        protected void Effects(AttackEffect attackEffect, int duration)
        {
            switch ((int)attackEffect)
            {
                case 0:
                    {
                        if (IsBlinded)
                            BlindDuration += duration;
                        else
                        {
                            IsBlinded = true;
                            BlindDuration = duration;
                        }
                        break;
                    }
                case 1:
                    {
                        if (IsStunned)
                            StunDuration += duration;
                        else
                        {
                            IsStunned = true;
                            StunDuration = duration;
                        }
                        break;
                    }
                case 2:
                    {
                        if (ISDebuff)
                            DebuffDuration += duration;
                        else
                        {
                            ISDebuff= true;
                            DebuffDuration = duration;
                        }
                        break;
                    }
                case 3:
                    {
                        if (IsBuffed)
                            BuffDuration += duration;
                        else
                        {
                            IsBuffed= true;
                            BuffDuration = duration;
                        }
                        break;
                    }
                case 4:
                    {
                        if (IsSilenced)
                            SilenceDuration += duration;
                        else
                        {
                            IsSilenced= true;
                            SilenceDuration = duration;
                        }
                        break;
                    }
                case 5:
                    {
                        if (IsHealed)
                            HealDuration+= duration;
                        else
                        {
                            IsHealed= true;
                            HealDuration = duration;
                        }
                        break;
                    }
                default:
                    {
                        //kdyz je effect none
                        break;
                    }
            }

        }

        protected void CheckForEffectReset()
        {
            if (BlindDuration == 0)
                IsBlinded = false;
            if (StunDuration == 0)
                IsStunned = false;
            if (DebuffDuration == 0)
                ISDebuff = false;
            if (BuffDuration == 0)
                IsBuffed = false;
            if (SilenceDuration == 0)
                IsSilenced = false;
            if (HealDuration == 0)
                IsHealed = false;
        }
       

    }
}
