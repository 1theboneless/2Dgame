﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("GameObject/Component References")]
    [Tooltip("The animator controller used to animate the player.")]
    public RuntimeAnimatorController animator = null;
    [Tooltip("The Rigidbody2D component to use in \"Astroids Mode\".")]
    public Rigidbody2D myRigidbody = null;

    [Header("Movement Variables")]
    [Tooltip("The speed at which the player will move.")]
    public float moveSpeed = 10.0f;
    [Tooltip("The speed at which the player rotates in asteroids movement mode")]
    public float rotationSpeed = 60f;

    //The InputManager to read input from
    private InputManager inputManager;

    public enum AimModes { AimTowardsMouse, AimForwards };

    [Tooltip("The aim mode in use by this player:\n" +
        "Aim Towards Mouse: Player rotates to face the mouse\n" +
        "Aim Forwards: Player aims the direction they face (doesn't face towards the mouse)")]
    public AimModes aimMode = AimModes.AimTowardsMouse;


    public enum MovementModes { MoveHorizontally, MoveVertically, FreeRoam, Astroids };

    [Tooltip("The movmeent mode used by this controller:\n" +
        "Move Horizontally: Player can only move left/right\n" +
        "Move Vertically: Player can only move up/down\n" +
        "FreeRoam: Player can move in any direction and can aim\n" +
        "Astroids: Player moves forward/back in the direction they are facing and rotates with horizontal input")]
    public MovementModes movementMode = MovementModes.FreeRoam;


    private bool canAimWithMouse
    {
        get
        {
            return aimMode == AimModes.AimTowardsMouse;
        }
    }


    private bool lockXCoordinate
    {
        get
        {
            return movementMode == MovementModes.MoveVertically;
        }
    }

    public bool lockYCoordinate
    {
        get
        {
            return movementMode == MovementModes.MoveHorizontally;
        }
    }


    private void Start()
    {
        SetupInput();
    }


    void Update()
    {
        // Collect input and move the player accordingly
        HandleInput();
        // Sends information to an animator component if one is assigned
        SignalAnimator();
    }


    private void SetupInput()
    {
        if (inputManager == null)
        {
            inputManager = InputManager.instance;
        }
        if (inputManager == null)
        {
            Debug.LogWarning("There is no player input manager in the scene, there needs to be one for the Controller to work");
        }
    }


    private void HandleInput()
    {
        // Find the position that the player should look at
        Vector2 lookPosition = GetLookPosition();
        // Get movement input from the inputManager
        Vector3 movementVector = new Vector3(inputManager.horizontalMoveAxis, inputManager.verticalMoveAxis, 0);
        // Move the player
        MovePlayer(movementVector);
        LookAtPoint(lookPosition);
    }

    private void SignalAnimator()
    {
        // Handle Animation
        if (animator != null)
        {

        }
    }

    public Vector2 GetLookPosition()
    {
        Vector2 result = transform.up;
        if (aimMode != AimModes.AimForwards)
        {
            result = new Vector2(inputManager.horizontalLookAxis, inputManager.verticalLookAxis);
        }
        else
        {
            result = transform.up;
        }
        return result;
    }


    /// <param name="movement">The direction to move the player</param>
    private void MovePlayer(Vector3 movement)
    {
        // Set the player's posiiton accordingly

        // Move according to astroids setting
        if (movementMode == MovementModes.Astroids)
        {

            // If no rigidbody is assigned, assign one
            if (myRigidbody == null)
            {
                myRigidbody = GetComponent<Rigidbody2D>();
            }

            // Move the player using physics
            Vector2 force = transform.up * movement.y * Time.deltaTime * moveSpeed;
            Debug.Log(force);
            myRigidbody.AddForce(force);

            // Rotate the player around the z axis
            Vector3 newRotationEulars = transform.rotation.eulerAngles;
            float zAxisRotation = transform.rotation.eulerAngles.z;
            float newZAxisRotation = zAxisRotation - rotationSpeed * movement.x * Time.deltaTime;
            newRotationEulars = new Vector3(newRotationEulars.x, newRotationEulars.y, newZAxisRotation);
            transform.rotation = Quaternion.Euler(newRotationEulars);

        }
        // Move according to the other settings
        else
        {
            // Don't move in the x if the settings stop us from doing so
            if (lockXCoordinate)
            {
                movement.x = 0;
            }
            // Don't move in the y if the settings stop us from doing so
            if (lockYCoordinate)
            {
                movement.y = 0;
            }
            // Move the player's transform
            transform.position = transform.position + (movement * Time.deltaTime * moveSpeed);
        }
    }

    /// <param name="point">The screen space position to look at</param>
    private void LookAtPoint(Vector3 point)
    {
        if (Time.timeScale > 0)
        {
            // Rotate the player to look at the mouse.
            Vector2 lookDirection = Camera.main.ScreenToWorldPoint(point) - transform.position;

            if (canAimWithMouse)
            {
                transform.up = lookDirection;
            }
            else
            {
                if (myRigidbody != null)
                {
                    myRigidbody.freezeRotation = true;
                }
            }
        }
    }
}
