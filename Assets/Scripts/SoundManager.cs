using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] sounds;
    private AudioSource backgroundMusic;
    private AudioSource explosion;          // rocket explosion
    private AudioSource mainMenuMusic;
    private AudioSource timeTick;
    private AudioSource spaceEngine;
    private AudioSource laserSound;
    private AudioSource explosionObst;      // obstacle explosion
    private AudioSource winGameSound;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        backgroundMusic = sounds[0];
        explosion = sounds[1];
        mainMenuMusic = sounds[2];
        timeTick = sounds[3];
        spaceEngine = sounds[4];
        laserSound = sounds[5];
        explosionObst = sounds[6];
        winGameSound = sounds[7];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }
    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    public void PlayExplosion()
    {
        explosion.Play();
    }
    public void PlayMainMenuMusic()
    {
        mainMenuMusic.Play();
    }
    public void StopMainMenuMusic()
    {
        mainMenuMusic.Stop();
    }
    public void PlayTimeTick()
    {
        timeTick.Play();
    }
    public void PlaySpaceEngine()
    {
        spaceEngine.Play();
    }
    public void StopSpaceEngine()
    {
        spaceEngine.Stop();
    }
    public void PlayLaserSound()
    {
        laserSound.Play();
    }
    public void PlayExplosionObstSound()
    {
        explosionObst.Play();
    }
    public void PlayWinGameSound()
    {
        winGameSound.Play();
    }
}
