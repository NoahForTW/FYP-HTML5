using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    bool isDragging = false;
    public float rotationSpeed;
    Rigidbody rb;
    Vector3 lastMousePos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        isDragging = true;

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * rotationSpeed);
        }
    }

}
