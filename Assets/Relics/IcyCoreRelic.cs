using UnityEngine;

[CreateAssetMenu(fileName = "IcyCore", menuName = "Relics/Icy Core")]
public class IcyCoreRelic : RelicData
{
    [Range(0, 100)] public int slowFactor;
    public float slowTimer = 1f;

}
