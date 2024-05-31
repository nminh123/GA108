using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Move")]
    public Transform player;
    public bool isRight = false;
    [Header("Attack")]
    public Vector3 attack;
    public float attackRange;
    public LayerMask PlayerMask;
    [Header("CountDown")]
    public float StartTime;
    public float ResetTime;
    [Header("HP")]
    public int Heal;

    private void Update()
    {
        StartTime -= Time.deltaTime;
        
    }
    public void loadTime(bool _Time)
    {
        if (StartTime <= 0 && _Time == true)
        {
            StartTime = 0;
            StartTime = ResetTime;
        }

            
            
    }
    public void takeDame(int damege)
    {
        Heal -= damege;
        if(Heal <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    public void LookPlayer()
    {
        Vector2 flip = transform.lossyScale;
        if(transform.position.x > player.position.x && !isRight)
        {
            transform.localScale = flip;
            transform.Rotate(0, 180, 0);
            isRight = true;
        }
        else if(transform.position.x < player.position.x && isRight)
        {
            transform.localScale = flip;
            transform.Rotate(0, 180, 0);
            isRight = false;
        }
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attack.x;
        pos += transform.up * attack.y;
        Collider2D col = Physics2D.OverlapCircle(pos, attackRange, PlayerMask);
        if (col != null)
        {
            col.GetComponent<Player>().TakeDamege(2);
            Debug.Log("chet ne x2");
        }
            
    }

}
