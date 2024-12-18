using UnityEngine;

public class Port : MonoBehaviour
{
    public MatchEntity _ownerMatchEntity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MoveablePair CollidedMoveable))
        {
            _ownerMatchEntity.PairObjectInteraction(true, CollidedMoveable);
            Debug.Log("aoubndwiuawdbuabwd");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out MoveablePair CollidedMoveable))
        {
            _ownerMatchEntity.PairObjectInteraction(false,CollidedMoveable);
        }
    }
}
