using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;

using static UiView;

public class Player : MonoBehaviour
{
    private GameObject _head;
    private int _Health;

    private Vector3 _previousMousePosition;
    private GameStateObject _State;

    private SnakeTail componentSnakeTail;
    private Vector3 tempVect = new Vector3(0, 0, 1);
    private TMP_Text textblock;

    public bool IsSnakeHead(GameObject obj) =>
        _head.GetInstanceID() == obj.GetInstanceID();

    public void PlayerInit(bool isNewSnake)
    {
        transform.position = new Vector3(3, 3, 10);
        if (componentSnakeTail != null)
            componentSnakeTail.Clear();
        else
            componentSnakeTail = new SnakeTail(_head.transform);

        _previousMousePosition = Input.mousePosition;

        int oldheal = _Health;
        _Health = 0;
        UpdateHealth(isNewSnake ? _State.StartGameHealth : oldheal);
    }

    public void UpdateHealth(int lives)
    {
        _Health += lives;
        if (_Health <= 0)
        {
            _State.PlayerDied();
            return;
        }

        _State.Score += System.Math.Abs(lives);

        if (lives < 0)
            componentSnakeTail.RemoveCircle(-lives);
        else
            componentSnakeTail.AddCircle(lives);

        textblock.SetText(_Health.ToString());
    }

    private void Awake()
    {
        _State = GameStateObject.GetInstance(this.gameObject);

        textblock = this.GetComponentInChildren<TMP_Text>();
        if (textblock == null)
            throw new System.ArgumentNullException(nameof(TextMeshPro));
        _head = this.transform.Find("Head")?.gameObject;
        if (_head == null)
            throw new System.ArgumentNullException("Head");
    }

    private void FixedUpdate()
    {
        if (_State.StateUi != StateUi.GamePlaying)
            return;
        SnakeInput();
        componentSnakeTail.Update();
    }

    private void SnakeInput()
    {
        var currentspeed = _State.Speed * Time.deltaTime;
        var d = Input.mousePosition - _previousMousePosition;
        var angleret = d.x * currentspeed;
        var xposition = transform.position.x + angleret;
        //Debug.Log(xposition);
        var size = _State.Level.LevelSize * 9;

        if (xposition > size)
            xposition = size;
        if (xposition < -size)
            xposition = -size;

        transform.position = (new Vector3(xposition, 0, transform.position.z + currentspeed));

        _previousMousePosition = Input.mousePosition;
    }

    private class SnakeTail
    {
        private readonly float _CircleDiameter;
        private readonly Material _material;
        private readonly List<Vector3> _positions = new List<Vector3>(100);
        private readonly List<Transform> _snakeCircles = new List<Transform>(100);
        private readonly Transform _SnakeHead;

        public SnakeTail(Transform SnakeHead)
        {
            _SnakeHead = SnakeHead;
            _CircleDiameter = SnakeHead.lossyScale.x;
            _positions.Add(SnakeHead.position);
            _material = SnakeHead.gameObject.GetComponent<Renderer>().material;
        }

        public void AddCircle(int count)
        {
            var lpos = _positions.Last();
            for (int i = 0; i < count; i++)
            {
                var index = _snakeCircles.Count;
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.GetComponent<Renderer>().material = _material;
                go.transform.TransformPoint(lpos);
                go.transform.localScale = new Vector3(_CircleDiameter, _CircleDiameter, _CircleDiameter);
                go.transform.parent = _SnakeHead.parent;
                _snakeCircles.Add(go.transform);
                _positions.Add(go.transform.position);
            }
        }

        public void Clear()
        {
            _snakeCircles.ForEach(x => Destroy(x.gameObject));
            _snakeCircles.Clear();
            _positions.Clear();
            _positions.Add(_SnakeHead.position);
        }

        public void RemoveCircle(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var index = _snakeCircles.Count - 1;
                Destroy(_snakeCircles[index].gameObject);
                _snakeCircles.RemoveAt(index);
                _positions.RemoveAt(index);
            }
        }

        public void Update()
        {
            float distance = ((Vector3)_SnakeHead.position - _positions[0]).magnitude;

            if (distance > _CircleDiameter)
            {
                Vector3 direction = ((Vector3)_SnakeHead.position - _positions[0]).normalized;

                _positions.Insert(0, _positions[0] + direction * _CircleDiameter);
                _positions.RemoveAt(_positions.Count - 1);

                distance -= _CircleDiameter;
            }

            for (int i = 0; i < _snakeCircles.Count; i++)
            {
                _snakeCircles[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], (distance / _CircleDiameter)/*+0.5f*/);
            }
        }
    }
}