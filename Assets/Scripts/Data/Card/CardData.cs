using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ImmuneDefense/Card")]
public class CardData : CardBase
{
    [SerializeField] private int damage;

    public int Damage => damage;
}