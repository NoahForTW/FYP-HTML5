using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFeedback : MonoBehaviour
{
    public Material _matchMaterial;
    public Material _misMatchMaterial;
    public Material _defaultMaterial;

    private Renderer _renderer;

    // Start is called before the first frame update
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ResetMaterial()
    {
        _renderer.material = _defaultMaterial;
    }
    public void ChangeMaterialWithMatch(bool IsCorrectMatch)
    {
        _renderer.material = IsCorrectMatch? _matchMaterial : _misMatchMaterial;
     /*   if (IsCorrectMatch)
        {
            _renderer.material = _matchMaterial;
        }
        else
        {
            _renderer.material = _misMatchMaterial;
        }*/
    }
}
