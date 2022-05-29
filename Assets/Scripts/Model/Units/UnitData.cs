using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{
    public float AttackDamage; //����
    public float HP;  //���������� ������

    bool canAct;
    public bool IsDead { get; private set; }

    public UnitData()
    {
        AttackDamage = 5;
        HP = 10;
        IsDead = false;
        canAct = false;
    }


    public void Attack( UnitData enemy )
    {
        if( !canAct )
        {
            Debug.Log( "� unit cannot perform an action" );
        }

        enemy.TakeDamage( AttackDamage );
        canAct = false;
    }

    
    public void TakeDamage( float damage )
    {
        HP -= damage;
        if ( HP <= 0 )
            IsDead = true;
    }
}
