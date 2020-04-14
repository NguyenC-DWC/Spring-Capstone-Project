using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIcon : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject planets = GameObject.Find("Planets");

        for (int i = 0; i < planets.transform.childCount; i++)
        {
            PlanetLevel currentPlanet = planets.transform.GetChild(i).GetComponent<PlanetLevel>();
            if(currentPlanet.setNumber == manager.currentSet && currentPlanet.levelNumber == manager.currentLevel)
            {
                rect.anchoredPosition = currentPlanet.gameObject.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 20);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + Mathf.Cos(Time.time) * .15f);
        rect.anchoredPosition = position;
    }
}
