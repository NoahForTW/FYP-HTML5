using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UVTextureUI : DragDrop
{
    [SerializeField] public Texture texture;
    RawImage image;
    public LayerMask layerMask;
    public bool canDrag = false;
    void Start()
    {
        image = GetComponent<RawImage>();
        image.texture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDrag)
        {

        }


    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        transform.SetParent(UVTextureMinigame.Instance.UVTextureGameObject.transform);
        canDrag = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.collider.gameObject;
            UVModelSide side = go.GetComponent<UVModelSide>();
            if (side != null && side.GetCanChangeTexture())
            {
                Material material = new Material(go.GetComponent<MeshRenderer>().material);
                material.mainTexture = texture;
                go.GetComponent<MeshRenderer>().material = material;
            }
        }
        canDrag = false;
    }

    private void OnDrawGizmos()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = canvas.planeDistance; //distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        //mousePos.z = transform.position.z;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Camera.main.transform.position, mousePos);
        Debug.Log(mousePos + " , "+transform.position.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mousePos, mousePos + Camera.main.transform.forward * 5);
    }
}
