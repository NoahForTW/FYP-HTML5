using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchSystemManager : MonoBehaviour
{
    private List<MatchEntity> _matchEntities;
    private int _targetMatchCount;
    private int _currentMatchCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _matchEntities = transform.GetComponentsInChildren<MatchEntity>().ToList();
        _targetMatchCount = _matchEntities.Count;
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
}
