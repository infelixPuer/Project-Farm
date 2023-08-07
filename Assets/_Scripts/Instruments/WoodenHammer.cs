using System.Collections;
using UnityEngine;
using _Scripts.ConstructionBuildings;
using _Scripts.UI;

namespace _Scripts.Instruments
{
    public class WoodenHammer : InstrumentBase
    {
        [Header("Wooden hammer specifics")]
        [SerializeField]
        private Canvas _constructionBuildingLoaderCanvas;
        
        public ConstructionBuilding Building { private get; set; }
        
        public override void MainAction()
        {
            Debug.Log("Building!");
        }

        public override void SecondaryAction()
        {
            UIManager.Instance.ShowCanvas(_constructionBuildingLoaderCanvas);
            StartCoroutine(CloseCanvas());
        }
        
        private IEnumerator CloseCanvas()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(1));
            UIManager.Instance.HideCanvas(_constructionBuildingLoaderCanvas);
        }
    }
}