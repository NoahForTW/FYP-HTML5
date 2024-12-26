using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePair : MonoBehaviour
{
    private Camera _mainCam;
    private float _CameraZDist;
    private Vector3 _initialPosition;
    private bool _connected;

    private const string _portTag = "Port";
    private const float _dragResponseThreshold = 2;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        _CameraZDist = _mainCam.WorldToScreenPoint(transform.position).z;
        SetInitialPosition(transform.position);
    }
    private void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _CameraZDist);
        Vector3 NewWorldPosition = _mainCam.ScreenToWorldPoint(ScreenPosition);
        transform.position = NewWorldPosition;
        
        if (!_connected )
        {
            transform.position = NewWorldPosition;
        }
        else if (Vector3.Distance(transform.position, NewWorldPosition) > _dragResponseThreshold)
        {
            _connected = false;
        }
    }
    private void OnMouseUp()
    {
        if (!_connected)
        {
            ResetPosition();
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void SetInitialPosition(Vector3 NewPosition)
    {
        _initialPosition = NewPosition;
        transform.position = _initialPosition;
    }
    private void ResetPosition()
    {
        transform.position = _initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Port"))
        {
            _connected = true;
            transform.position = other.transform.position;
            Debug.Log("In the Port Box");
        }
    }
}
