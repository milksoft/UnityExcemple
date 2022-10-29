using System;

using TMPro;

using UnityEngine;

public class Food : MonoBehaviour
{
    public int Value = 1;
    private GameStateObject _State;

    public void Awake()
    {
        var textblock = this.gameObject.GetComponentInChildren<TMP_Text>();
        if (textblock == null)
            throw new ArgumentNullException(nameof(TextMeshPro));
        textblock.SetText(Value.ToString());

        _State = GameStateObject.GetInstance(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_State.Snake.IsSnakeHead(collision.gameObject))
        {
            _State.Snake.UpdateHealth(Value);
            Destroy(this.gameObject);
        }
    }
}