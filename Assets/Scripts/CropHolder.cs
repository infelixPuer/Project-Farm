using UnityEngine;

public class CropHolder : MonoBehaviour
{
    [SerializeField] 
    private CropScriptableObject _crop;

    public void GetCrop()
    {
        if (InteractionManager.Instance.crop == _crop)
        {
            Debug.Log($"{_crop.Name} is already assigned!");
            return;
        }
        
        InteractionManager.Instance.crop = _crop;
        Debug.Log($"{InteractionManager.Instance.crop.Name}");
    }
}
