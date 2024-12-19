using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LineMinigame : MonoBehaviour
{
    public static LineMinigame Instance;

    [SerializeField] GameObject brush;

    [SerializeField] UILineRenderer currentLineRenderer;
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
        currentLineRenderer.AddPoint(MousePosition());
        /*        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                currentLineRenderer.SetPosition(0, worldPos);
                currentLineRenderer.SetPosition(1, worldPos);*/
    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.AddPoint(pointPos);
    }

    public void PointToMousePos()
    {
        Vector3 linePos = MousePosition();
        //Vector3 linePos = pos * 5;
        if (lastPos != linePos)
        {
            AddAPoint(linePos);
            lastPos = linePos;
        }
    }

    public void DeleteCurrentLine()
    {
        currentLineRenderer.ClearPoints();
    }
    Vector3 MousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = canvas.planeDistance; //distance of the plane from the camera 
        Vector3 screenPoint = Camera.main.ScreenToViewportPoint(mousePos);
        //* size of line game canvas
        RectTransform canvasRT = canvas.GetComponent<RectTransform>();
        float w = canvasRT.sizeDelta.x;
        float h = canvasRT.sizeDelta.y;
        screenPoint.x *= w;
        screenPoint.y *= h;



        return screenPoint;
    }

/*    private void OnDrawGizmos()
    {
        canvas = GetComponent<Canvas>();
        var screenPoint = Input.mousePosition;
        screenPoint.z = canvas.planeDistance; //distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector3 test = Camera.main.ScreenToViewportPoint(screenPoint);
        //* size of line game canvas
        RectTransform canvasRT = canvas.GetComponent<RectTransform>();
        float w = canvasRT.sizeDelta.x;
        float h = canvasRT.sizeDelta.y;
        test.x *= w;
        test.y *= h;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Camera.main.transform.position, mousePos);
        Debug.Log(test + ": "+ screenPoint);
    }*/
}

