using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UVModel : ModelInteraction
{
    Rigidbody rb;
    private void Awake()
    {
        UVTextureMinigame.Instance.UVToolsZoomEvent.AddListener(ZoomModel);
    }
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();

    }


    protected override void Update()
    {

        if (isDragging)
        {
            if (UVTextureMinigame.Instance.canModelRotate)
            {
                RotateModel(UVTextureMinigame.Instance.rotationSpeed);

            }
            else if (UVTextureMinigame.Instance.canModelMove)
            {
                MoveModel(0.25f);

                Vector3 clampedPosition = ClampToParentBounds(transform.position);

                // Apply the clamped position
                transform.position = clampedPosition;

            }

        }
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
