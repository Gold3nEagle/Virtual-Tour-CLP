using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;

    }

    public void TurnOnFan()
    {
        animator.speed = 1;
    }
}
