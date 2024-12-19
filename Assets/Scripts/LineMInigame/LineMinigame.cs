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

    [SerializeField] GameObject LineRendererPrefab;
    [SerializeField] UILineRenderer currentLineRenderer;
    Vector3 lastPos;
    Canvas canvas;

    [SerializeField] GameObject LineParent3D;

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

    public void Spawn3DLine()
    {
        int totalPoints = currentLineRenderer.points.Count;
        float multiplier = LineParent3D.transform.localScale.x 
             / Mathf.Abs(
            currentLineRenderer.points[0].x - currentLineRenderer.points[totalPoints - 1].x);
        GameObject line3d = Instantiate(LineRendererPrefab);
        line3d.transform.position = Vector3.zero;
        LineRenderer lineRenderer = line3d.GetComponent<LineRenderer>();
        for (int i = 0; i < currentLineRenderer.points.Count; i++)
        {
            Vector2 difference = currentLineRenderer.points[i] - currentLineRenderer.points[0];
            Vector3 positionFromStart = new Vector3(
                LineParent3D.transform.position.x - LineParent3D.transform.localScale.x * 0.5f,
                LineParent3D.transform.position.y,
                LineParent3D.transform.position.z);

            // add point
            if (i > 1) lineRenderer.positionCount++;
            lineRenderer.SetPosition(i, positionFromStart + (Vector3)difference * multiplier);
        }
        AddColliderToDrawing(line3d, lineRenderer);


    }

    void AddColliderToDrawing(GameObject drawing, LineRenderer lineRenderer)
    {
        MeshCollider collider = drawing?.GetComponent<MeshCollider>();
        if (collider != null)
        {
            Mesh mesh = new Mesh();
            lineRenderer.BakeMesh(mesh, true);
            collider.sharedMesh = mesh;
        }
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

