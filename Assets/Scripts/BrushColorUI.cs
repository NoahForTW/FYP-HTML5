using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrushColorUI : MonoBehaviour
{
    private void Start()
    {
        //DrawingRenderer.instance.colorChange(changeBrush)
    }

    public void ChangeBrush()
    {
        DrawingRenderer.instance.colorChange.Invoke(GetComponent<Image>().color);
    }


}
