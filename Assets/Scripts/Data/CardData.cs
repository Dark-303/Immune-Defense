using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ImmuneDenfense/Card")]
public class CardData : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] [TextArea] private string desc;
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] private Sprite cardImage;  

    public string GetCardName()
    {
        return cardName;
    }

    public string GetDesc()
    {
        return desc;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetCost()
    {
        return cost;
    }

    public Sprite GetImage()
    {
        return cardImage;
    }
}