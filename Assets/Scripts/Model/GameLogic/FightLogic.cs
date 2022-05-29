using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLogic
{
    const uint TEAMS_COUNT = 2;     //Количество комманд
    const uint SLOTS_IN_TEAM = 4;   //Количество юнитов в комманде
    List<List<UnitData>> teams;     //Список команд из юнитов

    uint teamTurnIdnex = 0;         //Индекс команды, которая ходит в данный момент

    public void Init()
    {
        teams = new List<List<UnitData>>();
        for ( int i = 0; i < TEAMS_COUNT; i++ )
        {
            teams.Add( new List<UnitData>() );
            teams[i] = new List<UnitData>();
            for( int slotIndex = 0; slotIndex < SLOTS_IN_TEAM; slotIndex++ )
            {
                teams[i].Add( new UnitData() );
            }
        }

        teamTurnIdnex = (uint)Random.Range( 0, 2 );
    }

    public void StartNewTurn()
    {
        teamTurnIdnex = ( teamTurnIdnex + 1 ) % TEAMS_COUNT ;
    }

    public void Attack( UnitData atackUnit, UnitData atackedUnit )
    {
        atackedUnit.Attack( atackedUnit );

    }

    public void Skip()
    {
        StartNewTurn();
    }


    bool IsFightOver()
    {
        for( int teamIndex = 0; teamIndex < TEAMS_COUNT; teamIndex++ )
        {
            bool isAllTeamDead = true;
            for( int slotIndex = 0; slotIndex < SLOTS_IN_TEAM; slotIndex++ )
            {
                UnitData unitData = teams[teamIndex][slotIndex];
                if ( unitData != null && !unitData.IsDead )
                    return false;
            }

        }

        return true;
    }


}
