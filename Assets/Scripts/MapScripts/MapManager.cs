using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    int selectedSet;
    int selectedLevel;
    int selectedBranch;

    string sceneName;

    void Start()
    {
        StartCoroutine(GameObject.Find("Transition").GetComponent<TransitionManager>().transitionIn());
        GameObject.Find("MusicManager").GetComponent<MusicManager>().playSetLoop(Resources.Load("Music/Invitation") as AudioClip, true);

        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject planetLocations = GameObject.Find("PlanetLocations");

        for (int i = 1; i < planetLocations.transform.childCount; i++)
        {
            GameObject setPlanet = Instantiate(manager.planetSeed[i-1]);
            setPlanet.transform.SetParent(planetLocations.transform.GetChild(i), false) ;
        }
        
        for(int i = 0; i < manager.pathTaken.Count; i++)
        {
            for (int j = 0; j < GameObject.Find("Paths").transform.childCount; j++)
            {
                Path possiblePath = GameObject.Find("Paths").transform.GetChild(j).GetComponent<Path>();
                if (possiblePath.pathSet == manager.pathTaken[i].x && possiblePath.pathLevel == manager.pathTaken[i].y && possiblePath.previousBranch == manager.pathTaken[i].z)
                {
                    possiblePath.gameObject.GetComponent<RawImage>().enabled = true;
                    possiblePath.gameObject.GetComponent<RawImage>().color = Color.green;
                }
            }
        }
    }

    public void setScene(string sceneName, int set, int level, int branch)
    {
        this.sceneName = sceneName;
        selectedSet = set;
        selectedLevel = level;
        selectedBranch = branch;
    }

    public void goToScene()
    {
        if(sceneName != null)
        {
            StartCoroutine(startTransition(sceneName));
        }
    }

    public IEnumerator startTransition(string levelName)
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.currentSet = selectedSet;
        manager.currentLevel = selectedLevel;

        manager.pathTaken.Add(new Vector3(selectedSet, selectedLevel, selectedBranch));

        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(Resources.Load("SoundEffects/03 Coin") as AudioClip);

        StartCoroutine(GameObject.Find("Transition").GetComponent<TransitionManager>().transitionOut());
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levelName);
    }
}
