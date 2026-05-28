using UnityEngine;

[CreateAssetMenu(fileName = "New Helper", menuName = "Passive/Helper T Cell")]
public class HelperTCell : PassiveCardBase
{
    [SerializeField] private float bonus = 1.2f;

    public float Bonus => bonus;
}
