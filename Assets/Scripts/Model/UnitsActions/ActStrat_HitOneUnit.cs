using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActStrat_HitOneUnit : IActionStrategy
{
    public void Action( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit )
    {
        targetUnit.TakeDamage( actUnit.Damage );
        actUnit.PlayAtackAnimation( "PickaxeCharge" );
    }
}
