using UnityEngine;

[CreateAssetMenu(fileName = "New Platelet", menuName = "Passive/Platelet")]
public class PlateletData : PassiveCardBase
{
    [SerializeField] private int heal;

    public int Heal { get => Random.Range(0, heal + 1); }
}
