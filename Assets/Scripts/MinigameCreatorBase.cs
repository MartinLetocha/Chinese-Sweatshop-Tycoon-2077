using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "settings", menuName = "ScriptableObjects/songSettings")]
public class MinigameCreatorBase : ScriptableObject
{
    public TextAsset plan;
    public float reward;
    public bool sex;
}
