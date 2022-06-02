using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using  Spine;
using Spine.Unity;
using TMPro;


// ласс реализующий отображение и поведение юнитов
[RequireComponent( typeof(SkeletonAnimation ) )]
public class UnitPresenter : MonoBehaviour
{
    protected UnitData unitData;
    SkeletonAnimation skeletonAnimation;
    MeshRenderer meshRenderer;

    public static event Action<UnitPresenter> OnUnitClicked;
    public static event Action<UnitPresenter> OnUnitMouseEnter;
    public static event Action<UnitPresenter> OnUnitMouseExit;
    public bool IsDead => unitData.IsDead;

    public GameObject CurrentUnitEffect;  //Ёффект дл€ юнита который ходит
    public GameObject HitUnitEffect;      //Ёффект дл€ юнитов дл€ атаки
    public TextMeshProUGUI HP_Text;


    [SerializeField, Min( 0 )] float moveToFrontSpeed;  //—корость движени€ юнита на передний план
    public float MoveToForefrontSpeed => moveToFrontSpeed;

    public UnitPresenter()
    {
        unitData = new UnitData();
    }

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        meshRenderer = GetComponent<MeshRenderer>();
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
        //TODO: ¬место корутин можно использовать state machine в методе Update,
        //дл€ задержки между анимаци€ми и т.д.
    }

    private void OnMouseEnter() { if ( OnUnitMouseEnter != null ) OnUnitMouseEnter( this ); }

    private void OnMouseExit() { if ( OnUnitMouseExit != null ) OnUnitMouseExit( this ); }

    private void OnMouseDown() { if ( OnUnitClicked != null ) OnUnitClicked( this ); }

    public void Action( List<UnitPresenter> unitsToAtack, UnitPresenter clickedUnit )
    {
        StartCoroutine( FightCoroutine( unitsToAtack, clickedUnit ) );
    }

    public void TakeDamage( float damage )
    {
        unitData.TakeDamage( damage );
        skeletonAnimation.AnimationState.SetAnimation( 0, "Damage", false );
        HP_Text.text = unitData.HP.ToString();
    }

    public void SetIdleState() 
    { 
        skeletonAnimation.AnimationState.SetAnimation( 0, "Idle", true ); 
        meshRenderer.sortingOrder = 0; 
    }

    public void IncreaseScale()
    {
        transform.localScale = new Vector3( transform.localScale.x + Time.deltaTime * MoveToForefrontSpeed, transform.localScale.y + Time.deltaTime * MoveToForefrontSpeed, transform.localScale.z );
    }

    public void DecreaseScale()
    {
        transform.localScale = new Vector3( transform.localScale.x - Time.deltaTime * MoveToForefrontSpeed, transform.localScale.y - Time.deltaTime * MoveToForefrontSpeed, transform.localScale.z );
    }

    IEnumerator MoveUnitToForefront( IActionStrategy actStrategy, List<UnitPresenter> unitsToAtack, UnitPresenter clickedUnit )
    {
        meshRenderer.sortingOrder = 1;

        while ( transform.localScale.x < 1.3f )
        {
            yield return new WaitForEndOfFrame();
            IncreaseScale();
            actStrategy.MoveUnitsToForefront( this, unitsToAtack, clickedUnit );
        }
    }

    IEnumerator MoveUnitToBackfront( IActionStrategy actStrategy, List<UnitPresenter> unitsToAtack, UnitPresenter clickedUnit )
    {
        

        while ( transform.localScale.x > 1.0f )
        {
            yield return new WaitForEndOfFrame();
            DecreaseScale();
            actStrategy.MoveUnitsToBackfront( this, unitsToAtack, clickedUnit );
        }

        unitsToAtack.ForEach( unit => unit.transform.localScale = new Vector3( 1, 1, 1 ) );

        transform.localScale = new Vector3( 1, 1, 1 );
    }

    //‘ункци€ выдвигающа€ unit на поле бо€
    public IEnumerator FightCoroutine( List<UnitPresenter> unitsToAtack, UnitPresenter clickedUnit )
    {
        IActionStrategy actStrategy = ActionStrategyFabric.GetStrategy( unitData.unitType ); //new ActStrat_HitAllUnits();

        StartCoroutine( MoveUnitToForefront( actStrategy, unitsToAtack, clickedUnit ) );
        while ( transform.localScale.x < 1.3f )
        {
            yield return new WaitForEndOfFrame();
        }
        //IActionStrategy actStrategy = ActionStrategyFabric.GetStrategy( unitData.unitType ); //new ActStrat_HitAllUnits();
        actStrategy.Action( this, unitsToAtack, clickedUnit );

        yield return new WaitForSeconds( 1.0f ); //TODO: подставить врем€ анимации атаки юнита

        /*for( int i = 0; i < unitsToAtack.Count; i++ )
        {
            if ( unitsToAtack[i] != null )
                unitsToAtack[i].SetIdleState();
        }*/

        //SetIdleState();
        unitsToAtack.ForEach( unit => unit.SetIdleState() );

        StartCoroutine( MoveUnitToBackfront( actStrategy, unitsToAtack, clickedUnit ) );

        while ( transform.localScale.x > 1.0f )
        {
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3( 1, 1, 1 );
    }

    public void PlayAtackAnimation( string animName ){ skeletonAnimation.AnimationState.SetAnimation( 0, animName, false ); }

    public void SetActSprite(bool val){ CurrentUnitEffect.SetActive( val ); }

    public void SetHitSprite( bool val ){ HitUnitEffect.SetActive( val ); }

    public void SetDeadSate(){ gameObject.SetActive( false ); }

    public bool CanAct { get { return unitData.CanAct;  } set { unitData.CanAct = value; } }

    public float Damage { get { return unitData.AttackDamage; } }
}
