using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

class AntiBodyData : ScriptableObject
{
    [SerializeField] private string uniProtID;
    [SerializeField] private AntiBodyType type;
    private int damage;
    
}
