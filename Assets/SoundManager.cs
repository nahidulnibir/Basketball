using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static int soundOn = 1;
    AudioSource audioSource;

    public enum sounds
    {
        ballThrow,
        drop,
        score,
        burst,
        swish,
    }

    Dictionary<sounds, AudioClip> sound = new Dictionary<sounds, AudioClip>();

    public void PlaySound(sounds soundName)
    {
        if (soundOn == 1)
        {
            audioSource.PlayOneShot(sound[soundName]);
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
      soundOn = PlayerPrefs.GetInt("soundStatus",1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SoundToggle()
    {
        if (soundOn == 1)
            soundOn = 0;
        else
            soundOn = 1;

        PlayerPrefs.SetInt("soundStatus", soundOn);
    }
}

