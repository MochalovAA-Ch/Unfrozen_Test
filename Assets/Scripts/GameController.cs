using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{

    public static event Action<int> StartFight;
    public static event Action<int> EndFight;


    public List<UnitPresenter> team1;
    public List<UnitPresenter> team2;

    int testAtackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UnitPresenter.OnUnitClicked += OnUnitClicked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnUnitClicked( UnitPresenter unitPresenter )
    {
        if( team1[0] == unitPresenter )
        {
            Debug.Log( "Нельзя (?) бить самого себя" );
            return;
        }

        if( team1.Contains( unitPresenter ) )
        {
            Debug.Log( "Нельзя (?) бить союзников себя" );
            return;
        }


        team1[0].Attack( unitPresenter );
        CheckForDead( team1 );
        CheckForDead( team2 );
        Debug.Log( "OnUnitClikced" );
    }

    void CheckForDead( List<UnitPresenter> team )
    {
        for( int i = 0; i < team.Count; i++ )
        {
            if ( team[i].IsDead )
            {
                team[i].SetDeadSate();
            }
        }
    }
}
