using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawingRenderer : MonoBehaviour
{
    public static DrawingRenderer instance;
    public bool canDraw;
    [SerializeField] List<Color> colors = new List<Color>();
    [SerializeField] GameObject brush;
    [SerializeField] GameObject brushColorUI;
    [SerializeField] GameObject colorPalette;
    Camera cam;
    [SerializeField] CinemachineVirtualCamera vCam;
    LineRenderer currentLineRenderer;
    Vector3 lastPos;
    GameObject brushInstance;
    Color brushPrefabColor;

    public UnityEvent<Color> colorChange;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        cam = Camera.main;
        foreach (var c in colors)
        {
            GameObject colorUI = Instantiate(brushColorUI, colorPalette.transform);
            colorUI.GetComponent<Image>().color = c;

        }
        brushPrefabColor = brush.GetComponent<LineRenderer>().endColor;
        colorChange.AddListener(ChangeBrushColor);
    }

    // Update is called once per frame
    void Update()
    {
        Drawing();
    }

    void ChangeBrushColor(Color color)
    {
        brush.GetComponent<LineRenderer>().startColor = color;
        brush.GetComponent<LineRenderer>().endColor = color;
    }
    void Drawing()
    {
        if (!canDraw) { return; }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            PointToMousePos();
            Debug.Log(EventSystem.current.IsPointerOverGameObject());
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

    private void OnDisable()
    {
        brush.GetComponent<LineRenderer>().startColor = brushPrefabColor;
        brush.GetComponent<LineRenderer>().endColor = brushPrefabColor;
    }

}
