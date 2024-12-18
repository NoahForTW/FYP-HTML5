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

    GameObject brushInstance;
    UILineRenderer currentLineRenderer;
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
        currentLineRenderer = brushInstance.GetComponent<UILineRenderer>();

        brushInstance.transform.position = MousePosition();
        RectTransform rectTransform = brushInstance.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(
            rectTransform.localPosition.x,
            rectTransform.localPosition.y,
            0);
        currentLineRenderer.points.Add(Vector3.zero);

        /*        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                currentLineRenderer.SetPosition(0, worldPos);
                currentLineRenderer.SetPosition(1, worldPos);*/
    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.points.Add(pointPos);
    }

    public void PointToMousePos()
    {
        Vector3 pos = MousePosition() - brushInstance.transform.position;
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
        Destroy(brushInstance);
        currentLineRenderer = null;
    }
    Vector3 MousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = canvas.planeDistance; //distance of the plane from the camera 
        return new Vector3(Camera.main.ScreenToWorldPoint(mousePos).x,
                Camera.main.ScreenToWorldPoint(mousePos).y, 0);
    }

    private void OnDrawGizmos()
    {
        canvas = GetComponent<Canvas>();
        var screenPoint = Input.mousePosition;
        screenPoint.z = canvas.planeDistance; //distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector3 test = Camera.main.ScreenToViewportPoint(screenPoint);
        //* size of line game canvas
        test.x *= 720;
        test.y *= 405;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Camera.main.transform.position, mousePos);
        Debug.Log(test + ": "+ screenPoint);
    }
}

