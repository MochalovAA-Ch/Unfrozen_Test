using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    public static event Action<int> StartFight;
    public static event Action<int> EndFight;

    TeamSpawner teamSpawner;

    public List<UnitPresenter> team1;
    public List<UnitPresenter> team2;

    public Text infoText;

    UnitPresenter currentUnitToAct;
    //List<UnitPre>

    bool isPlayerMove;
    bool isFightInProgress;
    bool isFightOver;


    // Start is called before the first frame update
    void Start()
    {
        UnitPresenter.OnUnitClicked += OnUnitClicked;
        UnitPresenter.OnUnitMouseEnter += OnUnitMouseEnter;
        UnitPresenter.OnUnitMouseExit += OnUnitMouseExit;
        teamSpawner = GetComponent<TeamSpawner>();
        for ( int i = 0; i < team1.Count; i++ )
        {
            team1[i].Init( teamSpawner.unitData[i % 2] );
            team2[i].Init( teamSpawner.unitData[i % 2] );
        }
        //UnityEngine.Random rand = new UnityEngine.Random();
        isPlayerMove = true;//UnityEngine.Random.Range( 0, 2 ) == 0 ? false : true;

        StartCoroutine( StartNextTurn_WithDealy() );
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartNextTurn()
    {
        if( IsTeamIsDead( team1 ) )
        {
            SetInfoText( "Победа ИИ" );
            return;
        }

        if( IsTeamIsDead( team2 ) )
        {
            SetInfoText( "Победа Игрока" );
            return;
        }



        if ( !IsTeamCanAct( team1 ) && IsTeamCanAct( team2 ) )
            ResetStates();
        else
            ResetSprites();


        isPlayerMove = !isPlayerMove;
        SetInfoText( isPlayerMove ? "Ход игрока" : "Ход ИИ" );
        List<UnitPresenter> actTeam = isPlayerMove ? team1 : team2; //Команда которая выполняет ход
        currentUnitToAct = GetRandomUnitToAct( actTeam );
        currentUnitToAct.SetActSprite( true );

        
        if ( !isPlayerMove )
            ActOnUnit( currentUnitToAct, team1, GetRandomUnit(team1) );
    }

    IEnumerator StartNextTurn_WithDealy()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds( 2 );
        StartNextTurn();
    }

    IEnumerator WaitFightCoroutine()
    {
        isFightInProgress = true;

          //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds( 1 );

        isFightInProgress = false;
        SetDeadStates( team1 );
        SetDeadStates( team2 );

        StartNextTurn();
    }

    void SetInfoText( string Text){ infoText.text = Text; }

    UnitPresenter GetRandomUnitToAct( List<UnitPresenter> team )
    {
        if ( IsTeamIsDead( team ) )
            return null;

        if ( !IsTeamCanAct( team ) )
            return null;

        while ( true )
        {
            int n = UnityEngine.Random.Range( 0, team.Count );
            if ( team[n] != null && !team[n].IsDead && team[n].CanAct )
                return team[n];
        }
    }

    UnitPresenter GetRandomUnit( List<UnitPresenter> team )
    {
        if ( IsTeamIsDead( team ) ) return null;

        while ( true )
        {
            int n = UnityEngine.Random.Range( 0, team.Count );
            if ( team[n] != null && !team[n].IsDead )
                return team[n];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actUnit">Юнит который выполняет действие</param>
    /// <param name="team">Команда над которой выполняется действие</param>
    /// <param name="actOnUnit">Юнит над которым выполняется действие</param>
    void ActOnUnit( UnitPresenter actUnit ,List<UnitPresenter> team, UnitPresenter actOnUnit)
    {
        StartFight( 0 );
        actUnit.Action( team, actOnUnit );

        StartCoroutine( WaitFightCoroutine() );
    }

    void OnUnitClicked( UnitPresenter clickedUnit )
    {
        if ( !isPlayerMove || isFightInProgress)
            return;

        if( team1[0] == clickedUnit )
        {
            Debug.Log( "Нельзя (?) бить самого себя" );
            return;
        }

        if( team1.Contains( clickedUnit ) )
        {
            Debug.Log( "Нельзя (?) бить союзников себя" );
            return;
        }

        ActOnUnit( currentUnitToAct, team2, clickedUnit );
    }

    void OnUnitMouseEnter( UnitPresenter unitPresenter )
    {
        if ( !isPlayerMove || isFightInProgress) return;
        SetHitSpriteToUnit( unitPresenter, true );
    }

    void SetHitSpriteToUnit( UnitPresenter unitPresenter, bool val  )
    {
        if ( team2.Contains( unitPresenter ) ) unitPresenter.SetHitSprite( val );
    }


    void OnUnitMouseExit( UnitPresenter unitPresenter )
    {
        if ( !isPlayerMove ) return;
        SetHitSpriteToUnit( unitPresenter, false );
    }

        bool IsTeamIsDead( List<UnitPresenter> team  )
    {
        for ( int i = 0; i < team.Count; i++ )
        {
            if ( team[i] != null && !team[i].IsDead  )
            {
                return false;
            }
        }
        return true;
    }

    bool IsTeamCanAct( List<UnitPresenter> team )
    {
        for ( int i = 0; i < team.Count; i++ )
        {
            if ( team[i] != null && !team[i].IsDead && team[i].CanAct )
            {
                return true;
            }
        }
        return false;
    }

    void SetDeadStates( List<UnitPresenter> team )
    {
        for( int i = 0; i < team.Count; i++ )
        {
            if ( team[i] != null && team[i].IsDead )
            {
                team[i].SetDeadSate();
            }
        }
    }

    void ResetSprites()
    {
        team1.ForEach( ResetSprite );
        team2.ForEach( ResetSprite );
    }
    void ResetSprite( UnitPresenter unitPresenter  )
    {
        unitPresenter.SetActSprite( false );
        unitPresenter.SetHitSprite( false );
    }

    void ResetUnitState( UnitPresenter unitPresenter ) 
    { 
        unitPresenter.CanAct = true;
        ResetSprite( unitPresenter );
    }

    void ResetStates()
    {
        team1.ForEach( ResetUnitState );
        team2.ForEach( ResetUnitState );
    }


}
