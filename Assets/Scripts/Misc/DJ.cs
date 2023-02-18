using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class DJ : MonoBehaviour
{
    private AudioSource aS;
    public List<AudioClip> Mixtape;
    public AudioClip menuMusic;
    public GameManager gm;
    void Start()
    {
        aS = GetComponent<AudioSource>();
        PlayMenuMusic();
    }
    public void PlayGameMusic()
    {
        aS.clip = Mixtape[Random.Range(0, Mixtape.Count)];
        aS.Play();
    }
    public void PlayMenuMusic()
    {
        aS.clip = menuMusic;
        aS.Play();
    }
    public void StopPlayingMusic()
    {
        aS.Stop();
    }
    void FixedUpdate()
    {
        if (!aS.isPlaying && gm.currentState == GameManager.gameState.game)
        {
            PlayGameMusic();
        }
    }
}
