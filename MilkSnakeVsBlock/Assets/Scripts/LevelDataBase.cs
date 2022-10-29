using UnityEngine;

public class LevelDataBase : MonoBehaviour
{
    public int LevelSize = 4;

    // Start is called before the first frame update
    private void Start()
    {
        var t = this.transform.Find("Trasse");
        t.transform.localScale = new Vector3(LevelSize * 2, t.transform.localScale.y, t.transform.localScale.z);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}