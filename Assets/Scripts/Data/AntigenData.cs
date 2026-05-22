using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Enemies/AntigenData")]
public class AntigenData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private List<AntiBodyData> match;
    [SerializeField] private List<AntiBodyData> compatible;
}
