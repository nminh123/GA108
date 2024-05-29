using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Point;
    public Score score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tag.Playertag)
        {
            score.addScore(Point);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tag.Playertag)
        {
            score.addScore(Point);
            Destroy(this.gameObject);
        }
    }
}
