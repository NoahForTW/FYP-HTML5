using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UVModel : MonoBehaviour
{
    bool isDragging = false;
    Rigidbody rb;
    Vector3 lastMousePos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
      
    }

    private void OnMouseDrag()
    {   
        isDragging = Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0;

/*        if (Input.GetAxis("Mouse X") == 0 &&
            Input.GetAxis("Mouse Y") == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }*/
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging && UVTextureMinigame.Instance.canModelRotate)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0)
                * UVTextureMinigame.Instance.rotationSpeed
                , Space.World);

        }

    }

    private void OnDrawGizmos()
    {/*
        var screenPoint = Input.mousePosition;
        screenPoint.z = modelCanvas.planeDistance; //distance of the plane from the camera
        Vector3 mousePos = modelCanvas.worldCamera.ScreenToWorldPoint(screenPoint);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(modelCanvas.worldCamera.transform.position, mousePos);*/
    }
}
