using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class Player : Character
    {
        protected int Level { get; set; }
        protected int KillCount { get; set; }
        protected int Milestone { get; set; }
        protected int SkillPoints { get; set; }


        /// <summary>
        /// Non parametric constructor
        /// </summary>
        public Player() : base()
        {
            SetFirstLevel();
            NameYourCharacter();
            AttackModifier = 1;
            DefenseModifier = 1;

        }

        /// <summary>
        /// Constructor that takes player stats as parameters
        /// </summary>
        /// <param name="strength"></param>
        /// <param name="dexterity"></param>
        /// <param name="endurance"></param>
        /// <param name="intelect"></param>
        /// <param name="spirit"></param>
        public Player(double strength, double dexterity, double endurance, double intelect, double spirit) : base(strength, dexterity, endurance, intelect, spirit)
        {
            SetFirstLevel();
            NameYourCharacter();
            AttackModifier = 1;
            DefenseModifier = 1;
        }

        /// <summary>
        /// Sets character on his first level (level 0) 
        /// </summary>
        protected void SetFirstLevel()
        {
            KillCount = 0;
            Level = 0;
            Milestone = 1;

        }

        public  void PlayRound()
        {
            ChooseAction();   
        }

        /// <summary>
        /// ask player for name and sets it to the character
        /// </summary>
        protected void NameYourCharacter()
        {
            Console.WriteLine("Write a name for your character");
            this.Name = TypeHere();
            DoLine();
        }

        /// <summary>
        /// Offers player all available abillities that he can use
        /// </summary>
        protected void OfferAbillities()// tohle se da predelat pomoci linq
        {
            CheckAbillities();
            DoLine();
            Console.WriteLine("Abillities");
            for (int i = 0; i < Abillities.Count; i++)
            {
                if (Abillities[i].Usable)
                {
                    Console.WriteLine(i + ": " + AbillityInfoPrint(Abillities[i]));
                }
            }
            DoLine();

        }

        /// <summary>
        /// uses method OfferAbillities and uses player chosen abillity
        /// </summary>
        protected virtual void ChooseAction()
        {
            int abillityIndex = 0;
            bool parseResult = false;
            string chosenOption = " ";
            while (chosenOption.ToUpper() != "Q")
            {
                OfferAbillities();
                Console.WriteLine("Choose an attack or type Q to skip your turn");

                parseResult = int.TryParse(Console.ReadLine(), out abillityIndex);
                if (parseResult && abillityIndex < Abillities.Count)
                {
                    double damage = Abillities[abillityIndex].CalculateDamage(this.AttackPower, this.Strength, this.Dexterity, this.Endurance, this.Intellect, this.Spirit);
                    StaminaChanging(Abillities[abillityIndex].StaminaCost);
                    HitPointsChanging(Abillities[abillityIndex].HealthCost);
                         DoLine();
                    Attacking(new AttackEventArgs() { Amount = damage * AttackModifier, Effect = Abillities[abillityIndex].Effect, AttackEffectDuration = Abillities[abillityIndex].AbillityDuration });
                    break;
                }

            }
           

        }

        /// <summary>
        /// Sets new milestone for level up
        /// </summary>
        protected void SetKillMilestone()
        {
            if (Level <= 5)
            {
                Milestone += Level;
            }
            else if (Level <= 10)
            {
                Milestone += 8;
            }
            else
            {
                Milestone += 10;
            }
        }

        /// <summary>
        /// Checks the amount of kills adds level and sets new milestone for level up
        /// </summary>
        protected void CheckLevel()
        {
            if (KillCount == Milestone)
            {
                Level++;
                SkillPoints += AmountOfSkillPoints();
                SetKillMilestone();
                if (Level % 2 == 0)
                {
                    //vyber abillitu
                }
            }
        }

        /// <summary>
        /// determines the amount of skill points player gets when he levels up
        /// </summary>
        /// <returns>Number of skill points</returns>
        protected int AmountOfSkillPoints()
        {
            if (Level <= 5)
                return 1;
            else if (Level <= 10)
                return 2;
            else if (10 < Level)
                return 3;
            else return 0;
        }

        /// <summary>
        /// Lets player to spedn his skill points to increase his stats
        /// </summary>
        protected void LevelUP()
        {
            for (int i = 0; i < SkillPoints; i++)
            {
                DoLine();
                Console.WriteLine(string.Format("Level: {8} {0} Health: {1} {0} Stamina: {2} {0} Strength: {3} {0} Dexterity: {4} {0} Endurance: {5} {0} Intelect: {6} {0} Spirit: {7} ", Environment.NewLine, MaximumHitPoints, MaximumStamina, Strength, Dexterity, Endurance, Intellect, Spirit, Level));
                Console.WriteLine("Choose which stat you wish to increase" + Environment.NewLine + " s = Strength | d = Dexterity | e = Endurance | i = Intelect | p = Spirit");
                string val = Console.ReadLine().ToLower();
                switch (val)
                {
                    case "s":
                        {
                            Strength++;
                            break;
                        }
                    case "d":
                        {
                            Dexterity++;
                            break;
                        }
                    case "e":
                        {
                            Endurance++;
                            break;
                        }
                    case "i":
                        {
                            Intellect++;
                            break;
                        }
                    case "p":
                        {
                            Spirit++;
                            break;
                        }
                }
            }

        }

        /// <summary>
        /// Method for more clarity 
        /// </summary>
        /// <returns> type here message in console and string player wrote into the console</returns>
        protected string TypeHere()
        {
            Console.Write("Type here:");
            return Console.ReadLine();
           

        }

        /// <summary>
        /// Reacts to Victory Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public override void OnDefeat(object source, EventArgs e)
        {
           
        }

        /// <summary>
        /// Reacts to Defeat Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        
      

        /// <summary>
        /// Massage displayed when player attacks
        /// </summary>
        /// <param name="args">Atributes of attack</param>
        protected virtual void AttackMessage(AttackEventArgs args)
        {
            Console.WriteLine(string.Format("(PLAYER) --{0}-- with {1} Hitpoints and {2} Stamina{3}Attacks for: {4} attack causes: {5} for {6} rounds",this.Name,this.CurrentHitPoints,CurrentStamina,Environment.NewLine,Math.Round(args.Amount) ,args.Effect,args.AttackEffectDuration ));
        }

        /// <summary>
        /// Formats information about abbillity
        /// </summary>
        /// <param name="abillity"> abillity which is supposted to be formated</param>
        /// <returns>String which contains formated information about abillity</returns>
        protected string AbillityInfoPrint(Abillity abillity)
        {
            return string.Format("|{0} === {1}|Stamina Cost: {2}|Health Cost: {3}|Effect: {4}|Duration: {5}|Cooldown: {6}|", abillity.Name.ToUpper(), abillity.Description, abillity.StaminaCost,abillity.HealthCost, abillity.Effect, abillity.AbillityDuration, abillity.Cooldown);
        }

         /// <summary>
         /// method that raises Attack event and writes message with info about attack
         /// </summary>
         /// <param name="args">atributes of the attack</param>
     

        /// <summary>
        /// reacts to arena event that offers player abillities
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnAbillityOffer(object source, AbillitiesEventArgs e)
        {
            int abillityIndex = 0;
            bool parseResult = false;
            bool abillityAdded = false;
            while (!abillityAdded)
            { 
                for (int i = 0; i < e.ListOfAbillities.Count; i++)
                {
                    Console.WriteLine(i + " : " + AbillityInfoPrint(e.ListOfAbillities[i]));
                }
                Console.WriteLine("Write the number of the abillity you wish to learn and press Enter");
            parseResult = int.TryParse(Console.ReadLine(), out abillityIndex);
            if (parseResult && abillityIndex < e.ListOfAbillities.Count)
            {
                this.Abillities.Add((Abillity)e.ListOfAbillities[abillityIndex].Clone());
                    abillityAdded = true;
            }
            else
            {
                Console.WriteLine("Something went Wrong pick again");
            }
            }
        }


        #region Overriden

        /// <summary>
        /// Reacts on oponents attack (Defense)
        /// </summary>
        /// <param name="source">Object that raised this event</param>
        /// <param name="e">atributes of the attack</param>
        public override void OnAttack(object source, AttackEventArgs e)
        {

            if (e.Amount == this.DefensePower * DefenseModifier)
            {
                Effects(e.Effect, e.AttackEffectDuration);
                Console.WriteLine(string.Format("(ThePlayer) {0} barely doged Oponents attack but was affected by its effect", this.Name));
               

            }
            else if (e.Amount > this.DefensePower * DefenseModifier)
            {
                Console.WriteLine(string.Format("(ThePlayer) {0} was hitby oponents attack", this.Name));
                Effects(e.Effect, e.AttackEffectDuration);
                HitPointsChanging((DefensePower * DefenseModifier) - e.Amount);
            }
            else if (e.Amount < this.DefensePower * DefenseModifier)
            {
                Console.WriteLine(string.Format("(ThePlayer) {0} doged oponents attack", this.Name));
            }
        }

        protected override void Attacking(AttackEventArgs args)
        {
            AttackMessage(args);
            base.Attacking(args);
        }

        protected override void Won()
        {
            KillCount++;
            CheckLevel();
            base.Won();
        }

        public override void OnHitPointsChange(object source, PointsEventArgs e)
        {

            Console.WriteLine("Enemy has: " + e.CurrentPoints + "HP");

        }
        public override void OnStaminaChange(object source, PointsEventArgs e)
        {
            Console.WriteLine("Enemy has:" + e.CurrentPoints + "");
        }
        #endregion

        #region Events
        /// <summary>
        /// Event that triggers when player levels up
        /// </summary>
        public event EventHandler LevelUp;

        /// <summary>
        /// method that triggers levelUp event
        /// </summary>
        protected virtual void LevelingUp()
        {
            if (LevelUp != null)
            {
                LevelUp(this, EventArgs.Empty);
            }
        }

        #endregion

    }
}
