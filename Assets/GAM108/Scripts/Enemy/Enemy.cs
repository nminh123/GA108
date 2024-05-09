using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int speed;

    private bool 

    private Animator eAnimator;
    // Start is called before the first frame update
    void Start()
    {
        eAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();        
    }

    private void MoveEnemy()
    {
        var direction = Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
