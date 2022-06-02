using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionStrategy
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actUnit">���� ������� ���������� ��������</param>
    /// <param name="unitsToAffect">����� �� ������� �������� ����� ������</param>
    /// <param name="targetUnit">������� ����, �� �������� ������������ �������� ( ���� )</param>
    public void Action( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit );

    public void MoveUnitsToForefront( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit );

    public void MoveUnitsToBackfront( UnitPresenter actUnit, List<UnitPresenter> unitsToAffect, UnitPresenter targetUnit );
}
