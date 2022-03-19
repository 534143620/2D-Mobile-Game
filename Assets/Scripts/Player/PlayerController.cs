using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator anim;

    //地面检测 
    [Header("Check Param")]
    public Transform groundCheck;
    public float checkRange;
    public LayerMask checkLayerMask;

    [Header("States Check")]
    public bool isGround;
    public bool isJump;
    public bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        PhysicsCheck();
        Movement();
        Jump();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    void CheckInput()
    {
        bool isJump = Input.GetButtonDown("Jump");
        if (isJump && isGround)
        {
            canJump = true;
        }
    }
    void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRange, checkLayerMask);
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
        else
        {
            isJump = true;
            rb.gravityScale = 4;
            canJump = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRange);
    }


}
