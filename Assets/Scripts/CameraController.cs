using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    enum CameraStates 
    {
        DEFAULT,
        FIGHT
    }

    CameraStates cameraState;
    Camera camera;
    float defaultSize;
    public float fightSizeOffset;
    public float fightYOffset;
    // Start is called before the first frame update
    void Start()
    {
        FightController.StartFight += StartFight;
        FightController.EndFight += EndFight;
        cameraState = CameraStates.DEFAULT;
        camera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch ( cameraState )
        {
            case CameraStates.DEFAULT:
                DefaultUpdate();
                break;
            case CameraStates.FIGHT:
                FightUpdate();
                break;
        }
    }

    void StartFight( int t )
    {
        cameraState = CameraStates.FIGHT;
    }

    void EndFight( int t )
    {
        cameraState = CameraStates.DEFAULT;
    }

    void DefaultUpdate()
    {
        //Vector3.Lerp( transform.position, defaultPos + fightPosOffset, 100 * Time.deltaTime );
    }

    void FightUpdate()
    {
        //camera.orthographicSize = Mathf.Lerp( camera.orthographicSize, camera.orthographicSize - fightSizeOffset, 1 * Time.deltaTime );
       //transform.position =  Vector3.Lerp( transform.position, defaultPos + fightPosOffset, 100 * Time.deltaTime );
    }


}
