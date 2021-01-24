using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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


            float alarmTrans = 0;
            bool adding = true;
            RawImage alarm = GameObject.Find("Alarm").GetComponent<RawImage>();

            do
            {

                if (adding)
                {
                    alarmTrans += .05f;
                    if (alarmTrans >= 1)
                    {
                        adding = false;
                        alarmTrans = 1;
                    }
                }
                else
                {
                    alarmTrans -= .05f;
                    if(alarmTrans <= 0)
                    {
                        alarmTrans = 0;
                    }
                }

                alarm.color = new Color(55,0,0,alarmTrans);
                yield return new WaitForSeconds(.0125f);

            } while (alarmTrans != 0);

            yield return new WaitForSeconds(.5f);
        }

        StartCoroutine(GameObject.Find("Boss").GetComponent<BossCommon>().bossEntrance());
    }

    public void endLevel()
    {
        ship.GetComponent<PlayerShip>().setActive(false);
        StartCoroutine(field.GetComponent<PlayingField>().speedUp(3));

        StartCoroutine(moveShipOut(ship));
        StartCoroutine(GameObject.Find("MusicManager").GetComponent<MusicManager>().fadeToMusic(Resources.Load("Music/Space Megaforce - Area Clear") as AudioClip, false));
        StartCoroutine(sceneOut());

        StartCoroutine(textMove());
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

    private IEnumerator textMove()
    {
        Transform topText = GameObject.Find("Clear").GetComponent<Transform>();
        Transform bottomText = GameObject.Find("NextStage").GetComponent<Transform>();
        Transform endPosition1 = GameObject.Find("ClearPos").GetComponent<Transform>();
        Transform endPosition2 = GameObject.Find("NextPos").GetComponent<Transform>();

        yield return new WaitForSeconds(8f);

        float startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            topText.position = Vector3.Lerp(topText.position, endPosition1.position, (Time.time - startTime) / 1f);
            yield return null;
        }

        topText.position = endPosition1.position;

        startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            bottomText.position = Vector3.Lerp(bottomText.position, endPosition2.position, (Time.time - startTime) / 1f);
            yield return null;
        }

        bottomText.position = endPosition2.position;
    }
}
