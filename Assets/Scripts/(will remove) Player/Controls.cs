using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float moveSpeed = 5f; // movement speed
    public Animator animator; // reference to the animator component
    public new Rigidbody rigidbody; // reference to the Rigidbody component

    private void FixedUpdate()
    {
        // get input from the user
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed * Time.deltaTime;

        // update the player's position
        transform.position += movement;

        // set the walking animation speed based on the player's movement speed
        //animator.SetFloat("Speed", movement.magnitude);
        if (movement == Vector3.zero)
        {
            animator.SetTrigger("idle");
            animator.ResetTrigger("walk");
        }
        else
        {
            animator.SetTrigger("walk");
            animator.ResetTrigger("idle");
        }

        // rotate the player to face the direction of movement
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}