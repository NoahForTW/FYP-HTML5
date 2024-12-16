using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouseDrag : MonoBehaviour
{
    private Camera mainCam;
    private float CameraZDist;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        CameraZDist = mainCam.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseUp()
    {
        transform.hasChanged = false;
    }

    private void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDist);
        Vector3 NewWorldPosition = mainCam.ScreenToWorldPoint(ScreenPosition);
        transform.position = NewWorldPosition;
    }
}
