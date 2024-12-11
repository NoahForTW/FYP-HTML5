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

    [SerializeField] Material moveButtonMat;
    [SerializeField] Material rotateButtonMat;

    public void SelectTool(UVTools tool)
    {
        if (tool == UVTools.None) return;

        if (tool == UVTools.Move || tool == UVTools.Rotate)
        {
            moveButtonUI.image.material =
                tool == UVTools.Move ? moveButtonMat
                : null;
            rotateButtonUI.image.material =
                tool == UVTools.Rotate ? rotateButtonMat
                : null;
            selectedTool = tool;
        }
        else
        {
            // do zoom shit
        }
    }




}
