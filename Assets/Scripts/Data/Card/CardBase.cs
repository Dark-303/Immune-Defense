using UnityEngine;

public abstract class CardBase : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField][TextArea] private string desc;
    [SerializeField] private int cost;
    [SerializeField] private Sprite sprite;
    public string Name => cardName;
    public string Desc => desc;
    public int Cost => cost;
    public Sprite Sprite => sprite;
}
