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
        public enum SpiritEffect { Health, Stamina }

        #region Charecter Stats
        public string Name { get; set; }
        protected double Strength { get; set; }
        protected double Dexterity { get; set; }
        protected double Endurance { get; set; }
        protected double Intellect { get; set; }
        protected double Spirit { get; set; }
        #endregion

        #region Battle Stats
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
        #endregion

        #region Effect Variables
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
        #endregion

        protected List<Abillity> Abillities { get; set; }

        /// <summary>
        /// Constructor without any parameters
        /// </summary>
        public Character()
        {
            Strength = 1;
            Dexterity = 1;
            Endurance = 1;
            Intellect = 1;
            Spirit = 1;
            Abillities = new List<Abillity>();
            CalculateAll();
        }

        /// <summary>
        /// Constructor that uses parameters as stats
        /// </summary>
        /// <param name="strength"></param>
        /// <param name="dexterity"></param>
        /// <param name="endurance"></param>
        /// <param name="intelect"></param>
        /// <param name="spirit"></param>
        public Character(double strength, double dexterity, double endurance, double intelect, double spirit)
        {
            Strength = strength;
            Dexterity = dexterity;
            Endurance = endurance;
            Intellect = intelect;
            Spirit = spirit;
            Abillities = new List<Abillity>();
            CalculateAll();
        }

        #region Clasic Methods
        /// <summary>
        /// Calculates all the attributes
        /// </summary>
        protected void CalculateAll()
        {
            CalculateMaximumHitPoints();
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
                            ISDebuff = true;
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
                            IsBuffed = true;
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
                            IsSilenced = true;
                            SilenceDuration = duration;
                        }
                        break;
                    }
                case 5:
                    {
                        if (IsHealed)
                            HealDuration += duration;
                        else
                        {
                            IsHealed = true;
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

        #region Virtual Methods
        /// <summary>
        /// Contains fromula for calcutaing maximum HP
        /// </summary>
        protected virtual void CalculateMaximumHitPoints()
        {
            MaximumHitPoints = this.Endurance * 1 + this.Strength * 1;
            CurrentHitPoints = MaximumHitPoints;
        }
        /// <summary>
        /// Contains fromula for calcutaing maximum Stamina
        /// </summary>
        protected virtual void CalculateMaximumStamina()
        {
            MaximumStamina = this.Endurance * 1 + this.Dexterity * 1 + this.Strength * 1;
            CurrentStamina = MaximumStamina;
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
        #endregion

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
        #endregion

        #region Events

        #region Stat Events
        /// <summary>
        /// Event: When character attacks
        /// </summary>
        public event EventHandler<AttackEventArgs> Attack;
        /// <summary>
        /// Triggers Attack event
        /// </summary>
        /// <param name="args">parameters of the attack</param>
        protected virtual void Attacking(AttackEventArgs args)
        {
            if (Attack != null)
            {
                Attack(this, args);
            }
        }
        public event EventHandler Victory;
        protected virtual void Won()
        {
            if (Victory != null)
            {
                Victory(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// Event: when character hitpoints changes
        /// </summary>
        public event EventHandler<PointsEventArgs> HitPointsChange;
        /// <summary>
        /// Triggers HitPointsChanged event and changes hitpoints
        /// </summary>
        /// <param name="amount"> number determining hitpoins change</param>
        protected virtual void HitPointsChanging(double amount)
        {

            if ((this.CurrentHitPoints+amount)<=MaximumHitPoints)
            {
                this.CurrentHitPoints += amount;
            }
           
            if (HitPointsChange != null)
            {
                HitPointsChange(this, new PointsEventArgs() { MaximumPoints = this.MaximumHitPoints, CurrentPoints = this.CurrentHitPoints });
                if(CurrentHitPoints<=0)
                {
                    Won();
                }
            }
        }

        /// <summary>
        /// Event: when character stamina changes
        /// </summary>
        public event EventHandler<PointsEventArgs> StaminaChange;
        /// <summary>
        /// Triggers StaminaChange and also changes stamina
        /// </summary>
        /// <param name="amount">number determining how much stamina will change</param>
        protected virtual void StaminaChanging(double amount)
        {
            if((CurrentStamina+amount)<=MaximumStamina)
            {
                this.CurrentStamina += amount;
            }
            
            if (StaminaChange != null)
            {
                StaminaChange(this, new PointsEventArgs() { MaximumPoints = this.MaximumStamina, CurrentPoints = this.CurrentStamina });
                
            }
        }


        #endregion

        #region Effect Events

        /// <summary>
        /// Event: when character is blinded
        /// </summary>
        public event EventHandler Blind;
        /// <summary>
        /// Triiggers Blind Event
        /// </summary>
        protected virtual void Blinded()
        {
            if (Blind != null)
            {
                Blind(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// Event: when character i stuned
        /// </summary>
        public event EventHandler Stun;
        /// <summary>
        /// Triggers stun event
        /// </summary>
        protected virtual void Stunned()
        {
            if (Stun != null)
            {
                Stun(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// Event: When character is Silenced
        /// </summary>
        public event EventHandler Silence;
        /// <summary>
        /// Triggers Silence Event
        /// </summary>
        protected virtual void Silenceded()
        {
            if (Silence != null)
            {
                Silence(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// Event: when character is Buffed
        /// </summary>
        public event EventHandler Buff;
        /// <summary>
        /// Triggers Buff Event
        /// </summary>
        protected virtual void Buffed()
        {
            if (Buff != null)
            {
                Buff(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// Event: when character is Debufed
        /// </summary>
        public event EventHandler Debuff;
        /// <summary>
        /// Triggers Debuff event
        /// </summary>
        protected virtual void Debuffed()
        {
            if (Debuff != null)
            {
                Debuff(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// Event: when character is Healed
        /// </summary>
        public event EventHandler Heal;
        /// <summary>
        /// Triggers Heal Event
        /// </summary>
        protected virtual void Healed()
        {
            if (Heal != null)
            {
                Heal(this, EventArgs.Empty);
            }

        }
        #endregion

        #endregion

        #region Subscribers
        #region Effect Subscibers
        /// <summary>
        /// reacts on Enemy attack Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnAttack(object source, AttackEventArgs e)
        {

        }

        /// <summary>
        /// Reacts to Victory Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnDefeat(object source, EventArgs e)
        {

        }

        
       

        /// <summary>
        /// reacts on Enemy HitPointsChange Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnHitPointsChange(object source, PointsEventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy StaminaChange Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnStaminaChange(object source, PointsEventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy Blind Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnBlind(object source, EventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy Stun Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnStun(object source, EventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy Silence Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnSilence(object source, EventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy Buff Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnBuff(object source, EventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy Debuff Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnDebuff(object source, EventArgs e)
        {

        }

        /// <summary>
        /// reacts on Enemy Heal Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnHeal(object source, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Reacts arenas object end of the round
        /// </summary>
        /// <param name="sourece"></param>
        /// <param name="e"></param>


        public virtual void OnEndofRound(object sourece, EventArgs e)
        {
            BlindDuration--;
            StunDuration--;
            DebuffDuration--;
            BuffDuration--;
            SilenceDuration--;
            HealDuration--;
            CheckForEffectReset();

        }

        /// <summary>
        /// Reacts arenas object start of the round
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnStartOfRound(object source, EventArgs e)
        {

        }
        #endregion

    }
}
