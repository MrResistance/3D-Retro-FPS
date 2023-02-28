using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class DJ : MonoBehaviour
{
    private AudioSource aS;
    public List<AudioClip> Mixtape;
    public AudioClip menuMusic, winSound, lossSound;
    public GameManager gm;
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        aS = GetComponent<AudioSource>();
        switch (gm.currentState)
        {
            case GameManager.gameState.menu:
                PlayMenuMusic();
                break;
            case GameManager.gameState.game:
                PlayGameMusic();
                break;
            case GameManager.gameState.paused:
                break;
            case GameManager.gameState.gameOver:
                StopPlayingMusic();
                PlayLossSound();
                break;
            default:
                break;
        }

    }
    public void PlayWinSound()
    {
        aS.PlayOneShot(winSound);
    }
    public void PlayLossSound()
    {
        aS.PlayOneShot(lossSound);
    }
    public void PlayGameMusic()
    {
        FadeToNewClip(Mixtape[Random.Range(0, Mixtape.Count)], 1.5f);
    }
    public void PlayMenuMusic()
    {
        FadeToNewClip(menuMusic, 1f);
    }
    public void StopPlayingMusic()
    {
        aS.Stop();
    }
    IEnumerator FadeAudio(AudioClip clip, float fadeTime)
    {
        float currentTime = 0;
        float startVolume = aS.volume;

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            aS.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeTime);
            yield return null;
        }

        aS.Stop();
        aS.clip = clip;
        aS.Play();

        currentTime = 0;

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            aS.volume = Mathf.Lerp(0, startVolume, currentTime / fadeTime);
            yield return null;
        }
    }
    public void FadeToNewClip(AudioClip newClip, float fadeTime)
    {
        StartCoroutine(FadeAudio(newClip, fadeTime));
    }

}
