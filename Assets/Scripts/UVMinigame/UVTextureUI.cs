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
        canDrag = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
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
}
