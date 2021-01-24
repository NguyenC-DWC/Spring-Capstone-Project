using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //
    public static GameManager managerInstance;

    public int currentSet;
    public int currentLevel;

    public int currentHealth;
    public int maxHealth;
    public int currentScore = 0;
    public int currentSpeed = 3;

    public List<Vector3> pathTaken = new List<Vector3>();
    public List<GameObject> planetSeed = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if(managerInstance == null)
        {
            managerInstance = this;
            randomizeLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void randomizeLevels()
    {
        for(int i = 0; i < planetSeed.Count; i++)
        {
            GameObject temp = planetSeed[i];
            int index = Random.Range(i, planetSeed.Count);
            planetSeed[i] = planetSeed[index];
            planetSeed[index] = temp;
        }
    }

    public void addScore(int scoreValue)
    {
        currentScore += scoreValue;
    }
}
