using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{
    public bool isActive;
    public Transform displayShow;
    public Transform displayHide;

    public string weaponToDisplay;

    public TextMeshProUGUI weaponName;
    public Image weaponImage;


    public int currentLevel;
    public Image[] level;
    public Sprite emptyLevel;
    public Sprite fullLevel;
    public Sprite levelUp;

    private void Start()
    {
        PlayerWeapon weaponSet = GameObject.Find(weaponToDisplay).GetComponentInChildren<PlayerWeapon>();
        weaponName.text = weaponSet.weaponName;
        weaponImage.sprite = weaponSet.weaponImage;
    }

    void Update()
    {
        if(isActive)
        {
            for (int i = 0; i < level.Length; i++)
            {
                if (i < currentLevel)
                {
                    if (currentLevel == 6)
                    {
                        level[i].sprite = levelUp;
                    }
                    else if (currentLevel >= 3 && i < 3)
                    {
                        level[i].sprite = levelUp;
                    }
                    else
                    {
                        level[i].sprite = fullLevel;
                    }
                }
                else
                {
                    level[i].sprite = emptyLevel;
                }
            }
        }
    }

    public void updateLevel(ref PlayerWeapon weapon)
    {
        currentLevel = weapon.levelCharge;
    }

    public IEnumerator toggleDisplay()
    {
        isActive = !isActive;
        float startTime = Time.time;

        if (isActive)
        {
            while (Time.time < startTime + 1f)
            {
                transform.position = Vector3.Lerp(transform.position, displayShow.position, (Time.time - startTime) / 1f);
                yield return null;
            }
            transform.position = displayShow.position;
        }
        else
        {
            while (Time.time < startTime + 1f)
            {
                transform.position = Vector3.Lerp(transform.position, displayHide.position, (Time.time - startTime) / 1f);
                yield return null;
            }
            transform.position = displayHide.position;
        }
        Debug.Log("Done");
    }
}
