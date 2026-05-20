using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ImmuneDenfense/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string desc;
    public int damage;
    public int cost;
    public Sprite cardImage;  
}