using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private Canvas canvas;
    public LineRenderer lineRenderer;
    bool Dragging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = canvas.planeDistance;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = transform.position.z;
            transform.position = convertedMousePosition;

            Vector3 positionDifference = convertedMousePosition - lineRenderer.transform.position;
            lineRenderer.SetPosition(2, positionDifference);
        }
    }

    private void OnMouseDown()
    {
        Dragging = true;
    }

    private void OnMouseUp()
    {
        Dragging = false; 
    }
}
