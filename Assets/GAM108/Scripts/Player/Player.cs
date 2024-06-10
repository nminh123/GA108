using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [Header("Physic Player")]
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce; // jump force
    [SerializeField] private bool isGround;
    [SerializeField] private bool islive = true;
    [Header("Hp")]
    [SerializeField] private int Hp;
    [SerializeField] private float _time;
    [SerializeField] private float HPTIME;
    [SerializeField] bool isHp;
    [Header("Count Down")]
    [SerializeField] private float AatackTime;
    [SerializeField] private float CountDownAatack;
    [Header("Skill")]
    [SerializeField] GameObject skill;
    [SerializeField] Transform PointShot;
    [SerializeField] private float skillTime;
    [SerializeField] private float CountDownSkill;
    private int DoubleJump;
    private float moveDirection;
    private bool facingRight = true;
    private bool isMoving = true;
    private bool isGround1;
    private const float OffSetPosition = 10.0f;

    #region Hp Player
    //thanh mau
    [SerializeField]
    private Slider healthSlider;

    private int _healthMax;
    #endregion
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _healthMax = 100;
        healthSlider.maxValue = _healthMax;
    }

    public void Update()
    {
        inputJump();
        isDeah();
        AnimationController();
        aatack();
        Skill();
        time();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!islive)
            return;
        moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();

    }
    #region camdongvao
    private void AnimationController()
    {

        isMoving = rb.velocity.x != 0;

        anim.SetBool("isRuning", isMoving);
        anim.SetFloat("Jump/Fall", rb.velocity.y);
        anim.SetBool("IsGournded", isGround);
        anim.SetBool("IsGournded", isGround1);
        anim.SetBool("isDeah", islive);
    }
    #endregion
    private void Skill()
    {
        skillTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.I) && skillTime <= 0)
        {

            StartCoroutine(skill1());
            skillTime = CountDownSkill;

        }
    }
    IEnumerator skill1()
    {
        anim.SetTrigger("aatacking");
        yield return new WaitForSeconds(.3f);
        Instantiate(skill, PointShot.position, transform.rotation);
    }
    private void aatack()
    {
        AatackTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J) && AatackTime <= 0)
        {
            anim.SetTrigger("aatacking");
            AatackTime = CountDownAatack;
        }
    }
    private void time()
    {
        _time -= Time.deltaTime;
        if (isHp == false && _time <= 0)
        {
            _time = HPTIME;
            isHp = true;
        }

    }
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void Jump()
    {
        if (isGround && DoubleJump >= 0)
        {
            DoubleJump--;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            isGround = true;
            isGround1 = false;

        }
        if (DoubleJump == 0)
            isGround = false;
    }

    //public void TakeDamege(int damege)
    //{
    //    Hp -= damege;
    //    if (Hp <= 0)
    //    {
    //        
    //    }
    //}
    private void isDeah()
    {
        if (Input.GetKeyDown(KeyCode.U) && islive)
            islive = false;
        else if (Input.GetKeyDown(KeyCode.U) && !islive)
            islive = true;

    }
    private void inputJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.GroundTag)
        {
            isGround = true;
            isGround1 = true;
            DoubleJump = 2;
        }

        if (collision.gameObject.tag == "HBenemy")
        {
            if (isHp == true)
            {
                //Hp = Hp - 1;
                isHp = false;
               
            }

            /*if(collision.gameObject.tag == Tag.Spike)
            {
                //if(isHp == true)

                    Hp = Hp -1;
                    //isHp = false;


                if (Hp == 0)
                   //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                   SceneManager.LoadScene("GameOver");
                Debug.Log("Chet ne");
            }*/
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            _healthMax -= 6;
            healthSlider.value = _healthMax;
            if (_healthMax <= 0)
            {
                Destroy(collision.gameObject, 1.1f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == Tag.EnemyTag)
            {
                if(isHp == true)
                {
                    Hp = Hp -1;
                    isHp = false;
                }

            if (Hp == 0)
               //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               SceneManager.LoadScene("GameOver");
            Debug.Log("chet ne");

            //}

            if (collision.gameObject.tag == Tag.OffSetTag)
            {
                Debug.Log("chet ne");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
                SceneManager.LoadScene("GameOver");
            }
        }
        /*void offSet()
        {
            float offsetThreshold = 0.1f; // Define a small threshold for position comparison
            Vector2 offset = new Vector2(transform.position.x, OffSetPosition);

            // Check if the Y position is within the threshold of OffSetPosition
            if (Mathf.Abs(this.transform.position.y - OffSetPosition) < offsetThreshold)
            {
                SceneManager.LoadScene(0); // Reload the scene
            }
        }*/

    }
}
