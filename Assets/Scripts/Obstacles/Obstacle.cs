using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum CompletionEffects
{
    None,
    Deactivate,
    PlayAnimation
}
public class Obstacle : MonoBehaviour
{
    Animator Animator;
    public string AnimationName;
    public UnityEvent Event;
    public CompletionEffects CompletionEffect;
    private void Start()
    {
        Animator = GetComponent<Animator>();
        Event.AddListener(DoCompletionEvent);
    }

    public void DoCompletionEvent()
    {
        switch (CompletionEffect)
        {
            case global::CompletionEffects.Deactivate:
                StartCoroutine(DeactivateGameObject(this.gameObject, 2f));
                break;
            case global::CompletionEffects.PlayAnimation:
                PlayAnimation();
                break;
        }
    }
    public void PlayAnimation()
    {
        Animator?.SetBool(AnimationName, true);
    }

    IEnumerator DeactivateGameObject(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        go.SetActive(false);
    }
}
