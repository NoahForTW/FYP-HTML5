using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        isDragging = Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0;

        if (Input.GetAxis("Mouse X") == 0 &&
            Input.GetAxis("Mouse Y") == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
       /* if (Input.GetKey(KeyCode.Mouse0))
        {
            isDragging = Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDragging = false;
        }
        if (Input.GetAxis("Mouse X") == 0 &&
            Input.GetAxis("Mouse Y") == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }*/

/*        if (isDragging)
        {
            //transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * rotationSpeed);
        }*/


    }
    private void FixedUpdate()
    {
        if (isDragging)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed;

            rb.AddTorque(Vector3.right * y - Vector3.up * x);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 10);
    }
}
