using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class UVModel : MonoBehaviour
{
    bool isDragging = false;
    Rigidbody rb;
    float originalSize;
    private void Awake()
    {
        UVTextureMinigame.Instance.UVToolsZoomEvent.AddListener(HandleUVToolsZoomEvent);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSize = transform.localScale.x;
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
        if (isDragging)
        {
            if (UVTextureMinigame.Instance.canModelRotate)
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0)
                    * UVTextureMinigame.Instance.rotationSpeed
                    , Space.World);

            }
            else if (UVTextureMinigame.Instance.canModelMove)
            {
                transform.Translate(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * 0.25f, Space.World);

                Vector3 clampedPosition = ClampToParentBounds(transform.position);

                // Apply the clamped position
                transform.position = clampedPosition;

            }

        }
    }

    void HandleUVToolsZoomEvent(float value)
    {
        float newScale = originalSize * (1 + 2 * value) / 3f;
        transform.localScale =
            new Vector3(newScale, newScale, newScale);
    }
    Vector3 ClampToParentBounds(Vector3 worldPosition)
    {
        RectTransform parentRectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
        // Get the corners of the parent RectTransform in world space
        Vector3[] worldCorners = new Vector3[4];
        parentRectTransform.GetWorldCorners(worldCorners);

        // Calculate bounds from corners
        Vector3 minBounds = worldCorners[0]; // Bottom-left corner
        Vector3 maxBounds = worldCorners[2]; // Top-right corner

        // Clamp the position within these bounds
        float clampedX = Mathf.Clamp(worldPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(worldPosition.y, minBounds.y, maxBounds.y);

        // Return the clamped position
        return new Vector3(clampedX, clampedY, worldPosition.z);
    }

    /*    private void OnDrawGizmos()
        {*//*
            var screenPoint = Input.mousePosition;
            screenPoint.z = modelCanvas.planeDistance; //distance of the plane from the camera
            Vector3 mousePos = modelCanvas.worldCamera.ScreenToWorldPoint(screenPoint);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(modelCanvas.worldCamera.transform.position, mousePos);*//*
        }*/
}
