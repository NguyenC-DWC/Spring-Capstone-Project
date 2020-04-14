using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlanetLevel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public int setNumber;
    public int levelNumber;
    public string levelName;
    public string sceneName;

    public PlanetLevel[] previousLevels;

    Transform levelSelect;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelSelect = GameObject.Find("PlanetSelect").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        for(int i = 0; i < previousLevels.Length; i++)
        {
            if(manager.currentSet == previousLevels[i].setNumber && manager.currentLevel == previousLevels[i].levelNumber && levelSelect.position != transform.position)
            {
                StartCoroutine(moveSelector());
                GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/beep_03") as AudioClip, .5f);
                GameObject.Find("PlanetIndicator").GetComponent<TextMeshProUGUI>().text = levelName;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(levelSelect.position == transform.position)
        {
            GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/retro_beep_02") as AudioClip, .5f);
            GameObject.Find("PlanetConfirm").GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            GameObject.Find("MapManager").GetComponent<MapManager>().setScene(sceneName, setNumber, levelNumber);
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
