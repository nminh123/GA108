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
    [Header("Support")]
    [SerializeField] GameObject sp;
    [SerializeField] private Transform pointSp;
    private int DoubleJump;
    private float moveDirection;
    private bool facingRight = true;
    private bool isMoving = true;
    private bool isGround1;
    private const float OffSetPosition = 10.0f;
    private Item _item;
    private bool hitDame = true;

    [SerializeField]
    private GameObject canvasSpeed;

    #region Hp Player
    //thanh mau
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider CownDownSkill;
    [SerializeField]
    private Slider CownDownAt;

    private int _healthMax;
    #endregion

    #region Audio
    private AudioSource myAudioSource; //trinh phat am thanh

    [SerializeField]
    private AudioClip _myAudioSkill; //file am thanh

    [SerializeField]
    private AudioClip _myAudioAttack; //file am thanh

    [SerializeField]
    private AudioClip _myAudioisHealth; //file am thanh
    [SerializeField]
    private AudioClip _myAudioisBoss; //file am thanh

    [SerializeField]
    private AudioClip _myAudioItemSpeed; //file am thanh

    [SerializeField]
    private AudioClip _myAudioJump;
    #endregion
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //_healthMax = 100;
        Hp = 100;
        healthSlider.maxValue = Hp;
        CownDownSkill.maxValue = skillTime;
        CownDownAt.maxValue = AatackTime;

        myAudioSource = GetComponent<AudioSource>();

        canvasSpeed.SetActive(false);
    }

    public void Update()
    {
        inputJump();
        isDeah();
        AnimationController();
        aatack();
        Skill();
        //time();
        support();
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
        CownDownSkill.value = this.skillTime;
        if (Input.GetKeyDown(KeyCode.I) && skillTime <= 0)
        {

            StartCoroutine(skill1());         
            skillTime = CountDownSkill;
        }
        if(skillTime <= 0)
        {
            skillTime = 0;
        }
    }
    IEnumerator skill1()
    {
        anim.SetTrigger("aatacking");
        yield return new WaitForSeconds(.3f);
        myAudioSource.PlayOneShot(_myAudioSkill);
        Instantiate(skill, PointShot.position, transform.rotation);
        
        CownDownSkill.value = this.skillTime;
    }
    private void support()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(sp, pointSp.position, transform.rotation);
        }
    }
    private void aatack()
    {
        AatackTime -= Time.deltaTime;
        CownDownAt.value = this.AatackTime;
        if (Input.GetKeyDown(KeyCode.J) && AatackTime <= 0)
        {
            anim.SetTrigger("aatacking");
            AatackTime = CountDownAatack;
            myAudioSource.PlayOneShot(_myAudioAttack);
        }
    }
    private void time(float _Time)
    {


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
            myAudioSource.PlayOneShot(_myAudioJump);
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
        _time -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.U) && islive)
        {
            islive = false;
            if (isHp == false && _time <= 0)
            {

                _time = HPTIME;
                if (_time > 2)
                {
                    Hp++;
                }
                _time = HPTIME;
                isHp = true;
            }

        }
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
            if (hitDame)
            {
                Hp = Hp - 4;
                myAudioSource.PlayOneShot(_myAudioisHealth);
                healthSlider.value = this.Hp;
                hitDame = false;
                if(Hp <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                hitDame = true;
                if (Hp <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
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
        if (collision.gameObject.CompareTag("HBboss"))
        {
            Hp -= 6;
            myAudioSource.PlayOneShot(_myAudioisBoss);
            healthSlider.value = Hp;
            if (Hp <= 0)
            {
                Destroy(collision.gameObject, 1.1f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (collision.gameObject.tag == "item")
        {
            JumpForce += 2;
            myAudioSource.PlayOneShot(_myAudioItemSpeed);
            Destroy(collision.gameObject);
            StartCoroutine(timeItem());
        }
        
        if (collision.gameObject.tag == Tag.OffSetTag)
        {
            Debug.Log("chet ne");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            SceneManager.LoadScene("GameOver");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tag.EnemyTag)
        {

            Hp = Hp - 1;


            if (Hp == 0)
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene("GameOver");
            Debug.Log("chet ne");

            //}

            if (collision.gameObject.tag == Tag.OffSetTag)
            {
                Debug.Log("chet ne");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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

    IEnumerator timeItem()
    {
        canvasSpeed.SetActive(true);
        yield return new WaitForSeconds(3.4f);
        canvasSpeed.SetActive(false);
    }
}
