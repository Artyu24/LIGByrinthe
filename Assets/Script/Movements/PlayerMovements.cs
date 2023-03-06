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


    [Header("Movement Parameters")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    public bool IsGrounded;

    private void Awake()
    {
        playerControls = new PlayerControlsAsset();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnEnable()
    {
        movements = playerControls.Player1.Move;
        movements.Enable();

        playerControls.Player1.Jump.performed += DoJump;
        playerControls.Player1.Jump.Enable();
    }


    private void OnDisable()
    {
        movements.Disable();
        playerControls.Player1.Jump.Disable();
    }

    private void FixedUpdate()
    {
        Debug.Log("Movement Values : " + movements.ReadValue<float>());
        rb.velocity = new UnityEngine.Vector2(movements.ReadValue<float>() * Time.deltaTime * speed, rb.velocity.y - 0.1f);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") IsGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground") IsGrounded = false;
    }
}
