using UnityEngine;

[CreateAssetMenu(fileName = "New Crop type", menuName = "SOs/Add new Crop", order = 1)]
public class CropScriptableObject : ScriptableObject
{
    public string Name;
    public int GrowingTime;
    public int FrequencyOfWateringInDays;
    public int Output;
    public GameObject[] PhasesOfGrowing;
}
