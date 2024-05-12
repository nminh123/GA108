using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float climbingSpeed = 5.0f;
    private bool isClimbing = false;
    private bool isLadder;
    private float vertical;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if(isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    void FixedUpdate()
    {
        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbingSpeed);
        }
        else
        {
            rb.gravityScale = 4f;
        }

        anim.SetFloat("Jump/Fall", rb.velocity.y);
        anim.SetBool("Climb", isLadder);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
