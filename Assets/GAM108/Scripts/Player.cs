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
    private bool isFacingRight = true;
    [SerializeField] private bool isGround;
    //private bool DoubleJump;
    private Rigidbody2D rigid;
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Move();
        if(Input.GetKey(KeyCode.Space))
        {
            if(isGround)
            {
                Jump();
                isGround = false;
                countJump++;
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
    
    public void Move()
    {
        moveDirection = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(moveDirection * speed, rigid.velocity.y);
        Flip();

    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, JumpForce);
        //DoubleJump = !DoubleJump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
}
