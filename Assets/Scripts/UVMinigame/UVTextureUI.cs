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
        parentDuringDrag = UVTextureMinigame.Instance.UVTextureGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.collider.gameObject;
            UVModelSide side = go.GetComponent<UVModelSide>();
            if (side != null && !side.IsCurrentTextureCorrect())
            {
                Material material = new Material(go.GetComponent<MeshRenderer>().material);
                material.mainTexture = texture;
                Material[] materials = go.GetComponentInChildren<MeshRenderer>().materials;
                materials[0] = material;

                go.GetComponent<MeshRenderer>().materials = materials;
                side.PromptFeedback();
            }
        }
        canDrag = false;
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}
