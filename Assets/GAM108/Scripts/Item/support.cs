using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class support : MonoBehaviour
{
    public Boss target;
    public Rigidbody2D rb;
    public float speed;
    public float attackRange;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Boss").GetComponent<Boss>();
    }
    private void Update()
    {
        Vector2 targer = new Vector2(target.transform.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, targer, speed * Time.fixedDeltaTime);
        float dis = Vector2.Distance(target.transform.position, this.transform.position);
        if (dis > attackRange)
            rb.MovePosition(newPos);
        if (dis < attackRange)
        {
            rb.velocity = new Vector2(0, 0);

            return;
        }
    }
}
