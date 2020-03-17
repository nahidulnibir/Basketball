using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instace;
    public static SoundManager Instance { get { return _instace; } }

   

    static int soundOn = 1;
    AudioSource audioSource;

    public enum sounds
    {
        ballThrow,
        drop,
        score,
        burst,
        swish,
        miss,
        gameover,
        restart,
    }

    public Dictionary<sounds, AudioClip> sound = new Dictionary<sounds, AudioClip>();

    [Serializable]
    public struct soundStruct
    {
        public sounds soundName;
        public AudioClip ac;
    }
    public soundStruct[] soundsCollection;

    public void PlaySound(sounds soundName)
    {
        if (soundOn == 1)
        {
            audioSource.PlayOneShot(sound[soundName]);
        }
    }

    private void Awake()
    {
        if(_instace != null && _instace != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instace = this;
        }

        audioSource = GetComponent<AudioSource>();

        foreach(soundStruct sounds in soundsCollection)
        {
            sound.Add(sounds.soundName, sounds.ac);
        }

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

