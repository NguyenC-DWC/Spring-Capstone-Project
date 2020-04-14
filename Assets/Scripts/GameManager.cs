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

    public List<Vector2> pathTaken = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if(managerInstance == null)
        {
            managerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addScore(int scoreValue)
    {
        currentScore += scoreValue;
    }
}
