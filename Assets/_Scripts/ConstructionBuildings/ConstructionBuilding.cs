using UnityEngine;

namespace _Scripts.ConstructionBuildings
{
    public class ConstructionBuilding : MonoBehaviour
    {
        [field: SerializeField] public int CellSize { get; set; }

        [field: SerializeField] public int Width { get; set; }

        [field: SerializeField] public int Depth { get; set; }

        private void Awake()
        {
            transform.localScale = new Vector3(Width * CellSize, 1, Depth * CellSize);
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }

}