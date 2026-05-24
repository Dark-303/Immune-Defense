using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPathogen", menuName = "Enemies/Pathogen")]
public class PathogenData : ScriptableObject
{
    [Header("Biological Stats")]
    [SerializeField] private string pathogenName;
    [SerializeField] private int maxHP;
    [SerializeField] private int maxDamage;
    [SerializeField] private Sprite sprite;

    [SerializeField] private AntigenData antigen;
    [SerializeField] private List<ReceptorType> detectable;

    // Note to self this is so much nicer.
    public string PathogenName { get => pathogenName; set => pathogenName = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int MaxDamage { get => maxDamage; set => maxDamage = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public AntigenData Antigen { get => antigen; set => antigen = value; }
    public List<ReceptorType> Detectable { get => detectable; }
}