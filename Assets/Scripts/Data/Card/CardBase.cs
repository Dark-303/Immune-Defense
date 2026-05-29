using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardBase : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField][TextArea] private string desc;
    [SerializeField] private int cost;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<ReceptorType> allowed = new List<ReceptorType>();
    [SerializeField] private List<ReceptorType> unlocked = new List<ReceptorType>();
    public string Name => cardName;
    public string Desc => desc;
    public int Cost => cost;
    public Sprite Sprite => sprite;
    public List<ReceptorType> Unlocked => unlocked;

    public List<ReceptorType> Allowed => allowed;

    public void toggleReceptor(ReceptorType r)
    {
        if (unlocked.Contains(r))
        {
            unlocked.Remove(r);
        } else if (allowed.Contains(r))
        {
            unlocked.Add(r);
        }
    }
}
