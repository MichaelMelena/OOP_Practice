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

        public enum SpiritEffect { Health, Stamina }

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


        protected double AttackPower { get; set; }
        protected double DefensePower { get; set; }



        protected double Initiative { get; set; }
        protected double AttackModifier { get; set; }
        protected double DefenseModifier { get; set; }



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

        protected Abillity[] Abillities { get; set; }




        /// <summary>
        /// Calculates all the attributes
        /// </summary>
        protected void CalculateAll()
        {
            CalculateMaximumHitPoints();
            CalculateMaximumStamina();
            CalculateMaximumStamina();
            CalculateAttackPower();
            CalculateDefensePower();

            CalculateInitiative();
            DecideSpirtiEffect();
        }

        /// <summary>
        /// Checks if abillities meet the criteria to be used
        /// </summary>
        protected void CheckAbillities()
        {
            foreach (Abillity abillity in Abillities)
            {


                if (abillity.CurrentCooldown == 0 && abillity.StaminaCost <= CurrentStamina && abillity.HealthCost < CurrentHitPoints && IsSilenced == false)
                    abillity.Usable = true;
                else
                    abillity.Usable = false;

            }

        }

        /// <summary>
        /// Does a line to separate block of text in console
        /// </summary>
        protected void DoLine()
        {
            Console.WriteLine("--------------------------------------------------------");
        }



        //Virtuální metody ,které je možno přepsat
        /// <summary>
        /// Contains fromula for calcutaing maximum HP
        /// </summary>
        protected virtual void CalculateMaximumHitPoints()
        {
            MaximumHitPoints = this.Endurance * 1 + this.Strength * 1;
        }
        /// <summary>
        /// Contains fromula for calcutaing maximum Stamina
        /// </summary>
        protected virtual void CalculateMaximumStamina()
        {
            MaximumStamina = this.Endurance * 1 + this.Dexterity * 1 + this.Strength * 1;
        }
        /// <summary>
        /// Contains fromula for calcutaing attack power
        /// </summary>
        protected virtual void CalculateAttackPower()
        {
            AttackPower = this.Strength * 1 + this.Dexterity * 1;
        }
        /// <summary>
        /// Contains fromula for calcutaing defense power
        /// </summary>
        protected virtual void CalculateDefensePower()
        {
            DefensePower = this.Endurance * 1 + this.Dexterity * 1;
        }
        /// <summary>
        /// Contains fromula for calcutaing initiative
        /// </summary>
        protected virtual void CalculateInitiative()
        {
            Initiative = this.Dexterity * 1 + this.Intellect * 1 + this.Spirit * 1;
        }

        /// <summary>
        /// Decides which stat will we renewed each round-- health/stamina regeneration 
        /// </summary>
        protected virtual void DecideSpirtiEffect()
        {
            if (this.MaximumStamina == this.MaximumHitPoints || this.MaximumStamina > this.MaximumHitPoints)
                this.CurrentSpiritEffect = SpiritEffect.Stamina;
            else
                this.CurrentSpiritEffect = SpiritEffect.Health;
        }

        

        //Eventy
        public event EventHandler<AttackEventArgs> Attack;
        protected virtual void Attacking(AttackEventArgs args)
        {
            if (Attack != null)
            {
                Attack(this, args);
            }
        }

     
        public event EventHandler<PointsEventArgs> CurrentHitPointsChange;
        protected virtual void CurrentHitPointsChanging( double amount)
        {
            
            this.CurrentHitPoints += amount;
            if (CurrentHitPointsChange != null)
            {
                CurrentHitPointsChange(this, new PointsEventArgs() { MaximumPoints = this.MaximumHitPoints, CurrentPoints = this.CurrentHitPoints });
            }
        }

        public event EventHandler<PointsEventArgs> CurrentStaminaChange;
        protected virtual void CurrentStaminaChanging(double amount)
        {
            this.CurrentStamina += amount;
            if (CurrentStaminaChange != null)
            {
                CurrentStaminaChange(this, new PointsEventArgs() { MaximumPoints= this.MaximumStamina,CurrentPoints= this.CurrentStamina});
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
        public virtual void OnAttack(object source, AttackEventArgs e)
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

        /// <summary>
        /// Controls if effect should be activated or prolonged
        /// </summary>
        /// <param name="attackEffect">Type of atttack Effect</param>
        /// <param name="duration">How long effect lasts</param>
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

        /// <summary>
        /// Checks if some effect is no longer active
        /// </summary>
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
