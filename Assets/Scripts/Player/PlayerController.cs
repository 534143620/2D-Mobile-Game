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

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;

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
            showFx(jumpFX, new Vector3(0, -0.45f, 0));
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

    private void showFx(GameObject FxObject,Vector3 vector3) //显示跳跃和落地FX,加上相对位置的偏移
    {
        FxObject.SetActive(true);
        FxObject.transform.position = transform.position + vector3;
    }

    public void LandFX() //在落地animation的第一针做事件的显示，肯定是非常不好的垃圾代码，之后想办法优化
    {
        showFx(landFX, new Vector3(0, -0.75f, 0));
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRange);
    }


}
