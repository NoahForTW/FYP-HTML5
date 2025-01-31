using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_Usage : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private lr_Controller lineR;

    private void Start()
    {
        lineR.SetUpLine(points);
    }
}
