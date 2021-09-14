using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRotate : MonoBehaviour
{
    public float setRotationSpeed = 360;
    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 360 * Time.deltaTime;
        float currentAngle = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,currentAngle + rotationSpeed));
    }
}
