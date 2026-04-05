using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions inputs;
    private CharacterController controller;



    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float defaultMoveSpeed;

    private float verticalVelocity = 0f;
    private bool isDashing = false;
    private float dashTimer = 0f;

    [SerializeField] private Vector2 moveInput;




    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();
        defaultMoveSpeed = moveSpeed;
    }
    private void OnEnable()
    {
        inputs.Enable();

        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;


        inputs.Player.Jump.performed += OnJump;

        inputs.Player.DashSprint.performed += ctx => {
            OnDash(ctx);
            moveSpeed = defaultMoveSpeed * 2f;
        };

        inputs.Player.DashSprint.canceled += ctx => {
            moveSpeed = defaultMoveSpeed;
        };
    }
    void Start()
    {

    }
    void Update()
    {

        OnMove();
        //OnSimpleMove();
    }

    public void OnMove()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);
        Vector3 moveDir = transform.forward * moveSpeed * moveInput.y;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;


        moveDir.y = verticalVelocity;

        if (isDashing)
        {
            //->convertir el dash a un barrido por el piso! dash con gravedad integrada omaegoto!
            moveDir = transform.forward * dashForce * (dashTimer / dashDuration);

            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
                isDashing = false;
        }




        controller.Move(moveDir * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!controller.isGrounded) return;

        verticalVelocity = jumpForce;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        isDashing = true;
        dashTimer = dashDuration;
        
    }

}