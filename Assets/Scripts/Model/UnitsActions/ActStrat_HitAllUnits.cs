using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActStrat_HitAllUnits : IActionStrategy
{

    public void Action( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit )
    {
        for( int i = 0; i < unitsToAffect.Count; i++ )
        {
            if ( unitsToAffect != null )
                unitsToAffect[i].TakeDamage( actUnit.Damage );
        }

        actUnit.PlayAtackAnimation( "Miner_1" );
    }

    public void MoveUnitsToForefront( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit )
    {
        for ( int i = 0; i < unitsToAffect.Count; i++ )
        {
            if ( unitsToAffect != null )
                unitsToAffect[i].IncreaseScale();
        }
    }

    public void MoveUnitsToBackfront( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit )
    {
        for ( int i = 0; i < unitsToAffect.Count; i++ )
        {
            if ( unitsToAffect != null )
                unitsToAffect[i].DecreaseScale();
        }
    }

}
