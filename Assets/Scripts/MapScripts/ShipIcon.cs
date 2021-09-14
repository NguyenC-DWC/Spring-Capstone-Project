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
        GameObject planetLocations = GameObject.Find("PlanetLocations");

        for (int i = 0; i < planetLocations.transform.childCount; i++)
        {
            PlanetLocation currentPlanet = planetLocations.transform.GetChild(i).GetComponent<PlanetLocation>();
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
        Debug.Log(Mathf.Cos(Time.time) * .15f);
    }
}
