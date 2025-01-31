using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UVTool : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] UVModelTools UVModelTools;
    [SerializeField] UVTools uvTool;
    public void OnPointerDown(PointerEventData eventData)
    {
        UVModelTools.SelectTool(uvTool);
    }
}
