using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region toc do quai
    //toc do cua quai
    [SerializeField]
    private float speed;
    #endregion
    #region toa do cho quai di chuyen trai phai
    //toa do cho quai di chuyen trai phai
    [SerializeField]
    private float leftEnemy = 0f;
    [SerializeField]
    private float rightEnemy = 0f;
    #endregion

    //check xem quai di chuyen trai hay phai
    private bool isMovingRight = true;

    private SpriteRenderer eScale;

    // Start is called before the first frame update
    void Start()
    {
        eScale = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        //lay vi tri hien tai cua enemy
        var currentPosition = transform.localPosition;
        if (currentPosition.x > rightEnemy)
        {
            //di chuyen sang trai
            isMovingRight = false;
            eScale.flipX = true;
        }
        else if(currentPosition.x < leftEnemy)
        {   //di chuyen sang phai
            isMovingRight = true;
            eScale.flipX = false;
        }
        var direction = isMovingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(eDieAnimation());
            Destroy(gameObject);
        }
    }*/

    //test 
    //private IEnumerator eDieAnimation()
    //{
    //    eAnimator.SetBool("isDie", true);
    //    yield return new WaitForSeconds(2f);
    //    Destroy(gameObject);


    //}

}
