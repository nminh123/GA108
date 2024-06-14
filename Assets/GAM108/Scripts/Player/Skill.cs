using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tag.EnemyTag || collision.tag == Tag.GroundTag || collision.tag == "Boss")
        {
            anim.SetTrigger("Shotting");
            rb.velocity = new Vector2(0, rb.velocity.y);
            
            Destroy(this.gameObject,.5f);
        }
    }
}
