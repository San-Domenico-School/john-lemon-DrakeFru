using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This class accepts user input to create player movement and align it with
 *  the player animation. Component of the Player.
 *  
 *  Drake Fruhauf
 *  May 15, 2023
 */
public class PlayerMovement : MonoBehaviour
{
    float turnSpeed;
    Animator animator;
    Rigidbody rb;
    Quaternion rotation;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        turnSpeed = 20f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
        rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetIsWalking();
        SetMovement();
        SetRotation();
    }

    // Sets the value of movement based on user input
    private void SetMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0f, vertical);
    }

    // Sets the value of the IsWalking parameter in the Animator based on the
    // value of the movement
    private void SetIsWalking()
    {
        if (!Mathf.Approximately(movement.magnitude, 0f))
        {
            animator.SetBool("IsWalking", true);
        }
    }

    // Sets the value of rotation based on the value of the movement
    private void SetRotation()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    // Moves and rotates the player based on an event from the Animator
    private void OnAnimatorMove()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
