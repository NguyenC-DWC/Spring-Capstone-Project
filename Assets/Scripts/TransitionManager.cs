using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Transform blackScreen;
    public Transform screenHide;
    public Transform screenHide2;
    public Transform screenShow;

    public IEnumerator transitionIn()
    {
        float startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            blackScreen.position = Vector3.Lerp(blackScreen.position, screenHide.position, (Time.time - startTime) / 1f);
            yield return null;
        }

        blackScreen.position = screenHide2.position;
    }

    public IEnumerator transitionOut()
    {

        float startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            blackScreen.position = Vector3.Lerp(blackScreen.position, screenShow.position, (Time.time - startTime) / 1f);
            yield return null;
        }

        blackScreen.position = screenShow.position;
    }
}