using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMinigame : MonoBehaviour
{
    public static LineMinigame Instance;

    [SerializeField] GameObject brush;

    GameObject brushInstance;
    LineRenderer currentLineRenderer;
    Vector3 lastPos;
    Canvas canvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBrush()
    {
        brushInstance = Instantiate(brush,canvas.transform);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector3 brushPos = MousePosition();

        currentLineRenderer.SetPosition(0, brushPos);
        currentLineRenderer.SetPosition(1, brushPos);

        /*        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                currentLineRenderer.SetPosition(0, worldPos);
                currentLineRenderer.SetPosition(1, worldPos);*/
    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    public void PointToMousePos()
    {
        Vector3 linePos = MousePosition();
        if (lastPos != linePos)
        {
            AddAPoint(linePos);
            lastPos = linePos;
        }
    }

    public void DeleteCurrentLine()
    {
        Destroy(brushInstance);
        currentLineRenderer = null;
    }
    Vector3 MousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = canvas.planeDistance; //distance of the plane from the camera 
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
