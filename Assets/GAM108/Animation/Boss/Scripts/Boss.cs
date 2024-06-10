using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

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
    public bool CanAttack = true;

    #region Health Boss
    //thanh mau
    [SerializeField]
    private Slider healthSlider;

    private int _healthMax;

    [SerializeField]
    private GameObject hpCanvas;
    #endregion

    private void Start()
    {
        _healthMax = 1000;
        healthSlider.maxValue = _healthMax;
        hpCanvas.SetActive(false);
    }
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
            return;
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void LookPlayer()
    {
        Vector2 flip = transform.lossyScale;
        if(transform.position.x > player.position.x && !isRight)
        {
            transform.localScale = flip;
            transform.Rotate(0, 180, 0);
            healthSlider.transform.localScale = new Vector3(-1, 1, 1);
            isRight = true;
        }
        else if(transform.position.x < player.position.x && isRight)
        {
            transform.localScale = flip;
            transform.Rotate(0, 180, 0);
            healthSlider.transform.localScale = new Vector3(1, 1, 1);
            isRight = false;
        }
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attack.x;
        pos += transform.up * attack.y;
        Collider2D col = Physics2D.OverlapCircle(pos, attackRange, PlayerMask);
        //if (col != null)
        //{
        //    col.GetComponent<Player>().TakeDamege(2);
        //    Debug.Log("chet ne x2");
        //}
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox") || collision.gameObject.CompareTag("Skill") && CanAttack)
        {

            CanAttack = false;
            hpCanvas.SetActive(true);
            _healthMax -= 70;
            healthSlider.value = _healthMax;
            if (_healthMax <= 0)
            {
                Destroy(gameObject, 1.1f);
            }
            StartCoroutine(resetHit());
            
        }
    }
    IEnumerator resetHit()
    {

        yield return new WaitForSecondsRealtime(1);
        CanAttack = true;
    }

}
