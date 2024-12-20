using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingArea : MonoBehaviour, 
    IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject drawingAreaStart;
    [SerializeField] GameObject drawingAreaEnd;

    public bool drawing = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("start drag: " + eventData.pointerCurrentRaycast.gameObject.name);

        GameObject clickedGO = eventData.pointerCurrentRaycast.gameObject;
        if (clickedGO == drawingAreaStart)
        {
            //start drawing 
            LineMinigame.Instance.CreateBrush();
            drawing = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (drawing)
        {
            // add point to line renderer
            LineMinigame.Instance.PointToMousePos();
        }
        //Debug.Log("dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (drawing)
        {
            if (eventData.pointerCurrentRaycast.gameObject != drawingAreaEnd)
            {
                // restart
                LineMinigame.Instance.DeleteCurrentLine();
            }
            else
            {
                // game finished 
                Debug.Log("DOnee");
                LineMinigame.Instance.Spawn3DLine();
            }

        }
        drawing = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // if exit the line, restart
        if(drawing)
        {
            LineMinigame.Instance.DeleteCurrentLine();
            drawing = false;
        }

    }

}
