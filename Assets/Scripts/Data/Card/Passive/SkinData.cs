using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Passive/Skin")]
public class SkinData : PassiveCardBase
{
    [SerializeField] private int level;
    [SerializeField] private float bonus = 1.2f;

    public int GetHealth(int baseHP)
    {
        return (int)Mathf.Round(baseHP * Mathf.Pow(bonus, level));
    }
}
