using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    protected float originalSize;
    protected bool isDragging = false;

    Vector3 mouseDelta;
    private Vector3 previousMousePosition;

    protected virtual void Start()
    {
        originalSize = transform.localScale.x;
    }
    protected void OnMouseDrag()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        mouseDelta = currentMousePosition - previousMousePosition;

        // Determine if the mouse is moving
        isDragging = mouseDelta.magnitude > 0;
        //Debug.Log($"Mouse Delta: {mouseDelta}");

        // Update the previous mouse position
        previousMousePosition = currentMousePosition;

    }
    protected void OnMouseUp()
    {
        isDragging = false;
    }
    protected virtual void MoveModel(float speed)
    {
        transform.Translate(mouseDelta.normalized * speed, Space.World);
    }

    protected virtual void RotateModel(float speed)
    {
        transform.Rotate(new Vector3(mouseDelta.y, -mouseDelta.x, 0)
                    * speed
                    , Space.World);
    }

    protected virtual void ZoomModel(float value)
    {
        float newScale = originalSize * (1 + 2 * value) / 3f;
        transform.localScale =
            new Vector3(newScale, newScale, newScale);
    }

    protected virtual void Update()
    {
        if (isDragging)
        {
            RotateModel(UVTextureMinigame.Instance.rotationSpeed);

            //MoveModel(0.25f);

        }
    }
}
