using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public TimerCanvas TimerCanvas;
    public GUICanvas GUICanvas;

    private void Awake()
    {
        Instance = this;
    }
}
