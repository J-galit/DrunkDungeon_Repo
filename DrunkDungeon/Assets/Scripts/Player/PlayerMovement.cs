using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;

    private Rigidbody2D rb;

    private Animator animator;

    [SerializeField] private float drunk = 1f;

    private float drunkSway = 0f;
    [SerializeField]
    private float swayRange = 1f;
    [SerializeField]
    private float swayFrequency = 1f;

    private enum Inputs
    {
        Up, Down, Left, Right
    };

    private Inputs lastInput = Inputs.Up;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the input direction.
        movement.Set(InputManager.movement.x, InputManager.movement.y);

        //Gets the last input.
        GetlastInput(ref lastInput);


        drunkSway = swayRange * Mathf.Sin(Time.time * swayFrequency);

        //print(drunkSway);

        rb.velocity = movement * moveSpeed;

        if (drunk > 0f)
        {
            //Adds the drunksway to either the x or y axis depending on the direction of the last input.
            if (lastInput == Inputs.Up || lastInput == Inputs.Down)
            {
                Vector2 currentVelocity = rb.velocity;
                currentVelocity.x += drunkSway;
                rb.velocity = currentVelocity;
            }
            else if (lastInput == Inputs.Right || lastInput == Inputs.Left)
            {
                Vector2 currentVelocity = rb.velocity;
                currentVelocity.y += drunkSway;
                rb.velocity = currentVelocity;

            }
        }

        //Moves the player based on velocity.
        
        
        if (lastInput == Inputs.Up) 
        {
            animator.SetInteger("Direction", 0);
        }
        else if (lastInput == Inputs.Down)
        {
            animator.SetInteger("Direction", 2);
        }
        else if (lastInput == Inputs.Right) 
        {
            animator.SetInteger("Direction", 1);
        }
        else if(lastInput == Inputs.Left)
        {
            animator.SetInteger("Direction", 1);
            
        }
       
    }


    Inputs GetlastInput(ref Inputs lastInput)
    {
        //Checks the vector2 of the newest input and determines the direction.
        if (InputManager.movement.y == 1f && InputManager.movement.x == 0)
        {
            lastInput = Inputs.Up;
            
        }
        else if (InputManager.movement.y == -1f && InputManager.movement.x == 0)
        {
            lastInput = Inputs.Down;
            
        }
        else if (InputManager.movement.y == 0f && InputManager.movement.x == 1f)
        {
            lastInput = Inputs.Right;
            
        }
        else if (InputManager.movement.y == 0f && InputManager.movement.x == -1f)
        {
            lastInput = Inputs.Left;
            
        }

        return lastInput;
    }
}
