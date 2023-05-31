using UnityEngine;

public class CropHolder : MonoBehaviour
{
    [SerializeField] 
    private CropScriptableObject _crop;

    public void GetCrop()
    {
        if (InteractionManager.Instance.SelectedCrop == _crop)
        {
            Debug.Log($"{_crop.Name} is already assigned!");
            return;
        }
        
        InteractionManager.Instance.SelectedCrop = _crop;
        Debug.Log($"{InteractionManager.Instance.SelectedCrop.Name}");
    }
}
