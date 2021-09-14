using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlanetLocation : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public int setNumber;
    public int levelNumber;
    public int previousBranch = 0;

    Transform levelSelect;
    GameManager manager;

    public PlanetLocation[] previousLevels;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelSelect = GameObject.Find("PlanetSelect").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < previousLevels.Length; i++)
        {
            if (manager.currentSet == previousLevels[i].setNumber && manager.currentLevel == previousLevels[i].levelNumber && levelSelect.position != transform.position)
            {
                StartCoroutine(moveSelector());
                GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/beep_03") as AudioClip, .5f);
                GameObject.Find("PlanetIndicator").GetComponent<TextMeshProUGUI>().text = transform.GetChild(0).GetComponent<PlanetLevel>().levelName;

                if(previousLevels.Length > 1)
                {
                    if(i > 0)
                    {
                        previousBranch = i;
                    }
                    else
                    {
                        previousBranch = 0;
                    }
                }
                else
                {
                    previousBranch = 0;
                }
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (levelSelect.position == transform.position)
        {
            GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/retro_beep_02") as AudioClip, .5f);
            GameObject.Find("PlanetConfirm").GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            PlanetLevel levelSelected = transform.GetChild(0).GetComponent<PlanetLevel>();
            GameObject.Find("MapManager").GetComponent<MapManager>().setScene(levelSelected.sceneName, setNumber, levelNumber, previousBranch);
            GameObject.Find("Button").SetActive(true);
        }
    }

    private IEnumerator moveSelector()
    {
        float startTime = Time.time;
        while (Time.time < startTime + .15f)
        {
            levelSelect.position = Vector3.Lerp(levelSelect.position, transform.position, (Time.time - startTime) / .15f);
            yield return null;
        }
        levelSelect.position = transform.position;
    }
}
