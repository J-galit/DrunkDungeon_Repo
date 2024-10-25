using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;

    private Rigidbody2D rb;

    [SerializeField] private float drunk = 1f;

    private float drunkSway = 0.1f;
    [SerializeField]
    private float swayRange = 1f;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.Set(InputManager.movement.x, InputManager.movement.y);
        
        GetlastInput(ref lastInput);

        drunkSway = swayRange * Mathf.Sin(Time.time * swayFrequency);


        rb.velocity = movement * moveSpeed;


        if (drunk > 0f)
        {
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

        

    }


    Inputs GetlastInput(ref Inputs lastInput)
    {

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
