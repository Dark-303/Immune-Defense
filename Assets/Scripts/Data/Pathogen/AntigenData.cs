using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAntigen", menuName = "Enemies/AntigenData")]
public class AntigenData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private List<AntiBodyData> match;
    [SerializeField] private List<AntiBodyData> compatible;

    public List<AntiBodyData> Matches => match;
    public string ID => uniProtID;
}
