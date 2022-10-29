using UnityEngine;

public class Finish : MonoBehaviour
{
    private AudioSource _FinishSound;
    private GameStateObject _State;

    public void OnCollisionEnter(Collision collision)
    {
        if (_State.Snake.IsSnakeHead(collision.gameObject))
        {
            Destroy(this.gameObject);
            PlayAnimationWin();
            _State.PlayerWin();
        }
    }

    private void Awake()
    {
        _State = GameStateObject.GetInstance(this.gameObject);
        _FinishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_State.Snake.IsSnakeHead(other.gameObject))
        {
            Destroy(this.gameObject);
            PlayAnimationWin();
            _State.PlayerWin();
        }
    }

    private void PlayAnimationWin()
    {
        //_FinishSound?.Play();
    }
}