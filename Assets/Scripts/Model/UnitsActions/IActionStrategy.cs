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

}
