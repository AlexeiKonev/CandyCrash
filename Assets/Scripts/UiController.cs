using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    private  float Timer = 60f;
    public Text textTimer;
    public GameObject PausePanel;
    public AudioSource[] source;

	void Start () {
		
	}
	void FixedUpdate () {
        textTimer.text = Timer.ToString("F2");
        Timer -= Time.deltaTime;
    }
    public void AddTime()
    {
        Timer++;
    }

    public void MinusTime()
    {
        Timer--;
        if (Timer < 0) Application.Quit();
    }

    public void SetPause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ResetPause()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }



}
