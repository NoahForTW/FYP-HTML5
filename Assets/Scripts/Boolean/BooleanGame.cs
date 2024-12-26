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

    // List to keep track of all gear pieces and slots
    private List<GearPiece> gearPieces = new List<GearPiece>();
    private List<GearSlot> gearSlots = new List<GearSlot>();

    private bool isBoolGameComplete;

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

        // Find and store all GearSlot components under the GearGameParent
        GearSlot[] slots = GearGameParent.GetComponentsInChildren<GearSlot>();
        gearSlots.AddRange(slots);
    }

    public void ResetButton()
    {
        foreach (var gearPiece in gearPieces)
        {
            gearPiece.ResetPosition();
        }
    }

    public void ValidateAllPieces()
    {
        foreach (var slot in gearSlots)
        {
            if (slot.IsSlotEmpty())
            {
                return;
            }
        }

        if (AllSlotsAreCorrect())
        {
            isBoolGameComplete = true;
        }
    }

    private bool AllSlotsAreCorrect()
    {
        foreach (GearSlot slot in gearSlots)
        {
            if (!slot.isGearPlaced)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (AllSlotsAreCorrect() && !isBoolGameComplete)
        {
            isBoolGameComplete = true;
            Debug.Log("Bool Game Done");
        }
    }
}
