using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEntity : MonoBehaviour
{
    public MatchFeedback _feedback;
    public MoveablePair _moveablePair;
    public MatchSystemManager _matchSystemManager;

    private bool _matched;
    
    public Vector3 GetMoveablePairPosition()
    {
        return _moveablePair.GetPosition();
    }

    public void SetMoveablePairPosition(Vector3 NewMoveablePairPosition)
    {
        _moveablePair.SetInitialPosition(NewMoveablePairPosition);
    }
    public void PairObjectInteraction(bool IsEnter, MoveablePair moveable)
    {
        if (IsEnter && !_matched)
        {
            _matched = (moveable == _moveablePair);
            if (_matched)
            {
                _matchSystemManager.NewMatchRecord(_matched);
            }
        }
        else if (!IsEnter && _matched)
        {
            _matched = !(moveable == _moveablePair);
            if (!_matched)
            {
                _matchSystemManager.NewMatchRecord(_matched);
            }
        }
    }
}
