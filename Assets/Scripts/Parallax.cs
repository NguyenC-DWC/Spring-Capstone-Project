using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject camera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float posToCamera = (camera.transform.position.x * (1 - parallaxEffect));
        float dist = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y,transform.position.z);

        if (posToCamera > startPos + length)
        {
            startPos += length;
        }
        else if (posToCamera < startPos - length)
        {
            startPos -= length;
        }
    }
}
