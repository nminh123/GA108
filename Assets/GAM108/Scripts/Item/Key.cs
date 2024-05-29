using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    public Animator anim;
    public GameObject _LoadSecne;
    private void Awake()
    {
        _LoadSecne.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tag.Playertag)
        {
            StartCoroutine(LoadSecne());
            
        }
    }
    IEnumerator LoadSecne()
    {
        anim.SetTrigger("end");
        yield return new WaitForSecondsRealtime(1);
        anim.SetTrigger("start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
