using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicPlayer;

    public bool loop;

    private void Start()
    {
;   }

    public void playSetLoop(AudioClip loadMusic, bool loop)
    {
        musicPlayer.Stop();
        musicPlayer.loop = loop;
        musicPlayer.clip = loadMusic;
        musicPlayer.Play();

    }

    public IEnumerator fadeToMusic(AudioClip loadMusic, bool loop)
    {
        while(musicPlayer.volume > 0)
        {
            musicPlayer.volume -= .001f;
            yield return null;
        }

        musicPlayer.volume = 0;
        musicPlayer.Stop();

        yield return new WaitForSeconds(1);

        musicPlayer.loop = loop;

        musicPlayer.clip = loadMusic;
        musicPlayer.volume = .25f;
        musicPlayer.Play();
    }


}
