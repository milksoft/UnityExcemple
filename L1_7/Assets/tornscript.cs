using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornscript : MonoBehaviour
{
    Rigidbody tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = this.gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        tr.AddRelativeTorque(new Vector3(0, 0.1f, 0), ForceMode.Impulse);
    }
}
