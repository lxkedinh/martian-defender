using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// NOTE: The movement for this script uses the new InputSystem. The player needs to have a PlayerInput
// component added and the Behaviour should be set to Send Messages so that the OnMove and OnFire methods
// actually trigger

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public PlayMode playMode = PlayMode.Normal;

    private Vector2 moveInput;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Rigidbody2D rb;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {

        // rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.fixedDeltaTime));


        // Try to move player in input direction, followed by left right and up down input if failed
        bool success = MovePlayer(moveInput);

        if (!success)
        {
            // Try Left / Right
            success = MovePlayer(new Vector2(moveInput.x, 0));

            if (!success)
            {
                success = MovePlayer(new Vector2(0, moveInput.y));
            }
        }
    }

    // Tries to move the player in a direction by casting in that direction by the amount
    // moved plus an offset. If no collisions are found, it moves the players
    // Returns true or false depending on if a move was executed
    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0)
        {
            Vector2 moveVector = moveSpeed * Time.fixedDeltaTime * direction;

            // No collisions
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnChangeBuildObject()
    {
        MapController.Instance.ChangeObjectPlacementType();
    }

    public void OnPlayerMode()
    {
        if (playMode == PlayMode.Normal)
        {
            playMode = PlayMode.Build;
            return;
        }

        playMode = PlayMode.Normal;
    }
}

public enum PlayMode
{
    Normal,
    Build,
    Delete
}
