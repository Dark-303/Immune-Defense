using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAntiBody", menuName = "ImmuneDefense/AntiBody")]
public class AntiBodyData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private AntiBodyType type;
    [SerializeField] private Sprite sprite;
    private HashSet<ReceptorType> receptors = new HashSet<ReceptorType>();
    private bool isNotNeutralizer => type != AntiBodyType.nIgG1;

    [ShowIf("type", AntiBodyType.nIgG1)]
    [SerializeField] private int damage;
    [ShowIf("isNotNeutralizer")]
    [SerializeField] private float damageBoost;

    public string ID => uniProtID;

    public AntiBodyType Type => type;
    public int Damage => damage;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public HashSet<ReceptorType> Receptors => receptors;

    private void OnEnable()
    {
        switch (type)
        {
            case AntiBodyType.fIgG1:
                receptors.Add(ReceptorType.FcγRI);
                receptors.Add(ReceptorType.FcγRIIA);
                receptors.Add(ReceptorType.FcγRIIB);
                receptors.Add(ReceptorType.FcγRIIIA);
                receptors.Add(ReceptorType.FcγRIIIB);
                break;
            case AntiBodyType.nfIgG2:
                receptors.Add(ReceptorType.FcγRIIA);
                receptors.Add(ReceptorType.FcγRIIB);
                break;
            case AntiBodyType.fIgG3:
                receptors.Add(ReceptorType.FcγRI);
                receptors.Add(ReceptorType.FcγRIIA);
                receptors.Add(ReceptorType.FcγRIIB);
                receptors.Add(ReceptorType.FcγRIIIA);
                receptors.Add(ReceptorType.FcγRIIIB);
                break;
            case AntiBodyType.fIgE:
                receptors.Add(ReceptorType.FcεRI);
                break;
        }
    }
}
