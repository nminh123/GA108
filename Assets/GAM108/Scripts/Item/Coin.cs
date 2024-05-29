using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Point;
    public Score score;
    
    private bool isCoined = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tag.Playertag && !isCoined)
        {
            isCoined = true;
            score.addScore(Point);
            Destroy(this.gameObject);
        }
    }

}
