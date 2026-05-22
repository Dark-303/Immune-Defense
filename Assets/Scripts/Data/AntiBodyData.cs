using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAntiBody", menuName = "ImmuneDenfense/AntiBody")]
class AntiBodyData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private AntiBodyType type;
    private bool isNotNeutralizer => type != AntiBodyType.nIgG1;

    [ShowIf("type", AntiBodyType.nIgG1)]
    [SerializeField] private int damage;
    [ShowIf("isNotNeutralizer")]
    [SerializeField] private float damageBoost;

}
