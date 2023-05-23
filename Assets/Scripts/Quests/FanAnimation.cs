using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAnimation : MonoBehaviour
{
    public static Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;

    }

    public void TurnOnFan()
    {
        animator.speed = 1;
    }
}
