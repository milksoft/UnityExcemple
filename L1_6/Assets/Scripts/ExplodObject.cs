using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodObject : MonoBehaviour
{
    public float Volume;
    public Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        var center = this.transform.position;
        Debug.Log(this.gameObject.name);
        rigidbodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
        Debug.Log(rigidbodies.Length);
        foreach (var item in rigidbodies)
        {
            var position = item.transform.position;
            var toPosition = position - center;
            item.AddForce(toPosition * Volume, ForceMode.Impulse);
        }
    }

}
