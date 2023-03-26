using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerMovements : MonoBehaviour
{
    private PlayerControlsAsset playerControls;
    private InputAction movements;

    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    Chronometer chrono;

    private bool isCrouching;
    public float colliderHeight;

    [Header("Movement Parameters")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    public bool IsGrounded;

    private void Awake()
    {
        playerControls = new PlayerControlsAsset();
        rb = GetComponentInParent<Rigidbody>();
        capsuleCollider = GetComponentInParent<CapsuleCollider>();
        chrono = GetComponentInParent<Chronometer>();
        colliderHeight = capsuleCollider.height;
    }

    private void OnEnable()
    {
        movements = playerControls.Player1.Move;
        movements.Enable();

        playerControls.Player1.Jump.performed += DoJump;
        playerControls.Player1.Jump.started += Uncrouch;

        playerControls.Player1.Jump.Enable();

        playerControls.Player1.Crouch.started += DoCrouch;
        playerControls.Player1.Crouch.canceled += Uncrouch;

        playerControls.Player1.Crouch.Enable();
    }



    private void OnDisable()
    {
        movements.Disable();
        playerControls.Player1.Jump.Disable();
    }

    private void FixedUpdate()
    {
        //Debug.Log("Movement Values : " + movements.ReadValue<float>());
        if(CameraMovement.GetDirection() == DirectionState.NORTH)
        {
        rb.velocity = new UnityEngine.Vector3(movements.ReadValue<float>() * Time.deltaTime * speed, rb.velocity.y - 0.1f);
        }
        if(CameraMovement.GetDirection() == DirectionState.WEST)
        {
        rb.velocity = new UnityEngine.Vector3(rb.velocity.x, rb.velocity.y - 0.1f, movements.ReadValue<float>() * Time.deltaTime * speed);
        }
        if(CameraMovement.GetDirection() == DirectionState.SOUTH)
        {
        rb.velocity = new UnityEngine.Vector3(-movements.ReadValue<float>() * Time.deltaTime * speed, rb.velocity.y - 0.1f);
        }
        if(CameraMovement.GetDirection() == DirectionState.EAST)
        {
            rb.velocity = new UnityEngine.Vector3(rb.velocity.x, rb.velocity.y - 0.1f, -movements.ReadValue<float>() * Time.deltaTime * speed);
        }

    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (!IsGrounded)
        {
            Debug.Log("Can't jump");
        }
        else
        {
            Debug.Log("Jumped !");
            rb.velocity = new UnityEngine.Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
    private void DoCrouch(InputAction.CallbackContext obj)
    {
        Debug.Log("Crouching");
        if (!IsGrounded && !isCrouching)
        {
            Debug.Log("Can't crouch !");
        }
        else
        {
            isCrouching = true;
            capsuleCollider.height = colliderHeight / 2.25f;
        }
    }

    private void Uncrouch(InputAction.CallbackContext obj)
    {
        Debug.Log("Uncrouching");
        if (isCrouching)
        {
            capsuleCollider.height = colliderHeight;
            isCrouching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") IsGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground") IsGrounded = false;
    }
}
