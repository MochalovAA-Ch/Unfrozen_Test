using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using  Spine;
using Spine.Unity;
using TMPro;

[RequireComponent( typeof(SkeletonAnimation ) )]
public class UnitPresenter : MonoBehaviour
{
    protected UnitData unitData;
    SkeletonAnimation skeletonAnimation;

    public static event Action<UnitPresenter> OnUnitClicked;
    public static event Action<UnitPresenter> OnUnitMouseEnter;
    public static event Action<UnitPresenter> OnUnitMouseExit;
    public bool IsDead => unitData.IsDead;

    public GameObject CurrentUnitEffect;  //Ёффект дл€ юнита который ходит
    public GameObject HitUnitEffect;      //Ёффект дл€ юнитов дл€ атаки
    public TextMeshProUGUI HP_Text;

    public UnitPresenter()
    {
        unitData = new UnitData();
    }

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        CurrentUnitEffect.SetActive( false );
        CurrentUnitEffect.SetActive( false );
    }

    public void Init( UnitDataSO unitDataSo )
    {
        unitData.AttackDamage = unitDataSo.Damage;
        unitData.HP = unitDataSo.HP;
        unitData.unitType = unitDataSo.unitType;
        skeletonAnimation.skeletonDataAsset = unitDataSo.dataAsset;
        skeletonAnimation.skeleton.SetSkin( unitDataSo.Skin );
        skeletonAnimation.skeleton.SetSlotsToSetupPose(); // Use the pose from setup pose.
        skeletonAnimation.Update( 0 );

        HP_Text.text = unitData.HP.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if ( OnUnitMouseEnter != null )
            OnUnitMouseEnter( this );
        //Debug.Log( "ћышь зашла на позицию" );
    }

    private void OnMouseExit()
    {
        if ( OnUnitMouseExit != null )
            OnUnitMouseExit( this );
        //Debug.Log( "ћышь зашла на позицию" );
    }

    private void OnMouseDown()
    {
        if ( OnUnitClicked != null )
            OnUnitClicked( this );

        //Debug.Log( "ўелкнули мышью" );
    }

    public void Action( List<UnitPresenter> unitsToAtack, UnitPresenter clickedUnit )
    {
        IActionStrategy actStrategy = ActionStrategyFabric.GetStrategy( unitData.unitType ); //new ActStrat_HitAllUnits();
        actStrategy.Action( this, unitsToAtack, clickedUnit );
    }

    public void TakeDamage( float damage )
    {
        unitData.TakeDamage( damage );
        skeletonAnimation.AnimationState.SetAnimation( 0, "Damage", false );
        HP_Text.text = unitData.HP.ToString();
    }

    public void PlayAtackAnimation( string animName )
    {
        skeletonAnimation.AnimationState.SetAnimation( 0, animName, false );
    }

    public void SetActSprite(bool val){ CurrentUnitEffect.SetActive( val ); }

    public void SetHitSprite( bool val ){ HitUnitEffect.SetActive( val ); }

    public void SetDeadSate()
    {
        gameObject.SetActive( false );
    }

    public bool CanAct { get { return unitData.CanAct;  } set { unitData.CanAct = value; } }

    public float Damage { get { return unitData.AttackDamage; } }
}
