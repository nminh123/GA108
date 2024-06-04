using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{

    //thanh mau
    [SerializeField]
    private Slider healthSlider;

    private int _healthMax;
    // Start is called before the first frame update
    void Start()
    {
        _healthMax = 100;
        healthSlider.maxValue = _healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skill"))
        {
            _healthMax -= 5;
            healthSlider.value = _healthMax;
            if(_healthMax <= 0)
            {   
                Destroy(collision.gameObject, 0.2f);
                Destroy(gameObject, 1.1f);
            }
        }
    }
}
