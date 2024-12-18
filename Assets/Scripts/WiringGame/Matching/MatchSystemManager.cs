using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchSystemManager : MonoBehaviour
{
    public List<Material> _colorMaterials;
    private List<MatchEntity> _matchEntities;
    private int _targetMatchCount;
    private int _currentMatchCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _matchEntities = transform.GetComponentsInChildren<MatchEntity>().ToList();
        _targetMatchCount = _matchEntities.Count;
        SetEntityColours();
    }

    void SetEntityColours()
    {
        Shuffle(_colorMaterials);

        for (int i = 0; 1 < _matchEntities.Count; i++)
        {
            _matchEntities[i].SetMaterialToPairs(_colorMaterials[i]);
        }
    }

    // Update is called once per frame
    public void NewMatchRecord(bool MatchConnected)
    {
        if (MatchConnected)
        {
            _currentMatchCount++;
        }
        else
        {
            _currentMatchCount--;
        }

        if(_currentMatchCount == _targetMatchCount)
        {
            //woo all paired
        }
    }

    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
