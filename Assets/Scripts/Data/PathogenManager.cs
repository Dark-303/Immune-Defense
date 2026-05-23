using System.Collections.Generic;
using UnityEngine;

class PathogenManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject pathogenPrefab;

    [Header("Areas")]
    [SerializeField] private Transform pathogenArea;
    [SerializeField] private Transform antibodyArea;

    [Header("Pathogens")]
    [SerializeField] private List<PathogenData> pathogens;
    private List<PathogenData> masterList;

    private void Start()
    {
        masterList = new List<PathogenData>(pathogens);
    }

    public void GetPathogen()
    {
        PathogenData pathogen = pathogens[0];
        pathogens.RemoveAt(0);
        GameObject newPathogen = Instantiate(pathogenPrefab, pathogenArea);
        PathogenDisplay display = newPathogen.GetComponent<PathogenDisplay>();
        display.SetData(pathogen);
        display.UpdateVisuals();
    }
}