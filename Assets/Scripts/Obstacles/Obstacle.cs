using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animator animator;
    public string AnimationName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        animator?.SetBool(AnimationName, true);
    }
}
