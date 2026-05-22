using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Enemies/AntigenData")]
public class AntigenData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private List<AntiBodyType> types;
    [SerializeField] private List<AntiBodyData> compatible;
    private List<AntiBodyData> antiBodies;
    public AntigenData()
    {
        foreach (AntiBodyType type in types)
        {
            antiBodies.Add(new AntiBodyData(uniProtID, type));
        }
    }

    public AntiBodyData GetAnti
}
