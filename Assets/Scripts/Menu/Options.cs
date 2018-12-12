using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour {


    public GameObject menuUI;
    public GameObject optionsUI;

    public AudioMixer audioMixer;

    public bool volumeOn;

    void Start () {
        volumeOn = true;
    }

    public void ToggleVolume () {
        if (volumeOn) {
            audioMixer.SetFloat("volume", -80);

            volumeOn = !volumeOn;
        } else {
            audioMixer.SetFloat("volume", 0);

            volumeOn = !volumeOn;
        }
    }

    public void DisplayOptions () {
        menuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void HideOptions () {
        menuUI.SetActive(true);
        optionsUI.SetActive(false);
    }
}