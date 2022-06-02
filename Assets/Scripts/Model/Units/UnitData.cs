using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UNIT_TYPE
{
    MINER,
    MINER_GOLD
}
public class UnitData
{
    public float AttackDamage; //Урон
    public float HP;  //Количество жизней

    [SerializeField]
    public UNIT_TYPE unitType;

    public bool IsDead { get; private set; }  //Является ли юнит мертвым
    public bool CanAct { get; set; }  //Может ли выполнять действия ( в данном ходу )

    public UnitData()
    {
        AttackDamage = 5;
        HP = 10;
        IsDead = false;
        CanAct = true;
    }


    public void Attack( UnitData enemy )
    {
        if( !CanAct )
        {
            Debug.Log( "А unit cannot perform an action" );
        }

        enemy.TakeDamage( AttackDamage );
        CanAct = false;
    }
    
    public void TakeDamage( float damage )
    {
        HP -= damage;
        if ( HP <= 0 )
            IsDead = true;
    }
}
