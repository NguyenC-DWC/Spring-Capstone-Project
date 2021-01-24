using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float initialWidth;

    private void Start()
    {
        initialWidth = GetComponent<RectTransform>().sizeDelta.x;
        Debug.Log(initialWidth);
    }

    public void barDecrease(float currentBossHealth)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(initialWidth * currentBossHealth, GetComponent<RectTransform>().sizeDelta.y);
    }
}
