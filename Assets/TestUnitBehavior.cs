using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class TestUnitBehavior : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;

    FightLogic fightLogic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        SkeletonData skeletonData = skeletonAnimation.SkeletonDataAsset.GetSkeletonData( false );
        skeletonAnimation.skeleton.SetSkin( skeletonData.FindSkin( "blood" ) );
        fightLogic = new FightLogic();
        fightLogic.Init();
    }

    // Update is called once per frame
    void Update()
    {

        if ( Input.GetKeyDown( KeyCode.A ) )
        {
            fightLogic.StartNewTurn();
            //skeletonAnimation.AnimationState.SetAnimation( 0, "Damage", false );
        }

        if ( Input.GetKeyDown( KeyCode.D ) )
        {
            
           // skeletonAnimation.AnimationState.SetAnimation( 0, "Miner_1", false );
            //skeletonAnimation.initialSkinName = "blood";
            skeletonAnimation.skeleton.SetSkin( "blood" );
            skeletonAnimation.skeleton.SetSlotsToSetupPose(); // Use the pose from setup pose.
            skeletonAnimation.Update( 0 ); // Use the pose in the currently active animation.

            Resources.UnloadUnusedAssets();
            //skeletonData.Skins
        }

    }
}
