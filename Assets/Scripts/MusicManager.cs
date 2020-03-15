using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicPlayer;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    public void startFade(AudioClip loadMusic)
    {
        StartCoroutine(fadeToMusic(loadMusic));
    }

    public IEnumerator fadeToMusic(AudioClip loadMusic)
    {
        while(musicPlayer.volume > 0)
        {
            musicPlayer.volume -= .001f;
            yield return null;
        }

        musicPlayer.volume = 0;
        musicPlayer.Stop();

        yield return new WaitForSeconds(1);

        musicPlayer.clip = loadMusic;
        musicPlayer.volume = .25f;
        musicPlayer.Play();
    }


}
