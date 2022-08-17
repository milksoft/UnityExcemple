using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{

    public string Message = "Welcome to Room!!!";
    public UnityEngine.UI.Text Text;
    void Start()
    {
        //UnityEngine.UI.Text ts = this.gameObject.scene.GetRootGameObjects()
        //    .SelectMany(x => x.GetComponentsInChildren<Component>())
        //v
        //    .Select(x => x.GetType().ToString()).Distinct();
        //Debug.Log(string.Join('\n',ts));
        //this.gameObject.scene.GetRootGameObjects().First().GetComponentsInChildren<Text()
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Debug.Log(Message);
            Text.text = Message;
        }
    }
}

