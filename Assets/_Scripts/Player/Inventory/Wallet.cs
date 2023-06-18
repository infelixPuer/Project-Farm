using System;
using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class Wallet
    {
        public int Balance { get; private set; }

        public Wallet()
        {
            Balance = 0;
        }
        
        public void AddMoney(int amount)
        {
            Balance += amount;
            Debug.Log("Money added!" +
                      $"\nCurrent balance: {Balance}");
        }
        
        public void RemoveMoney(int amount)
        {
            try
            {
                if (amount > Balance)
                    throw new Exception("Not enough money!");
            
                Balance -= amount;
                Debug.Log("Money removed!" +
                          $"\nCurrent balance: {Balance}");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}