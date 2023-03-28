using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovements : MonoBehaviour
{
    private PlayerControlsAsset playerControls;
    private InputAction movements;

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
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

        #region Anim

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x + rb.velocity.z));
            anim.SetFloat("Jump", Mathf.Abs(rb.velocity.y));
            anim.SetFloat("Falling", rb.velocity.y);
            if (isCrouching)
                anim.SetFloat("Crouch", 10);
            else
                anim.SetFloat("Crouch", 0);
        }

        if (sr != null)
        {
            if (CameraMovement.GetDirection() == DirectionState.NORTH || CameraMovement.GetDirection() == DirectionState.WEST)
            {
                if (rb.velocity.x + rb.velocity.z > 0)
                    sr.flipX = false;
                else if (rb.velocity.x + rb.velocity.z < 0)
                    sr.flipX = true;
            }
            else
            {
                if (rb.velocity.x + rb.velocity.z > 0)
                    sr.flipX = true;
                else if (rb.velocity.x + rb.velocity.z < 0)
                    sr.flipX = false;
            }
        }

        #endregion
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
            capsuleCollider.center = new Vector3(0, -0.45f, 0);
        }
    }

    private void Uncrouch(InputAction.CallbackContext obj)
    {
        Debug.Log("Uncrouching");
        if (isCrouching)
        {
            capsuleCollider.height = colliderHeight;
            capsuleCollider.center = Vector3.zero;
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
