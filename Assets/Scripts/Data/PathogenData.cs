using UnityEngine;

[CreateAssetMenu(fileName = "NewPathogen", menuName = "Enemies/Pathogen")]
public class PathogenData : ScriptableObject
{
    [Header("Biological Stats")]
    [SerializeField] private string pathogenName;
    [SerializeField] private int maxHP;
    [SerializeField] private int maxDamage;
    [SerializeField] private Sprite visualSprite;

    [SerializeField] private AntigenData antigen;
}