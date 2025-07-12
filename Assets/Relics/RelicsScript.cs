using UnityEngine;

public abstract class RelicData : ScriptableObject
{
    public string relicName;

    [TextArea]
    public string description;

    public Sprite image;

    [Range(0, 3)] public int rarity;

    public bool enabled = true;
    
    public bool obtained = false;


}