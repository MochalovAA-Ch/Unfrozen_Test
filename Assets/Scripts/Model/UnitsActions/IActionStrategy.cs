using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionStrategy
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actUnit">Юнит который производит действие</param>
    /// <param name="unitsToAffect">Юниты на которых действие имеет эффект</param>
    /// <param name="targetUnit">Целеовй юнит, по которому производится действие ( клик )</param>
    public void Action( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit );

}
