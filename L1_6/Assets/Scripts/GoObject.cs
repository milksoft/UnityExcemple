using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoObject : MonoBehaviour
{
    public Vector3 Velocity;

    void Start()
    {
        var rb = this.GetComponentInParent<Rigidbody>();
        rb?.AddForce(Velocity, ForceMode.VelocityChange);
    }
}