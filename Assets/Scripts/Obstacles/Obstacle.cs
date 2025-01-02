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
[RequireComponent(typeof(Animator))]
public class Obstacle : MonoBehaviour
{
    Animator Animator;
    public string AnimationName;
    public UnityEvent Event;
    public CompletionEffects completionEffect;
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }


    public void PlayAnimation()
    {
        Animator?.SetBool(AnimationName, true);
    }
}
