using UnityEngine;

[CreateAssetMenu(fileName = "New DC", menuName = "Passive/Dendritic Cell")]
public class DCData : PassiveCardBase
{
    [SerializeField] private float bonus = 1.2f;

    public float Bonus => bonus;
}
