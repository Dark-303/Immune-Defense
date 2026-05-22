using UnityEngine;

[CreateAssetMenu(fileName = "NewPathogen", menuName = "Enemies/Pathogen")]
public class PathogenData : ScriptableObject
{
    [Header("Biological Stats")]
    public string pathogenName;
    public int maxHP;
    public int maxDamage;
    public Sprite visualSprite;

    public AntigenData
}