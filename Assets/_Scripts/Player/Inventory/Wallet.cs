using System;
using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class Wallet
    {
        private int _balance;
        public int Balance
        {
            get => _balance;
            private set
            {
                if (_balance != value)
                {
                    _balance = value;
                    OnBalanceChanged(value);
                }
            }
        }
        
        public event EventHandler<int> BalanceChanged; 
        
        protected virtual void OnBalanceChanged(int e)
        {
            BalanceChanged?.Invoke(this, e);
        }

        public Wallet() => Balance = 0;

        public void AddMoney(int amount) => Balance += amount;

        public void RemoveMoney(int amount)
        {
            try
            {
                if (amount > Balance)
                    throw new Exception("Not enough money!");
            
                Balance -= amount;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}