using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAntiBody", menuName = "ImmuneDenfense/AntiBody")]
public class AntiBodyData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private AntiBodyType type;
    [SerializeField] private Sprite sprite;
    private bool isNotNeutralizer => type != AntiBodyType.nIgG1;

    [ShowIf("type", AntiBodyType.nIgG1)]
    [SerializeField] private int damage;
    [ShowIf("isNotNeutralizer")]
    [SerializeField] private float damageBoost;

    public string ID => uniProtID;

    public Sprite Sprite { get => sprite; set => sprite = value; }


}
