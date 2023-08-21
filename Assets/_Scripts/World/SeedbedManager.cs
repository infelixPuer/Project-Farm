using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.World
{
    public class SeedbedManager : MonoBehaviour
    {
        public List<Seedbed> Seedbeds { get; private set; } = new List<Seedbed>();
        
        public static SeedbedManager Instance;
        public bool IsTooltipVisible;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void AddSeedbed(Seedbed seedbed)
        {
            Seedbeds.Add(seedbed);
        }
        
        public void RemoveSeedbed(Seedbed seedbed)
        {
            Seedbeds.Remove(seedbed);
        }
    }
}