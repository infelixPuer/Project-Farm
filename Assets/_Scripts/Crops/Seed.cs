using UnityEngine;

namespace _Scripts.Crops
{
    public class Seed : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _cropPrefab;
        
        public GameObject CropPrefab => _cropPrefab;
    }
}