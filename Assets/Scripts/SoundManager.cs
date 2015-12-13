﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource effectSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}
	
    public void PlaySingle(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        effectSource.pitch = randomPitch;
        effectSource.clip = clips[randomIndex];
        effectSource.Play();
    }


}
