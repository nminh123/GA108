using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isRight = false;
    
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
}
