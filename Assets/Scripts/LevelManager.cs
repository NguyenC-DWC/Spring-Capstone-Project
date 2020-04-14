using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameObject ship;
    GameObject field;

    public AudioClip levelMusic;
    public AudioClip bossMusic;

    bool shipMove = false;

    private void Start()
    {
        ship = GameObject.Find("PlayerShip");
        field = GameObject.Find("PlayingField");

        StartCoroutine(startLevel());
    }

    private IEnumerator startLevel()
    {
        StartCoroutine(GameObject.Find("Transition").GetComponent<TransitionManager>().transitionIn());
        yield return new WaitForSeconds(.75f);

        GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/warpout") as AudioClip);
        yield return new WaitForSeconds(1f);

        float initialSpeed = 12;
        while (initialSpeed > 0)
        {
            ship.GetComponent<Rigidbody2D>().velocity = transform.right * initialSpeed;
            initialSpeed -= .2f;
            yield return null;
        }

        initialSpeed = 0;
        ship.GetComponent<Rigidbody2D>().velocity = transform.right * initialSpeed;
        ship.GetComponent<PlayerShip>().setActive(true);


        field.GetComponent<Rigidbody2D>().velocity = transform.right * field.GetComponent<PlayingField>().speed;
        GameObject.Find("MusicManager").GetComponent<MusicManager>().playSetLoop(levelMusic, true);
    }

    public void startSlow()
    {
        StartCoroutine(field.GetComponent<PlayingField>().slowToStop());
        StartCoroutine(soundAlarm());
        StartCoroutine(GameObject.Find("MusicManager").GetComponent<MusicManager>().fadeToMusic(bossMusic, true));
    }

    private IEnumerator soundAlarm()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/ambient_alarm1") as AudioClip,.5f);
            yield return new WaitForSeconds(.75f);
        }
    }

    public void endLevel()
    {
        ship.GetComponent<PlayerShip>().setActive(false);
        StartCoroutine(field.GetComponent<PlayingField>().speedUp(3));

        StartCoroutine(moveShipOut(ship));
        StartCoroutine(GameObject.Find("MusicManager").GetComponent<MusicManager>().fadeToMusic(Resources.Load("Music/Space Megaforce - Area Clear") as AudioClip, false));
        StartCoroutine(sceneOut());
    }

    private IEnumerator sceneOut()
    {
        yield return new WaitForSeconds(7f);

        while (GameObject.Find("MusicManager").GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }

        Debug.Log("Music Stopped Playing");
        yield return new WaitForSeconds(3);

        StartCoroutine(GameObject.Find("Transition").GetComponent<TransitionManager>().transitionOut());
        yield return new WaitForSeconds(.5f);

        SceneManager.LoadScene("MapScene");
    }

    private IEnumerator moveShipOut(GameObject ship)
    {
        StartCoroutine(delayLeave());

        while (!shipMove)
        {
            ship.GetComponent<Rigidbody2D>().velocity = field.GetComponent<Rigidbody2D>().velocity;
            yield return null;
        }

        float initialSpeed = -5;
        while (initialSpeed < 10)
        {
            ship.GetComponent<Rigidbody2D>().velocity = field.GetComponent<Rigidbody2D>().velocity + (Vector2)(transform.right * initialSpeed);
            initialSpeed += .1f;
            yield return null;
        }
    }

    private IEnumerator delayLeave()
    {
        yield return new WaitForSeconds(5);
        shipMove = true;
        Debug.Log("Begin move out.");
    }
}
