using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class VectorDemoInfo : MonoBehaviour
{
    public Transform Object1;
    public Transform Object2;
    public float Factor;
    private TMP_Text Messagebox;
    // Start is called before the first frame update
    void Start()
    {
        Messagebox = this.GetComponentInParent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string s = $"{Object1.eulerAngles}+{Object2.eulerAngles} = {Object1.eulerAngles + Object2.eulerAngles}\n"
        +$"{Object1.eulerAngles}-{Object2.eulerAngles} = {Object1.eulerAngles - Object2.eulerAngles}\n"
        +$"{Object1.eulerAngles}*{Factor} = {(Object1.eulerAngles * Factor)}\n"
        +$"Distance {Object1.eulerAngles};{Object2.eulerAngles} = {Vector3.Distance(Object1.eulerAngles,Object2.eulerAngles)}\n";

        Messagebox.text = s;
    }
}
