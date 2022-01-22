using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSound : MonoBehaviour {

    public AudioSource[] source;
    private Slider slider;
    void Start ()
    {
        slider = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSource()
    {
        foreach(AudioSource audio in source)
        {
            audio.volume = slider.value;
        }
    }
}
