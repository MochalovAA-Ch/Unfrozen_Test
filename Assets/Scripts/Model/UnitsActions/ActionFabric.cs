using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionStrategyFabric
{
    public static IActionStrategy GetStrategy(UNIT_TYPE type)
    {
        switch ( type )
        {
            case UNIT_TYPE.MINER:
                return new ActStrat_HitOneUnit();
            case UNIT_TYPE.MINER_GOLD:
                return new ActStrat_HitAllUnits();
            default:
                return new ActStrat_HitOneUnit();
        }
    }
}
