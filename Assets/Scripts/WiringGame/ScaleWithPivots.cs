using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithPivots : MonoBehaviour
{

    public Transform StartPivot;
    public Transform EndPivot;
    private Vector3 InitialScale;

    // Start is called before the first frame update
    private void Start()
    {
        InitialScale = transform.localScale;
        UpdateTransformForScale();
    }

    // Update is called once per frame
    void Update()
    {
        if ((StartPivot.transform.hasChanged || EndPivot.transform.hasChanged))
        {
            UpdateTransformForScale();
        }
    }

    void UpdateTransformForScale()
    {
        float distance = Vector3.Distance(StartPivot.transform.position, EndPivot.transform.position);
        transform.localScale = new Vector3(InitialScale.x, distance, InitialScale.z);

        Vector3 middlePoint = (StartPivot.transform.position + EndPivot.transform.position) / 2f ;
        transform.position = middlePoint;

        Vector3 rotationDirection = (EndPivot.position - StartPivot.position);
        transform.up = rotationDirection;

    }

}
