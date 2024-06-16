using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Point;
    public Score score;
    
    private bool isCoined = false;

    #region Audio
    private AudioSource myAudioSource; //trinh phat am thanh

    [SerializeField]
    private AudioClip _myAudio; //file am thanh
    #endregion

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tag.Playertag && !isCoined)
        {
            isCoined = true;
            myAudioSource.PlayOneShot(_myAudio);
            score.addScore(Point);
            
            Destroy(this.gameObject, 0.2f);
        }
    }

}
