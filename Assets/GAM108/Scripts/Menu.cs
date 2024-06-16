using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource myAdio;
    [SerializeField]
    private AudioClip myClip;

    private void Start()
    {
        myAdio = GetComponent<AudioSource>();
    }
    public void LoadGame()
    {
        myAdio.PlayOneShot(myClip);
        StartCoroutine(timeLoadScene());
        
    }
    public void LoadExit()
    {
        myAdio.PlayOneShot(myClip);
        Application.Quit();
    }
    public void LoadMenu()
    {
        myAdio.PlayOneShot(myClip);
        StartCoroutine(timeLoadScene());
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator timeLoadScene()
    {
        yield return new WaitForSeconds(0.18f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
