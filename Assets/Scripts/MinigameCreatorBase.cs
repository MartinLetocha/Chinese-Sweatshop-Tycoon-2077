using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "settings", menuName = "ScriptableObjects/songSettings")]
public class MinigameCreatorBase : ScriptableObject
{
    public TextAsset plan;
    public float reward;
    public int level;
    public List<Sprite> images = new List<Sprite>();
    public bool sex;
}
