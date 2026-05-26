using UnityEngine;
using UnityEngine.Assemblies;

[CreateAssetMenu(fileName = "NewCard", menuName = "ImmuneDefense/Plasmacytes")]
public class PlasmacyteData : CardBase
{
    [SerializeField] private int baseProductionTime;
    [SerializeField] private int knownProductionTime;
    private int currentTime = 0;
    private int tickTime = 0;

    public int CurrentTime { get => currentTime; set => currentTime = value; }
    public int TickTime { get => tickTime; set => tickTime = value; }

    public AntiBodyData GetAntiBody(AntigenData antigen)
    {
        return antigen.Matches[Random.Range(0, antigen.Matches.Count)];
    }

}
