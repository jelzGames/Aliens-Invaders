using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    private void Awake()
    {
        // Load one time and  play between scenes using global static variable 
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnLevelWasLoaded(int level)
    {
        music.Stop();

        if (level == 0)
        {
            music.clip = startClip;

        }
        if (level == 1)
        {
            music.clip = gameClip;

        }
        if (level == 2)
        {
            music.clip = endClip;

        }
        music.loop = true;
        music.Play();
    }
}
