using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableMinigame : MonoBehaviour
{
    public static VariableMinigame Instance;
    [SerializeField] public GameObject variablePieceParent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
