using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public GameObject powerUp;
    public Vector3 lastShipPos;

    public int shipsInFormation;

    public int scoreValue = 0;

    public bool dropsItem = false;

    private void Start()
    {
        shipsInFormation = transform.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.childCount == 0)
        {
            //Ensures that the powerup only deploys if the player hits all of the ships.
            if(shipsInFormation <= 0)
            {
                Debug.Log("All ships hit.");
                if(dropsItem)
                {
                    Instantiate(powerUp, lastShipPos, Quaternion.identity);
                }
                GameObject.Find("GameManager").GetComponent<GameManager>().addScore(scoreValue);
            }
            else
            {
                Debug.Log("Not all ships hit.");
            }
            Destroy(this.gameObject);
        }
        else if(transform.childCount == 1 && shipsInFormation == 1)
        {
            lastShipPos = transform.GetChild(0).transform.position;
        }
    }

    public void decreaseShips()
    {
        shipsInFormation -= 1;
    }
}
