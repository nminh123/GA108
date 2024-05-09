using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveDirection;
    [SerializeField] private int countJump = 0;
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce; // jump force
    [SerializeField] private bool isGround;
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
        
        if(Input.GetKey(KeyCode.Space))
        {
            if(isGround)
            {
                Jump();
                isGround = false;
                DoubleJump = false;
            }
            else if (DoubleJump)
            {
                Jump();
                DoubleJump = false; 
            }
        }
        
        if(isGround && !Input.GetKey(KeyCode.Space))
        {
            DoubleJump = false;
        }
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

        amin.SetBool("isRuning", isMoving);

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
