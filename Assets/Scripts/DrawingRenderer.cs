using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingRenderer : MonoBehaviour
{
    public GameObject brush;
    Camera cam;
    [SerializeField] CinemachineVirtualCamera vCam;
    LineRenderer currentLineRenderer;
    Vector3 lastPos;
    GameObject brushInstance;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Drawing();
    }
    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            PointToMousePos();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            AddColliderToDrawing();
        }
        else
        {
            currentLineRenderer = null;
        }
    }
    void CreateBrush()
    {
        brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        currentLineRenderer.SetPosition(0, worldPos);
        currentLineRenderer.SetPosition(1, worldPos);
    }
    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (lastPos != worldPos)
        {
            AddAPoint(worldPos);
            lastPos = worldPos;
        }
    }

    void AddColliderToDrawing()
    {
        MeshCollider collider = brushInstance?.GetComponent<MeshCollider>();
        if (collider != null) { 
            Mesh mesh = new Mesh();
            currentLineRenderer.BakeMesh(mesh, true);
            collider.sharedMesh = mesh;
        }
    }
}
