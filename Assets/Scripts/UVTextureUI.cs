using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UVTextureUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Texture2D texture;
    Image image;
    public LayerMask layerMask;
    public bool canDrag = false;
    void Start()
    {
        image = GetComponent<Image>();
        Material mat = new Material(image.material);
        mat.mainTexture = texture;
        image.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDrag)
        {
            DragUI();
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.transform.gameObject.name);
            Debug.DrawLine(ray.origin, hit.point);
        }
    }

    void DragUI()
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canDrag= false;
    }
}
