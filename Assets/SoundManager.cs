using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] sounds;
    private AudioSource backgroundMusic;
    private AudioSource explosion;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        backgroundMusic = sounds[0];
        explosion = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }
    public void PlayExplosion()
    {
        explosion.Play();
    }
}
