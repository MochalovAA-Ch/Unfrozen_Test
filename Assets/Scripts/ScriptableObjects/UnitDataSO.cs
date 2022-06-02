using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

[CreateAssetMenu(menuName ="Units/Miner")]
public class UnitDataSO : ScriptableObject
{
    [SerializeField]
    public float HP;
    [SerializeField]
    public float Damage;

    [SerializeField]
    public UNIT_TYPE unitType;
    [SerializeField]
    public SkeletonDataAsset dataAsset;

    //TODO: сделать выбор из списка скинов SkeletonDataAsset
    public string Skin = "base";

}
