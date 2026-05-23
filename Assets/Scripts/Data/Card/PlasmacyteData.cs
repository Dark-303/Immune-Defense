using UnityEngine;
using UnityEngine.Assemblies;

[CreateAssetMenu(fileName = "NewCard", menuName = "ImmuneDefense/Plasmacytes")]
public class PlasmacyteData : CardBase
{
    [SerializeField] private int baseProductionTime;
    [SerializeField] private int knownProductionTime;
    private int currentTime = 0;

    public int CurrentTime { get; set; }

    public AntiBodyData GetAntiBody(AntigenData antigen)
    {
        return antigen.Matches[Random.Range(0, antigen.Matches.Count)];
    }

}
