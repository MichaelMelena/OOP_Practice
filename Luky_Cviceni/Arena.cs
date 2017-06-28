using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class Arena
    {
        private Player ThePlayer { get; set; }
        private Character Opponent { get; set; }
        List<Abillity> Abillities { get; set; }

        bool BattleInProgress { get; set; }

        public Arena(Player player)
        {
            this.ThePlayer = player;
            Abillities = new List<Abillity>();
          

        }
        public void Start()
        {
            
            PreapareBattle();
            PlayerSetUp();
            Battle();
        }

        private void Battle()
        {
            BattleInProgress = true;
            while(BattleInProgress)
            {
                ThePlayer.PlayRound();
            }

        }
        private void PreapareBattle()
        {
            Opponent = CreateNewEnemy();
            SUbscribe();
        }
        private void PlayerSetUp()
        {
            AbillityOffering();
        }

        private void SUbscribe()
        {
            ThePlayer.Attack += Opponent.OnAttack;
            ThePlayer.Blind += Opponent.OnBlind;
            ThePlayer.Buff += Opponent.OnBuff;
            ThePlayer.Debuff += Opponent.OnDebuff;
            ThePlayer.Heal += Opponent.OnHeal;
            ThePlayer.HitPointsChange += Opponent.OnHitPointsChange;
            ThePlayer.Silence += Opponent.OnSilence;
            ThePlayer.StaminaChange += Opponent.OnStaminaChange;
            ThePlayer.Stun += Opponent.OnStun;
            ThePlayer.Victory += Opponent.OnDefeat;
            ThePlayer.Victory += this.OnEndOfBattle;
            ThePlayer.Victory += this.OnOponentDefeat;

            this.AbillityOffer += ThePlayer.OnAbillityOffer;
            this.StartOfRound += ThePlayer.OnStartOfRound;
            this.EndOfRound += ThePlayer.OnEndofRound;
            


            Opponent.Attack += ThePlayer.OnAttack;
            Opponent.Blind += ThePlayer.OnBlind;
            Opponent.Buff += ThePlayer.OnBuff;
            Opponent.Debuff += ThePlayer.OnDebuff;
            Opponent.Heal += ThePlayer.OnHeal;
            Opponent.HitPointsChange += ThePlayer.OnHitPointsChange;
            Opponent.Silence += ThePlayer.OnSilence;
            Opponent.StaminaChange += ThePlayer.OnStaminaChange;
            Opponent.Stun += ThePlayer.OnStun;
            Opponent.Victory += ThePlayer.OnDefeat;
            Opponent.Victory += this.OnEndOfBattle;

            this.StartOfRound += Opponent.OnStartOfRound;
            this.EndOfRound += Opponent.OnEndofRound;
        }

        private void UnSubscribe()
        {
            ThePlayer.Attack -= Opponent.OnAttack;
            ThePlayer.Blind -= Opponent.OnBlind;
            ThePlayer.Buff -= Opponent.OnBuff;
            ThePlayer.Debuff -= Opponent.OnDebuff;
            ThePlayer.Heal -= Opponent.OnHeal;
            ThePlayer.HitPointsChange -= Opponent.OnHitPointsChange;
            ThePlayer.Silence -= Opponent.OnSilence;
            ThePlayer.StaminaChange -= Opponent.OnStaminaChange;
            ThePlayer.Stun -= Opponent.OnStun;
            ThePlayer.Victory -= Opponent.OnDefeat;
            ThePlayer.Victory -= this.OnEndOfBattle;
            ThePlayer.Victory -= this.OnOponentDefeat;

            this.AbillityOffer -= ThePlayer.OnAbillityOffer;
            this.StartOfRound -= ThePlayer.OnStartOfRound;
            this.EndOfRound -= ThePlayer.OnEndofRound;

            Opponent.Attack -= ThePlayer.OnAttack;
            Opponent.Blind -= ThePlayer.OnBlind;
            Opponent.Buff -= ThePlayer.OnBuff;
            Opponent.Debuff -= ThePlayer.OnDebuff;
            Opponent.Heal -= ThePlayer.OnHeal;
            Opponent.HitPointsChange -= ThePlayer.OnHitPointsChange;
            Opponent.Silence -= ThePlayer.OnSilence;
            Opponent.StaminaChange -= ThePlayer.OnStaminaChange;
            Opponent.Stun -= ThePlayer.OnStun;
            Opponent.Victory -= ThePlayer.OnDefeat;
            Opponent.Victory += this.OnEndOfBattle;

            this.StartOfRound -= Opponent.OnStartOfRound;
            this.EndOfRound -= Opponent.OnEndofRound;
        }

        private void Player_StaminaChange(object sender, PointsEventArgs e)
        {
            throw new NotImplementedException();
        }

        

        private Character CreateNewEnemy()
        {
            return new Dummy(70);///test code
        }
        public void AddAbillity(Abillity abillity)
        {
            Abillities.Add(abillity);
        }

        #region Events
        public event EventHandler EndOfRound;
        protected virtual void EndingOfRound()
        {
            if (EndOfRound != null)
            {
                EndOfRound(this, EventArgs.Empty);
            }
        }

        public event EventHandler StartOfRound;
        protected virtual void StartingOfRound()
        {
            if (StartOfRound != null)
            {
                StartOfRound(this, EventArgs.Empty);
            }
        }
        public event EventHandler<AbillitiesEventArgs> AbillityOffer;

        private void AbillityOffering()
        {
            if (AbillityOffer != null)
            {
                AbillityOffer(this, new AbillitiesEventArgs() { ListOfAbillities= this.Abillities });
            }
        }

       
       
        #endregion
        protected virtual void OnEndOfBattle(object source, EventArgs e)
        {
            BattleInProgress = false;
        }
        protected virtual void OnOponentDefeat(object source, EventArgs e)
        {
            UnSubscribe();
            CreateNewEnemy();
            CreateNewEnemy();
            SUbscribe();
            Battle();
        }
    }
}
