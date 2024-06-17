using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public bool isPause = true;
    public GameObject panel;
    private void Update()
    {
        inputPasue();
    }
    void inputPasue()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            isPause = false;

        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            Time .timeScale = 1;
            panel.SetActive(false);
            isPause = true;
        }
    }
}
