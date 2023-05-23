using System;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] 
    private TextMeshPro _textMeshPro;

    public GridObject GridObject
    {
        set => _gridObject = value;
    }

    private GridObject _gridObject;

    private void Update()
    {
        _textMeshPro.text = _gridObject.ToString();
    }
}
