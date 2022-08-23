using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotationObject : MonoBehaviour
{
    public Vector3 Speed;
    // Update is called once per frame
    void Update()
    {
      this.transform.Rotate(Speed*Time.deltaTime);
    }
}
