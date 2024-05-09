using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int countJump = 0;
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce; // jump force
    [SerializeField] private bool isGround;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
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

        AnimationController();
    }
        private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
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

    }
    #endregion
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        //DoubleJump = !DoubleJump;
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
