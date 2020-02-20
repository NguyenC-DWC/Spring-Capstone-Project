using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerWeapon" || other.tag == "EnemyWeapon" || other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
