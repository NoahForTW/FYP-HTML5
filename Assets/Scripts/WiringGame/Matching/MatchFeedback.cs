using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFeedback : MonoBehaviour
{
    public Material _matchMaterial;
    public Material _misMatchMaterial;

    private Renderer _renderer;

    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeMaterialWithMatch(bool IsCorrectMatch)
    {
        if (IsCorrectMatch)
        {
            _renderer.material = _matchMaterial;
        }
        else
        {
            _renderer.material = _misMatchMaterial;
        }
    }
}
