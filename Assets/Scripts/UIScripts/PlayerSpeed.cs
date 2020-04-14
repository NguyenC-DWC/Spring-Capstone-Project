using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public int currentSpeed;

    public GameObject[] speed;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < speed.Length; i++)
        {
            if (i < currentSpeed)
            {
               speed[i].SetActive(true);
            }
            else
            {
               speed[i].SetActive(false);
            }
        }
    }

    public void updateSpeed(int shipSpeed)
    {
        currentSpeed = shipSpeed - 3;
    }
}
