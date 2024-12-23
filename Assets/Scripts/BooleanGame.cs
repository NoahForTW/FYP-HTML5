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

    public void DisplayValidation(string message, float delay)
    {
        StopAllCoroutines(); // Stop any ongoing coroutine to avoid overlapping
        boolValidation.text = message;
        boolValidation.gameObject.SetActive(true); // Ensure the text is visible
        StartCoroutine(HideTextAfterDelay(delay));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        boolValidation.gameObject.SetActive(false); // Hide the text after the delay
    }

    private void Start()
    {
        boolValidation.text = "";

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
