using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ImmuneDefense/Plasmacytes")]
public class PlasmacyteData : CardBase
{
    [SerializeField] private int baseProductionTime;
    [SerializeField] private int knownProductionTime;

    public AntiBodyData GetAntiBody(AntigenData antigen)
    {
        return antigen.Matches[Random.Range(0, antigen.Matches.Count)];
    }

}
