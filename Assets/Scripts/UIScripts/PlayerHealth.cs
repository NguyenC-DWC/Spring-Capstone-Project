using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public Image[] health;
    public Sprite emptyHealth;
    public Sprite fullHealth;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < health.Length; i++)
        {
            if(i < currentHealth)
            {
                health[i].sprite = fullHealth;
            }
            else
            {
                health[i].sprite = emptyHealth;
            }


            if(i < maxHealth)
            {
                health[i].enabled = true;
            }
            else
            {
                health[i].enabled = false;
            }
        }
    }

    public void updateHealth(int shipHealth)
    {
        currentHealth = shipHealth;
    }
}
