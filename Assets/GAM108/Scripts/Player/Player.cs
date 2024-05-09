using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [Header("Physic Player")]
    [SerializeField] private int countJump = 0;
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce; // jump force
    [SerializeField] private bool isGround;
    [SerializeField] private bool islive = true;
    [Header("Count Down")]
    [SerializeField] private float AatackTime;
    [SerializeField] private float CountDownAatack;
    //private bool DoubleJump;
    private float moveDirection;
    private bool facingRight = true;
    private bool isMoving = true;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    { 
        inputJump();
        isDeah();
        AnimationController();
        aatack();
    }
        private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!islive)
            return;
        moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();

    }
    #region camdongvao
    private void AnimationController()
    {

        isMoving = rb.velocity.x != 0;

        anim.SetBool("isRuning", isMoving);
        anim.SetFloat("Jump/Fall", rb.velocity.y);
        anim.SetBool("IsGournded", isGround);
        anim.SetBool("isDeah", islive);
    }
    #endregion
    private void aatack()
    {
        AatackTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J) && AatackTime < 0)
        {
                anim.SetTrigger("aatacking");
                AatackTime = CountDownAatack;                
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        countJump++;
        //DoubleJump = !DoubleJump;
    }
    private void isDeah()
    {
        if (Input.GetKeyDown(KeyCode.U) && islive)
            islive = false;
        else if (Input.GetKeyDown(KeyCode.U) && !islive)
            islive = true;

    }
    private void inputJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGround)
            {
                Jump();
                isGround = false;
                //countJump++;
                //DoubleJump = false;
            }
            /*else if (DoubleJump)
            {
                Jump();
                //DoubleJump = false; 
            }*/
        }

        /*if(isGround && !Input.GetKey(KeyCode.Space))
        {
            DoubleJump = false;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Tag.GroundTag)
        {
            isGround = true;
        }
    }
}
