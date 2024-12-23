using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BooleanGame : MonoBehaviour
{
    [Tooltip("Assign a GameSlot Parent")]
    public GameObject GearGameParent;

    public TMP_Text boolValidation;

    public static BooleanGame Instance;

    public GearPiece gearPiece;

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
    public void ResetButton()
    {

    }
}
