using HelixJampGameLogic;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputControler : MonoBehaviour
{
    public GameObject Camera;
    public GameObject RotationRoot;

    private readonly Vector3 CameraViewOffset = new Vector3(0, 3, -18);
    //private readonly GameInstanceState gameInstance;
    private Vector3 previousPosition = default;

    public void CameraControlUpdateLoop()
    {
        const float SpeedViewScroll = 30f;
         
        if (HelixJampGameLogic.GameState.Game.CurrentLevel.CurrentPlatform == null)
            return;
        var targetpos = HelixJampGameLogic.GameState.Game.CurrentLevel.CurrentPlatform.position + CameraViewOffset;
        Camera.transform.position = 
            Vector3.MoveTowards(Camera.transform.position, targetpos, SpeedViewScroll * Time.deltaTime);
    }

    public void MouseInputControlUpdateLoop()
    {
        const float sensetive = 0.3f;

        if (Input.GetMouseButton(0))
        {
            var d = Input.mousePosition - previousPosition;
            var angleret = -d.x * sensetive;
            RotationRoot.transform.Rotate(0, angleret, 0);
        }
        previousPosition = Input.mousePosition;
    }

    void Update()
    {
        CameraControlUpdateLoop();
        MouseInputControlUpdateLoop();
    }
}
