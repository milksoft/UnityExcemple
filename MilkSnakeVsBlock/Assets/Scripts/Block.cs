using TMPro;

using UnityEngine;

public class Block : MonoBehaviour
{
    public Color lerpedColor = Color.white;
    public int Value = 1;
    private GameStateObject _State;

    private void Awake()
    {
        var textblock = this.gameObject.GetComponentInChildren<TMP_Text>();
        if (textblock == null)
            throw new System.ArgumentNullException(nameof(TextMeshPro));
        textblock.SetText(Value.ToString());
        setcolorshaider();

        _State = GameStateObject.GetInstance(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_State.Snake.IsSnakeHead(other.gameObject))
        {
            _State.Snake.UpdateHealth(-Value);
            if (_State.StateUi != UiView.StateUi.GameOver)
                Destroy(this.gameObject);
        }
    }

    private void setcolorshaider()
    {
        lerpedColor = Color.Lerp(lerpedColor, Color.red, (float)Value / 20f);
        this.gameObject.GetComponentInChildren<Renderer>().material.color = lerpedColor;
    }
}