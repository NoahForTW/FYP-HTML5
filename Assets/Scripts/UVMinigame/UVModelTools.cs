using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UVTools
{
    None,
    Move, 
    Rotate,
    ZoomIn,
    ZoomOut
}
public class UVModelTools : MonoBehaviour
{
    public UVTools selectedTool = UVTools.None;

    [SerializeField] Button moveButtonUI;
    [SerializeField] Button rotateButtonUI;
    [SerializeField] Slider zoomSliderUI;

    [SerializeField] Material selectedButtonMat;

    [SerializeField] float increment;

    public void SelectTool(UVTools tool)
    {
        if (tool == UVTools.None) return;

        if (tool == UVTools.Move || tool == UVTools.Rotate)
        {
            moveButtonUI.targetGraphic.material =
                tool == UVTools.Move ? selectedButtonMat
                : null;
            rotateButtonUI.targetGraphic.material =
                tool == UVTools.Rotate ? selectedButtonMat
                : null;
            selectedTool = tool;
        }
        else
        {
            // do zoom shit
            OnSliderValueChange(tool);
        }
    }

    public void OnSliderValueChange(Slider slider)
    {
        UVTextureMinigame.Instance.UVToolsZoomEvent.Invoke(slider.value);
    }
    public void OnSliderValueChange(UVTools uVTools)
    {
        float value = zoomSliderUI.value;

        switch (uVTools)
        {
            case UVTools.ZoomIn:
                value += increment;
                if (value > 1) value = 1;
                break;

            case UVTools.ZoomOut:
                value -= increment;
                if (value < 0) value = 0;
                break;
        }
        zoomSliderUI.value = value;

        UVTextureMinigame.Instance.UVToolsZoomEvent.Invoke(value);
    }




}
