using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    [NonSerialized]
    public Boolean bossMusic;
    [NonSerialized]
    public Boolean combatMusic;
    [NonSerialized]
    public Boolean backgroundMusic;
    [NonSerialized]
    public Boolean menuMusic;

    // Use this for initialization
    void Awake () {

        bossMusic = false;
        combatMusic = false;
        backgroundMusic = false;
        menuMusic = true;

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

		foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start ()
    {
        Play("MenuMusic");
    }
	
	// Update is called once per frame
	void Update () {

        

        if(bossMusic) {
            if (!isPlaying("BossMusic")) {
                Stop("CombatMusic");
                Stop("BackgroundMusic");
                Stop("MenuMusic");
                Play("BossMusic");
            }

        } else if(combatMusic) {
            if (!isPlaying("CombatMusic")) {
                Stop("BossMusic");
                Stop("BackgroundMusic");
                Stop("MenuMusic");
                Play("CombatMusic");
            }

        } else if(backgroundMusic) {
            if (!isPlaying("BackgroundMusic")) {
                Stop("BossMusic");
                Stop("CombatMusic");
                Stop("MenuMusic");
                Play("BackgroundMusic");
            }

        } else if (menuMusic) {
            if (!isPlaying("MenuMusic")) {
                Stop("BossMusic");
                Stop("CombatMusic");
                Stop("BackgroundMusic");
                Play("MenuMusic");
            }
        }

    }

    public void Play (string name) {
       
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public bool isPlaying(string name) {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }


        bool playing = s.source.isPlaying;

        return playing;
    }
}
