using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private int horizontal, vertical;
    private bool animationSnapping = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        // Animation Snapping 
        float snappedHorizontal = 0.0f;
        float snappedVertical = 0.0f;

        if (animationSnapping)
        {
            #region Snapped Horizontal
            if (horizontalMovement > 0.0f && horizontalMovement < 0.35f)
            {
                snappedHorizontal = 0.33f; // Walk animation
            }
            else if (horizontalMovement > 0.35f)
            {
                snappedHorizontal = 0.66f; // Jog animation
            }
            else if (horizontalMovement < 0.0f && horizontalMovement > -0.35f)
            {
                snappedHorizontal = -0.33f; // Walk animation
            }
            else if (horizontalMovement < -0.35f)
            {
                snappedHorizontal = -0.66f; // Jog animation
            }
            else
            {
                snappedHorizontal = 0.0f;
            }
            #endregion

            #region Snapped Vertical
            if (verticalMovement > 0.0f && verticalMovement < 0.35f)
            {
                snappedVertical = 0.33f; // Walk animation
            }
            else if (verticalMovement > 0.35f)
            {
                snappedVertical = 0.66f; // Jog animation
            }
            else if (verticalMovement < 0.0f && verticalMovement > -0.35f)
            {
                snappedVertical = -0.33f; // Walk animation
            }
            else if (verticalMovement < -0.35f)
            {
                snappedVertical = -0.66f; // Jog animation
            }
            else
            {
                snappedVertical = 0.0f;
            }
            #endregion
        }

        animator.SetFloat(horizontal, !animationSnapping ? horizontalMovement : snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, !animationSnapping ? verticalMovement : snappedVertical, 0.1f, Time.deltaTime);
    }
}
