using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        gameObject.SetActive(false);
    }
}
