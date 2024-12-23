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

    // List to keep track of all gear pieces
    private List<GearPiece> gearPieces = new List<GearPiece>();

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

    private void Start()
    {
        // Find and store all GearPiece components under the GearGameParent
        GearPiece[] pieces = GearGameParent.GetComponentsInChildren<GearPiece>();
        gearPieces.AddRange(pieces);
    }

    public void ResetButton()
    {
        foreach (var gearPiece in gearPieces)
        {
            gearPiece.ResetPosition();
        }
    }
}
