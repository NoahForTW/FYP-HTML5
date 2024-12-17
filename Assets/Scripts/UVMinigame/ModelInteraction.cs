using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    protected float originalSize;
    protected bool isDragging = false;

    protected virtual void Start()
    {
        originalSize = transform.localScale.x;
    }
    protected void OnMouseDrag()
    {
        isDragging = Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0;

    }
    protected void OnMouseUp()
    {
        isDragging = false;
    }
    protected virtual void MoveModel(float speed)
    {
        transform.Translate(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * speed, Space.World);
    }

    protected virtual void RotateModel(float speed)
    {
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0)
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
