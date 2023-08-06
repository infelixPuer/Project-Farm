using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.NPCes
{
    public class NPCManager : MonoBehaviour
    {
        public List<NPC> NPCList { get; private set; }
        
        public static NPCManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
            
            NPCList = GetComponentsInChildren<NPC>().ToList();
        }

        public void ToogleNPCMovement(bool isMoving)
        {
            NPCList.ForEach(x => x.IsMoving = isMoving);
        }
    }
}