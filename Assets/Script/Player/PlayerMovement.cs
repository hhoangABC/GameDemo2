using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float runSpeed = 9f;
    [SerializeField] protected float jumpSpeed = 30f;

    protected Vector2 moveInput;
    protected Rigidbody2D rb;
    protected Animator anima;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    protected virtual void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    protected virtual void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    protected virtual void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool horizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anima.SetBool("isRunning", horizontalSpeed);
    }

    protected virtual void FlipSprite()
    {
        bool horizontalSpeed = Mathf.Abs (rb.velocity.x) > Mathf.Epsilon;
        if (horizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
