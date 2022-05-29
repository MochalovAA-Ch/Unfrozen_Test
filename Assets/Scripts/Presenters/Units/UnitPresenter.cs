using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitPresenter : MonoBehaviour
{
    protected UnitData unitData;

    public static event Action<UnitPresenter> OnUnitClicked;
    
    public bool IsDead => unitData.IsDead;

    public UnitPresenter()
    {
        unitData = new UnitData();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        //Debug.Log( "Мышь зашла на позицию" );
    }

    private void OnMouseDown()
    {
        if ( OnUnitClicked != null )
            OnUnitClicked( this );

        //Debug.Log( "Щелкнули мышью" );
    }

    public void Attack( UnitPresenter unitToAtack )
    {
        unitData.Attack( unitToAtack.unitData );
    }

    public void SetDeadSate()
    {
        gameObject.SetActive( false );
    }
    


    
}
