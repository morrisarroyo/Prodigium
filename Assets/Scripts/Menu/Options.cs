using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour {


    public GameObject menuUI;
    public GameObject optionsUI;
    public GameObject toggleUI;

    public AudioMixer audioMixer;

    public bool volumeOn;
    public float checkVolume;

    void Start () {
        audioMixer.GetFloat("volume", out checkVolume);
        if (checkVolume == 0) {
            toggleUI.GetComponent<Toggle>().isOn = true;
            volumeOn = true;
        } else {
            toggleUI.GetComponent<Toggle>().isOn = false;
            volumeOn = false;
        }
    }

    public void ToggleVolume () {
        if (volumeOn) {
            audioMixer.SetFloat("volume", -80);

            volumeOn = false;
        } else {
            audioMixer.SetFloat("volume", 0);

            volumeOn = true;
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